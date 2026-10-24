namespace Digitest.IncomingInspection.Core.Models;

public sealed class InspectionTemplateItem
{
    public int RowNo { get; set; }
    public string InspectionTypeTr { get; set; } = string.Empty;
    public string? InspectionTypeEn { get; set; }
    public string? AcceptCriteria { get; set; }
    public bool IsActive { get; set; } = true;
}
/// Yeni formda hazır gelen sabit muayene satırı 
