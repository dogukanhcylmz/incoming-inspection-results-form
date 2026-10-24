using Microsoft.Extensions.Configuration;

namespace Digitest.IncomingInspection.WinForms;

internal sealed class AppSettings
{
    public required string ConnectionString { get; init; }

    /// açılışta veritabanı ve tablolar otomatik oluşturulur/güncellenr
    public bool AutoCreateDatabase { get; init; } = true;

    /// <summary>PDF klasörü. %USERPROFILE% gibi ortam değişkenleri kullanılabilir. Boşsa Belgeler\DIGITEST\Girdi Muayene.</summary>
    public string? PdfOutputFolder { get; init; }

    /// <summary>true ise oluşturulan PDF varsayılan görüntüleyicide açılır.</summary>
    public bool OpenPdfAfterCreate { get; init; } = true;

    public static AppSettings Load()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
            .AddJsonFile("appsettings.Local.json", optional: true, reloadOnChange: false)
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrWhiteSpace(connectionString))
            throw new InvalidOperationException("appsettings.json içinde ConnectionStrings:DefaultConnection tanımlanmalıdır.");

        return new AppSettings
        {
            ConnectionString = connectionString,
            AutoCreateDatabase = ReadBool(configuration, "Database:AutoCreate", true),
            PdfOutputFolder = configuration["Pdf:OutputFolder"],
            OpenPdfAfterCreate = ReadBool(configuration, "Pdf:OpenAfterCreate", true)
        };
    }

    private static bool ReadBool(IConfiguration configuration, string key, bool defaultValue) =>
        bool.TryParse(configuration[key], out var value) ? value : defaultValue;
}
