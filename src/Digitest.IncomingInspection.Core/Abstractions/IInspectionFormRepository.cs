using Digitest.IncomingInspection.Core.Models;

namespace Digitest.IncomingInspection.Core.Abstractions;

public interface IInspectionFormRepository
{
    Task<IReadOnlyList<InspectionFormListItem>> SearchAsync(InspectionFormFilter filter, CancellationToken cancellationToken = default);

    ///Bulunamazsa null döner
    Task<InspectionForm?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    ///Yeni formu kayded
    Task<int> InsertAsync(InspectionForm form, CancellationToken cancellationToken = default);

    Task UpdateAsync(InspectionForm form, CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);

    Task<AutoCompleteData> GetAutoCompleteDataAsync(CancellationToken cancellationToken = default);
}
