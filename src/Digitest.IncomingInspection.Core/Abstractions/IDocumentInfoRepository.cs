using Digitest.IncomingInspection.Core.Models;

namespace Digitest.IncomingInspection.Core.Abstractions;

public interface IDocumentInfoRepository
{
    Task<FormDocumentInfo> GetAsync(CancellationToken cancellationToken = default);

    Task SaveAsync(FormDocumentInfo info, CancellationToken cancellationToken = default);
}
