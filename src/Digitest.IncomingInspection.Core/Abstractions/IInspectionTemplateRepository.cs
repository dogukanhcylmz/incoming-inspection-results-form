using Digitest.IncomingInspection.Core.Models;

namespace Digitest.IncomingInspection.Core.Abstractions;

public interface IInspectionTemplateRepository
{
    /// <summary>Aktif sabit satırları satır numarasına göre sıralı getirir.</summary>
    Task<IReadOnlyList<InspectionTemplateItem>> GetActiveAsync(CancellationToken cancellationToken = default);
}
