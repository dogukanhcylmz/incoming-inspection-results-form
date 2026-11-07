using System.Globalization;
using System.Text;
using Digitest.IncomingInspection.Core.Models;

namespace Digitest.IncomingInspection.Reporting;

/// <summary>
/// Formun PDF'ini üretip çıktı klasörüne kaydeder.
/// Dosya adı: FR-QC-009_GM-2026-0001_LOTNO.pdf
/// </summary>
public sealed class InspectionFormPdfExporter
{
    private const int MaxFileNameLength = 120;

    private readonly InspectionFormPdfGenerator _generator;

    public InspectionFormPdfExporter(InspectionFormPdfGenerator generator, string? outputFolder)
    {
        _generator = generator ?? throw new ArgumentNullException(nameof(generator));
        OutputFolder = ResolveFolder(outputFolder);
    }

    /// <summary>PDF'lerin kaydedildiği klasör (ortam değişkenleri çözülmüş hali).</summary>
    public string OutputFolder { get; }

    /// <summary>PDF'i oluşturur ve tam dosya yolunu döner.</summary>
    public string Export(InspectionForm form)
    {
        ArgumentNullException.ThrowIfNull(form);

        if (form.IsNew || string.IsNullOrWhiteSpace(form.FormNo))
            throw new InvalidOperationException("PDF almadan önce form kaydedilmelidir.");

        Directory.CreateDirectory(OutputFolder);

        var bytes = _generator.Generate(form);
        var baseName = BuildFileName(form);
        var path = Path.Combine(OutputFolder, baseName + ".pdf");

        try
        {
            File.WriteAllBytes(path, bytes);
        }
        catch (IOException)
        {
            // Aynı isimli PDF bir görüntüleyicide açıksa üzerine yazılamaz; zaman damgalı yeni dosya oluşturulur.
            var stamp = DateTime.Now.ToString("yyyyMMdd_HHmmss", CultureInfo.InvariantCulture);
            path = Path.Combine(OutputFolder, $"{baseName}_{stamp}.pdf");
            File.WriteAllBytes(path, bytes);
        }

        return path;
    }

    private static string ResolveFolder(string? configured)
    {
        if (string.IsNullOrWhiteSpace(configured))
        {
            var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            return Path.Combine(documents, "MDH", "Girdi Muayene");
        }

        return Path.GetFullPath(Environment.ExpandEnvironmentVariables(configured.Trim()));
    }

    private static string BuildFileName(InspectionForm form)
    {
        var raw = string.Join("_", new[] { form.DocumentNo, form.FormNo, form.LotNo }
            .Where(p => !string.IsNullOrWhiteSpace(p)));

        var invalid = Path.GetInvalidFileNameChars();
        var builder = new StringBuilder(raw.Length);

        foreach (var ch in raw.Trim())
            builder.Append(invalid.Contains(ch) || char.IsWhiteSpace(ch) ? '_' : ch);

        var name = builder.ToString();
        while (name.Contains("__", StringComparison.Ordinal))
            name = name.Replace("__", "_", StringComparison.Ordinal);

        name = name.Trim('_', '.');
        if (name.Length > MaxFileNameLength)
            name = name[..MaxFileNameLength];

        return name.Length == 0 ? "GirdiMuayeneFormu" : name;
    }
}
