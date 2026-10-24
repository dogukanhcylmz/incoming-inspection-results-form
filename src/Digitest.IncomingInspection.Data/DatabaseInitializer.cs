using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Digitest.IncomingInspection.Data;

public sealed class DatabaseInitializer
{
    private const string ScriptResourcePrefix = "Scripts.";
    private const string DatabaseCollation = "Turkish_CI_AS";
    private const int ScriptTimeoutSeconds = 120;

    private static readonly Regex BatchSeparator =
        new(@"^\s*GO\s*(?:--.*)?$", RegexOptions.Multiline | RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);

    private static readonly Regex SafeDatabaseName =
        new(@"^[A-Za-z0-9_]{1,100}$", RegexOptions.CultureInvariant);

    private readonly string _connectionString;

    public DatabaseInitializer(string connectionString)
    {
        if (string.IsNullOrWhiteSpace(connectionString))
            throw new ArgumentException("Bağlantı cümlesi boş olamaz.", nameof(connectionString));

        _connectionString = connectionString;
    }

    /// <summary>Veritabanını oluşturur (yoksa) ve şemayı günceller.</summary>
    public async Task InitializeAsync(CancellationToken cancellationToken = default)
    {
        var databaseName = GetDatabaseName();

        await CreateDatabaseIfMissingAsync(databaseName, cancellationToken).ConfigureAwait(false);

        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken).ConfigureAwait(false);

        foreach (var (name, script) in LoadScripts())
        {
            foreach (var batch in SplitBatches(script))
            {
                try
                {
                    await connection.ExecuteAsync(new CommandDefinition(
                        batch, commandTimeout: ScriptTimeoutSeconds, cancellationToken: cancellationToken)).ConfigureAwait(false);
                }
                catch (SqlException ex)
                {
                    throw new InvalidOperationException($"Veritabanı scripti çalıştırılamadı: {name}\n{ex.Message}", ex);
                }
            }
        }
    }

    /// <summary>Veritabanını değiştirmeden sadece bağlantıyı test eder.</summary>
    public async Task TestConnectionAsync(CancellationToken cancellationToken = default)
    {
        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken).ConfigureAwait(false);
        await connection.ExecuteScalarAsync<int>(new CommandDefinition("SELECT 1;", cancellationToken: cancellationToken)).ConfigureAwait(false);
    }

    private string GetDatabaseName()
    {
        var builder = new SqlConnectionStringBuilder(_connectionString);
        var name = builder.InitialCatalog;

        if (string.IsNullOrWhiteSpace(name))
            throw new InvalidOperationException("Bağlantı cümlesinde veritabanı adı (Database=...) belirtilmelidir.");

        if (!SafeDatabaseName.IsMatch(name))
            throw new InvalidOperationException(
                $"Veritabanı adı '{name}' geçersiz. Sadece harf, rakam ve alt çizgi kullanılabilir.");

        return name;
    }

    private async Task CreateDatabaseIfMissingAsync(string databaseName, CancellationToken cancellationToken)
    {
        var masterBuilder = new SqlConnectionStringBuilder(_connectionString) { InitialCatalog = "master" };

        await using var master = new SqlConnection(masterBuilder.ConnectionString);
        await master.OpenAsync(cancellationToken).ConfigureAwait(false);

        var exists = await master.ExecuteScalarAsync<int>(new CommandDefinition(
            "SELECT CASE WHEN DB_ID(@Name) IS NULL THEN 0 ELSE 1 END;",
            new { Name = databaseName },
            cancellationToken: cancellationToken)).ConfigureAwait(false);

        if (exists == 1)
            return;

        // Veritabanı adı yukarıda doğrulandığı için köşeli parantez içinde güvenle kullanılabilir.
        var sql = $"CREATE DATABASE [{databaseName}] COLLATE {DatabaseCollation};";
        await master.ExecuteAsync(new CommandDefinition(
            sql, commandTimeout: ScriptTimeoutSeconds, cancellationToken: cancellationToken)).ConfigureAwait(false);
    }

    private static IEnumerable<(string Name, string Script)> LoadScripts()
    {
        var assembly = typeof(DatabaseInitializer).Assembly;

        var names = assembly.GetManifestResourceNames()
            .Where(n => n.StartsWith(ScriptResourcePrefix, StringComparison.Ordinal)
                        && n.EndsWith(".sql", StringComparison.OrdinalIgnoreCase))
            .OrderBy(n => n, StringComparer.Ordinal)
            .ToList();

        if (names.Count == 0)
            throw new InvalidOperationException("Gömülü veritabanı scriptleri bulunamadı.");

        foreach (var name in names)
        {
            using var stream = assembly.GetManifestResourceStream(name)
                               ?? throw new InvalidOperationException($"Script okunamadı: {name}");
            using var reader = new StreamReader(stream, Encoding.UTF8, detectEncodingFromByteOrderMarks: true);
            yield return (name, reader.ReadToEnd());
        }
    }

    private static IEnumerable<string> SplitBatches(string script) =>
        BatchSeparator.Split(script)
            .Select(b => b.Trim())
            .Where(b => b.Length > 0);
}
