using Microsoft.Data.SqlClient;

namespace Digitest.IncomingInspection.Data;

///Her işlem kendi bağlantısını açıp kapatıyor. Bağlantı havuzunu SqlClient yönetiyo 
public sealed class SqlConnectionFactory
{
    public SqlConnectionFactory(string connectionString)
    {
        if (string.IsNullOrWhiteSpace(connectionString))
            throw new ArgumentException("Bağlantı cümlesi boş olamaz.", nameof(connectionString));

        ConnectionString = connectionString;
    }

    public string ConnectionString { get; }

    public string Description
    {
        get
        {
            var builder = new SqlConnectionStringBuilder(ConnectionString);
            return $"{builder.DataSource} / {builder.InitialCatalog}";
        }
    }

    public async Task<SqlConnection> OpenAsync(CancellationToken cancellationToken = default)
    {
        var connection = new SqlConnection(ConnectionString);
        try
        {
            await connection.OpenAsync(cancellationToken).ConfigureAwait(false);
            return connection;
        }
        catch
        {
            await connection.DisposeAsync().ConfigureAwait(false);
            throw;
        }
    }
}
