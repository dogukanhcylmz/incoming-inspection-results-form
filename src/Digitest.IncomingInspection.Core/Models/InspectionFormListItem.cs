using System.Globalization;
using Digitest.IncomingInspection.Core.Enums;

namespace Digitest.IncomingInspection.Core.Models;

/// kayıt listesi ekranı özet satır
public sealed class InspectionFormListItem
{
    public int Id { get; set; }
    public string FormNo { get; set; } = string.Empty;
    public DateTime InspectionDate { get; set; }
    public string SupplierName { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public string LotNo { get; set; } = string.Empty;
    public string? PurchaseOrderNo { get; set; }
    public decimal? IncomingQuantity { get; set; }
    public string? QuantityUnit { get; set; }
    public string? InspectorName { get; set; }
    public OverallResult OverallResult { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public string OverallResultText => OverallResult.ToDisplayText();

    public string QuantityText =>
        IncomingQuantity is null
            ? string.Empty
            : $"{IncomingQuantity.Value.ToString("0.###", CultureInfo.CurrentCulture)} {QuantityUnit}".Trim();

    public DateTime LastChangedAt => UpdatedAt ?? CreatedAt;
}
