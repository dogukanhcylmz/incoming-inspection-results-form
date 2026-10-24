using Digitest.IncomingInspection.WinForms.Forms;
using Digitest.IncomingInspection.WinForms.Infrastructure;
using QuestPDF.Infrastructure;

namespace Digitest.IncomingInspection.WinForms;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();

        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
        Application.ThreadException += (_, e) => UiHelper.ShowError(null, e.Exception);
        AppDomain.CurrentDomain.UnhandledException += (_, e) =>
        {
            if (e.ExceptionObject is Exception ex)
                UiHelper.ShowError(null, ex);
        };

        // QuestPDF Community lisansı (bkz. README - Lisans).
        QuestPDF.Settings.License = LicenseType.Community;

        AppSettings settings;
        try
        {
            settings = AppSettings.Load();
        }
        catch (Exception ex)
        {
            UiHelper.ShowError(null, ex, "Ayar dosyası (appsettings.json) okunamadı.");
            return;
        }

        var services = new AppServices(settings);

        if (!PrepareDatabase(services))
            return;

        Application.Run(new MainForm(services));
    }

    ///AutoCreate açıksa oluşturur,günceller,kapalıysa sadece bağlantıyı dener
    private static bool PrepareDatabase(AppServices services)
    {
        Cursor.Current = Cursors.WaitCursor;
        try
        {
            if (services.Settings.AutoCreateDatabase)
                Task.Run(() => services.DatabaseInitializer.InitializeAsync()).GetAwaiter().GetResult();
            else
                Task.Run(() => services.DatabaseInitializer.TestConnectionAsync()).GetAwaiter().GetResult();

            return true;
        }
        catch (Exception ex)
        {
            UiHelper.ShowError(null, ex,
                "Veritabanına bağlanılamadı.\n\n" +
                $"Bağlantı: {services.ConnectionDescription}\n\n" +
                "SQL Server servisinin çalıştığından ve appsettings.json içindeki bağlantı cümlesinin doğru olduğundan emin olun.");
            return false;
        }
        finally
        {
            Cursor.Current = Cursors.Default;
        }
    }
}
