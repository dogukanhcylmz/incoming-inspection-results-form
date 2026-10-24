using Digitest.IncomingInspection.Core.Enums;

namespace Digitest.IncomingInspection.WinForms.Infrastructure;

/// <summary>
/// Kabul / Şartlı Kabul / Red için ekranda kullanılan renkler.
/// </summary>
internal static class ResultColors
{
    private static readonly Color AcceptedBack = Color.FromArgb(226, 240, 217);
    private static readonly Color AcceptedFore = Color.FromArgb(55, 86, 35);
    private static readonly Color ConditionalBack = Color.FromArgb(255, 242, 204);
    private static readonly Color ConditionalFore = Color.FromArgb(127, 96, 0);
    private static readonly Color RejectedBack = Color.FromArgb(248, 215, 218);
    private static readonly Color RejectedFore = Color.FromArgb(132, 32, 41);
    private static readonly Color PendingBack = Color.FromArgb(237, 237, 237);
    private static readonly Color PendingFore = Color.FromArgb(64, 64, 64);

    public static Color Back(OverallResult result) => result switch
    {
        OverallResult.Accepted => AcceptedBack,
        OverallResult.ConditionalAccepted => ConditionalBack,
        OverallResult.Rejected => RejectedBack,
        _ => PendingBack
    };

    public static Color Fore(OverallResult result) => result switch
    {
        OverallResult.Accepted => AcceptedFore,
        OverallResult.ConditionalAccepted => ConditionalFore,
        OverallResult.Rejected => RejectedFore,
        _ => PendingFore
    };

    /// <summary>Muayene satırı zemin rengi; karar seçilmemişse beyaz.</summary>
    public static Color Back(InspectionDecision decision) => decision switch
    {
        InspectionDecision.Accepted => AcceptedBack,
        InspectionDecision.ConditionalAccepted => ConditionalBack,
        InspectionDecision.Rejected => RejectedBack,
        _ => Color.White
    };
}
