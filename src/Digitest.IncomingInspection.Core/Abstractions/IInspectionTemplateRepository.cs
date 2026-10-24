using Digitest.IncomingInspection.Core.Models;

namespace Digitest.IncomingInspection.Core.Abstractions;

public interface IInspectionTemplateRepository
{
    Task<IReadOnlyList<InspectionTemplateItem>> GetActiveAsync(CancellationToken cancellationToken = default);
}
