using Digitest.IncomingInspection.Core.Enums;

namespace Digitest.IncomingInspection.Core.Models;

public sealed class InspectionFormFilter
{
    public DateTime? DateFrom { get; set; }
    public DateTime? DateTo { get; set; }
    public string? FormNo { get; set; }
    public string? SupplierName { get; set; }
    public string? ProductName { get; set; }
    public string? LotNo { get; set; }
    public OverallResult? Result { get; set; }

    /// listede gösterilecek max kayıt sayısı
    public int MaxRows { get; set; } = 2000;
}
