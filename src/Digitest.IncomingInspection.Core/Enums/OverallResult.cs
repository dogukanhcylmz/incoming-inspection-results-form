namespace Digitest.IncomingInspection.Core.Enums;

/// Veritabanında TINYINT
public enum OverallResult : byte
{
    Pending = 0,
    Accepted = 1,
    ConditionalAccepted = 2,
    Rejected = 3
}
