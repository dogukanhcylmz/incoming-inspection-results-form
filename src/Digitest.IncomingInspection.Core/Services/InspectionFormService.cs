using Digitest.IncomingInspection.Core.Abstractions;
using Digitest.IncomingInspection.Core.Models;
using Digitest.IncomingInspection.Core.Validation;

namespace Digitest.IncomingInspection.Core.Services;

/// Form iş akışı yeni form oluşturma- okuma- doğrulayıp kaydetme.
/// Ekranlar bu servis aracaılığıyla veritabanına yazacak

public sealed class InspectionFormService
{
    private readonly IInspectionFormRepository _forms;
    private readonly IInspectionTemplateRepository _templates;
    private readonly IDocumentInfoRepository _documentInfo;

    public InspectionFormService(
        IInspectionFormRepository forms,
        IInspectionTemplateRepository templates,
        IDocumentInfoRepository documentInfo)
    {
        _forms = forms ?? throw new ArgumentNullException(nameof(forms));
        _templates = templates ?? throw new ArgumentNullException(nameof(templates));
        _documentInfo = documentInfo ?? throw new ArgumentNullException(nameof(documentInfo));
    }

    public async Task<InspectionForm> CreateNewAsync(CancellationToken cancellationToken = default)
    {
        var info = await _documentInfo.GetAsync(cancellationToken).ConfigureAwait(false);
        var templates = await _templates.GetActiveAsync(cancellationToken).ConfigureAwait(false);

        var form = new InspectionForm
        {
            InspectionDate = DateTime.Today,
            DocumentNo = info.DocumentNo,
            DocPublishDate = info.PublishDate,
            DocRevision = info.Revision,
            DocRevisionDate = info.RevisionDate
        };

        for (var rowNo = 1; rowNo <= FormConstants.LineCount; rowNo++)
        {
            var template = templates.FirstOrDefault(t => t.RowNo == rowNo);
            form.Items.Add(template is null
                ? InspectionItem.CreateEmpty(rowNo)
                : InspectionItem.CreateFromTemplate(template));
        }

        return form;
    }

    public async Task<InspectionForm?> GetAsync(int id, CancellationToken cancellationToken = default)// kayıtlı gfrom getirme
    {
        var form = await _forms.GetByIdAsync(id, cancellationToken).ConfigureAwait(false);
        form?.EnsureAllRows(); //7
        return form;
    }

    public async Task<SaveResult> SaveAsync(InspectionForm form, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(form);

        Normalize(form);

        var errors = InspectionFormValidator.Validate(form);
        if (errors.Count > 0)
            return SaveResult.Invalid(errors);

        form.OverallResult = form.CalculateOverallResult();

        var itemsToSave = form.Items.Where(i => i.IsUsed).OrderBy(i => i.RowNo).ToList();
        var allItems = form.Items;
        form.Items = itemsToSave;

        try
        {
            if (form.IsNew)
                form.Id = await _forms.InsertAsync(form, cancellationToken).ConfigureAwait(false);
            else
                await _forms.UpdateAsync(form, cancellationToken).ConfigureAwait(false);
        }
        catch
        {
            form.Items = allItems;
            throw;
        }

        var saved = await GetAsync(form.Id, cancellationToken).ConfigureAwait(false)
                    ?? throw new InvalidOperationException("Kaydedilen form veritabanından okunamadı.");

        return SaveResult.Ok(saved);
    }

    public Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
        => _forms.DeleteAsync(id, cancellationToken);

    private static void Normalize(InspectionForm form)/// boş bırakılanlar kırpılırı
    {
        form.ProjectNo = Clean(form.ProjectNo);
        form.ProductName = Clean(form.ProductName) ?? string.Empty;
        form.LotNo = Clean(form.LotNo) ?? string.Empty;
        form.InventoryCode = Clean(form.InventoryCode);
        form.QuantityUnit = Clean(form.QuantityUnit);
        form.TechnicalDrawingNo = Clean(form.TechnicalDrawingNo);
        form.TechnicalDrawingRev = Clean(form.TechnicalDrawingRev);
        form.Other = Clean(form.Other);

        form.WaybillNo = Clean(form.WaybillNo);
        form.PurchaseOrderNo = Clean(form.PurchaseOrderNo);
        form.InventoryGroup = Clean(form.InventoryGroup);
        form.SupplierName = Clean(form.SupplierName) ?? string.Empty;

        form.NcrText = Clean(form.NcrText);
        form.Notes = Clean(form.Notes);
        form.InspectorName = Clean(form.InspectorName);

        form.DocumentNo = Clean(form.DocumentNo) ?? string.Empty;
        form.DocRevision = Clean(form.DocRevision) ?? string.Empty;

        form.InspectionDate = form.InspectionDate.Date;
        form.ArrivalDate = form.ArrivalDate?.Date;
        form.DocPublishDate = form.DocPublishDate.Date;
        form.DocRevisionDate = form.DocRevisionDate?.Date;

        foreach (var item in form.Items)
        {
            item.InspectionTypeTr = Clean(item.InspectionTypeTr) ?? string.Empty;
            item.InspectionTypeEn = Clean(item.InspectionTypeEn);
            item.AcceptCriteria = Clean(item.AcceptCriteria);
            item.Applied = Clean(item.Applied);
            item.Statement = Clean(item.Statement);
        }
    }

    private static string? Clean(string? value) =>
        string.IsNullOrWhiteSpace(value) ? null : value.Trim();
}
