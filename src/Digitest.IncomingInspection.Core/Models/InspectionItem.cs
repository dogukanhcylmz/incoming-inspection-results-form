using Digitest.IncomingInspection.Core.Enums;

namespace Digitest.IncomingInspection.Core.Models;

/// Formdaki muayene tablosunun bir satırı 
public sealed class InspectionItem
{
    public int Id { get; set; }
    public int FormId { get; set; }

    // 1-7
    public int RowNo { get; set; }

    
    /// 7. satır serbest
    public bool IsFixed { get; set; }

    public string InspectionTypeTr { get; set; } = string.Empty;
    public string? InspectionTypeEn { get; set; }
    public string? AcceptCriteria { get; set; }
    public string? Applied { get; set; }
    public InspectionDecision Decision { get; set; }
    public string? Statement { get; set; }

    public bool IsUsed => IsFixed || !string.IsNullOrWhiteSpace(InspectionTypeTr);

    /// bir bilgi girilmiş mi
    public bool HasAnyInput =>
        Decision != InspectionDecision.None
        || !string.IsNullOrWhiteSpace(Applied)
        || !string.IsNullOrWhiteSpace(Statement)
        || !string.IsNullOrWhiteSpace(AcceptCriteria);

    public string InspectionTypeDisplay =>
        string.IsNullOrWhiteSpace(InspectionTypeEn)
            ? InspectionTypeTr
            : InspectionTypeTr + FormConstants.BilingualSeparator + InspectionTypeEn;

    public static InspectionItem CreateFromTemplate(InspectionTemplateItem template) => new()
    {
        RowNo = template.RowNo,
        IsFixed = true,
        InspectionTypeTr = template.InspectionTypeTr,
        InspectionTypeEn = template.InspectionTypeEn,
        AcceptCriteria = template.AcceptCriteria
    };

    public static InspectionItem CreateEmpty(int rowNo) => new()
    {
        RowNo = rowNo,
        IsFixed = false
    };
}
