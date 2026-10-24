using Digitest.IncomingInspection.Core.Models;

namespace Digitest.IncomingInspection.Core.Abstractions;

public interface IInspectionFormRepository
{
    Task<IReadOnlyList<InspectionFormListItem>> SearchAsync(InspectionFormFilter filter, CancellationToken cancellationToken = default);

    /// <summary>Formu satırlarıyla birlikte getirir. Bulunamazsa null döner.</summary>
    Task<InspectionForm?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>Yeni formu kaydeder; kayıt numarasını (FormNo) üretir ve form nesnesine yazar. Yeni Id'yi döner.</summary>
    Task<int> InsertAsync(InspectionForm form, CancellationToken cancellationToken = default);

    /// <summary>Var olan formu ve satırlarını günceller.</summary>
    Task UpdateAsync(InspectionForm form, CancellationToken cancellationToken = default);

    /// <summary>Formu ve satırlarını siler. Kayıt bulunamazsa false döner.</summary>
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);

    Task<AutoCompleteData> GetAutoCompleteDataAsync(CancellationToken cancellationToken = default);
}
