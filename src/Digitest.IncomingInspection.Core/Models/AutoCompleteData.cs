namespace Digitest.IncomingInspection.Core.Models;

/// Daha önce girilmiş öneri değerler   açılır formda
public sealed class AutoCompleteData
{
    public IReadOnlyList<string> Suppliers { get; init; } = Array.Empty<string>();
    public IReadOnlyList<string> InventoryGroups { get; init; } = Array.Empty<string>();
    public IReadOnlyList<string> QuantityUnits { get; init; } = Array.Empty<string>();
    public IReadOnlyList<string> Inspectors { get; init; } = Array.Empty<string>();
}
