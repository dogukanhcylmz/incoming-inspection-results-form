using System.Diagnostics;
using Microsoft.Data.SqlClient;

namespace Digitest.IncomingInspection.WinForms.Infrastructure;

/// <summary>
/// Mesaj kutuları ve küçük arayüz yardımcıları.
/// </summary>
internal static class UiHelper
{
    public const string AppTitle = "DIGITEST Girdi Muayene";

    public static void ShowError(IWin32Window? owner, Exception exception, string? context = null)
    {
        var message = BuildErrorMessage(exception, context);
        MessageBox.Show(owner, message, AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    public static void Warn(IWin32Window? owner, string message) =>
        MessageBox.Show(owner, message, AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);

    public static void Info(IWin32Window? owner, string message) =>
        MessageBox.Show(owner, message, AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);

    public static bool Confirm(IWin32Window? owner, string message) =>
        MessageBox.Show(owner, message, AppTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question,
            MessageBoxDefaultButton.Button2) == DialogResult.Yes;

    /// <summary>Dosyayı Windows'taki varsayılan programla açar (PDF için PDF görüntüleyici).</summary>
    public static void OpenWithShell(string path)
    {
        Process.Start(new ProcessStartInfo(path) { UseShellExecute = true });
    }

    private static string BuildErrorMessage(Exception exception, string? context)
    {
        var root = Unwrap(exception);

        var detail = root switch
        {
            SqlException sql => DescribeSqlError(sql),
            _ => root.Message
        };

        return string.IsNullOrWhiteSpace(context) ? detail : $"{context}\n\n{detail}";
    }

    private static Exception Unwrap(Exception exception)
    {
        var current = exception;

        while (current is AggregateException { InnerExceptions.Count: 1 } aggregate)
            current = aggregate.InnerExceptions[0];

        return current;
    }

    private static string DescribeSqlError(SqlException exception) => exception.Number switch
    {
        -2 => "Veritabanı işlemi zaman aşımına uğradı. Lütfen tekrar deneyin.",
        2 or 53 or -1 or 258 =>
            "SQL Server'a ulaşılamadı. SQL Server servisinin çalıştığını ve sunucu adının doğru olduğunu kontrol edin.\n\n" +
            $"Ayrıntı: {exception.Message}",
        4060 => $"Veritabanı açılamadı. Veritabanı adı doğru mu?\n\nAyrıntı: {exception.Message}",
        18456 => $"SQL Server oturum açma hatası. Kullanıcı adı/şifre veya Windows yetkisini kontrol edin.\n\nAyrıntı: {exception.Message}",
        547 => $"Kayıt başka verilerle ilişkili olduğu için işlem yapılamadı.\n\nAyrıntı: {exception.Message}",
        2627 or 2601 => $"Aynı numarayla başka bir kayıt zaten var.\n\nAyrıntı: {exception.Message}",
        _ => $"Veritabanı hatası ({exception.Number}): {exception.Message}"
    };
}
