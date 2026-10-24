using Digitest.IncomingInspection.Core.Enums;

namespace Digitest.IncomingInspection.Core.Models;

public sealed class InspectionForm
{
    public int Id { get; set; }

    public string FormNo { get; set; } = string.Empty;

    public string? ProjectNo { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string LotNo { get; set; } = string.Empty;
    public DateTime InspectionDate { get; set; } = DateTime.Today;
    public string? InventoryCode { get; set; }
    public decimal? IncomingQuantity { get; set; }
    public string? QuantityUnit { get; set; }
    public string? TechnicalDrawingNo { get; set; }
    public string? TechnicalDrawingRev { get; set; }
    public string? Other { get; set; }

    public string? WaybillNo { get; set; }
    public string? PurchaseOrderNo { get; set; }
    public DateTime? ArrivalDate { get; set; }
    public string? InventoryGroup { get; set; }
    public string SupplierName { get; set; } = string.Empty;

    public string? NcrText { get; set; }
    public string? Notes { get; set; }
    public string? InspectorName { get; set; }
    public OverallResult OverallResult { get; set; }

    //formun oluştuğu andaki
    public string DocumentNo { get; set; } = string.Empty;
    public DateTime DocPublishDate { get; set; }
    public string DocRevision { get; set; } = string.Empty;
    public DateTime? DocRevisionDate { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public List<InspectionItem> Items { get; set; } = new();

    public bool IsNew => Id == 0;

    /// Satırlardam genel sonucu hesaplar
    /// Red; Beklemede; Şartlı Kabul; Kabul.
    public OverallResult CalculateOverallResult()
    {
        var used = Items.Where(i => i.IsUsed).ToList();

        if (used.Any(i => i.Decision == InspectionDecision.Rejected))
            return OverallResult.Rejected;

        if (used.Count == 0 || used.Any(i => i.Decision == InspectionDecision.None))
            return OverallResult.Pending;

        if (used.Any(i => i.Decision == InspectionDecision.ConditionalAccepted))
            return OverallResult.ConditionalAccepted;

        return OverallResult.Accepted;
    }

    public void EnsureAllRows()
    {
        for (var rowNo = 1; rowNo <= FormConstants.LineCount; rowNo++)
        {
            if (Items.All(i => i.RowNo != rowNo))
                Items.Add(InspectionItem.CreateEmpty(rowNo)); //boş serbest satır olarak yapf
        }

        Items = Items.OrderBy(i => i.RowNo).ToList();
    }
}
