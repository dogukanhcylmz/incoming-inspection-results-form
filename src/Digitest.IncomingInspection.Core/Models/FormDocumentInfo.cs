namespace Digitest.IncomingInspection.Core.Models;

/// sağ üst köşe doküman bilgileri
public sealed class FormDocumentInfo
{
    public string DocumentNo { get; set; } = "FR-QC-009";
    public DateTime PublishDate { get; set; } = new(2026, 6, 1);
    public string Revision { get; set; } = "00";
    public DateTime? RevisionDate { get; set; }
}
