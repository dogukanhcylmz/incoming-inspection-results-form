namespace Digitest.IncomingInspection.Core.Enums;

public static class EnumDisplay
{
    public static string ToDisplayText(this OverallResult result) => result switch
    {
        OverallResult.Pending => "Beklemede",
        OverallResult.Accepted => "Kabul",
        OverallResult.ConditionalAccepted => "Şartlı Kabul",
        OverallResult.Rejected => "Red",
        _ => result.ToString()
    };

    public static string ToDisplayText(this InspectionDecision decision) => decision switch
    {
        InspectionDecision.None => "Seçilmedi",
        InspectionDecision.Accepted => "Kabul",
        InspectionDecision.ConditionalAccepted => "Şartlı Kabul",
        InspectionDecision.Rejected => "Red",
        _ => decision.ToString()
    };
}
