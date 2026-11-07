namespace Digitest.IncomingInspection.WinForms.Infrastructure;


internal static class AppAssets
{
    private static readonly Lazy<Image?> LogoImage = new(() => TryLoadImage(Path.Combine("Assets", "logo.png")));
    private static readonly Lazy<Icon?> IconFile = new(() => TryLoadIcon(Path.Combine("Assets", "app.ico")));

    public static Image? Logo => LogoImage.Value;

    public static Icon? AppIcon => IconFile.Value;

    private static Image? TryLoadImage(string relativePath)
    {
        try
        {
            var path = Path.Combine(AppContext.BaseDirectory, relativePath);
            if (!File.Exists(path))
                return null;

            // Dosyayı kilitlememek için belleğe kopyalanır.
            using var stream = File.OpenRead(path);
            using var image = Image.FromStream(stream);
            return new Bitmap(image);
        }
        catch
        {
            return null;
        }
    }

    private static Icon? TryLoadIcon(string relativePath)
    {
        try
        {
            var path = Path.Combine(AppContext.BaseDirectory, relativePath);
            return File.Exists(path) ? new Icon(path) : null;
        }
        catch
        {
            return null;
        }
    }
}
