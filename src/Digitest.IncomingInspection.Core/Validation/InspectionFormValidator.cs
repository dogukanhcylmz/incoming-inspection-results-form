using Digitest.IncomingInspection.Core.Enums;
using Digitest.IncomingInspection.Core.Models;

namespace Digitest.IncomingInspection.Core.Validation;

public static class InspectionFormValidator
{
    public static IReadOnlyList<ValidationError> Validate(InspectionForm form)
    {
        ArgumentNullException.ThrowIfNull(form);

        var errors = new List<ValidationError>();

        Required(errors, form.ProductName, nameof(InspectionForm.ProductName), "Ürün/Parça Adı"); //zorunlu alans
        Required(errors, form.LotNo, nameof(InspectionForm.LotNo), "Lot/Parti/Seri No");
        Required(errors, form.SupplierName, nameof(InspectionForm.SupplierName), "Tedarikçi Adı");

        if (form.InspectionDate == default)
            errors.Add(new(nameof(InspectionForm.InspectionDate), "Muayene Tarihi girilmelidir."));

        Required(errors, form.DocumentNo, nameof(InspectionForm.DocumentNo), "Doküman No");
        Required(errors, form.DocRevision, nameof(InspectionForm.DocRevision), "Revizyon");

        if (form.DocPublishDate == default)
            errors.Add(new(nameof(InspectionForm.DocPublishDate), "Yayın Tarihi girilmelidir."));

        if (form.ArrivalDate is { } arrival && form.InspectionDate != default && arrival.Date > form.InspectionDate.Date)
            errors.Add(new(nameof(InspectionForm.ArrivalDate), "Geliş Tarihi, muayene Tarihinden sonra olamaz."));

        if (form.IncomingQuantity is < 0m)
            errors.Add(new(nameof(InspectionForm.IncomingQuantity), "Gelen miktar negatif olamaz."));

        MaxLength(errors, form.ProjectNo, FieldLimits.ProjectNo, nameof(InspectionForm.ProjectNo), "Proje Numarası");
        MaxLength(errors, form.ProductName, FieldLimits.ProductName, nameof(InspectionForm.ProductName), "Ürün/Parça Adı");
        MaxLength(errors, form.LotNo, FieldLimits.LotNo, nameof(InspectionForm.LotNo), "Lot/Parti/Seri No");
        MaxLength(errors, form.InventoryCode, FieldLimits.InventoryCode, nameof(InspectionForm.InventoryCode), "Envanter Kodu");
        MaxLength(errors, form.QuantityUnit, FieldLimits.QuantityUnit, nameof(InspectionForm.QuantityUnit), "Birim");
        MaxLength(errors, form.TechnicalDrawingNo, FieldLimits.TechnicalDrawingNo, nameof(InspectionForm.TechnicalDrawingNo), "Teknik Resim No");
        MaxLength(errors, form.TechnicalDrawingRev, FieldLimits.TechnicalDrawingRev, nameof(InspectionForm.TechnicalDrawingRev), "Teknik Resim Rev");
        MaxLength(errors, form.Other, FieldLimits.Other, nameof(InspectionForm.Other), "Diğer");
        MaxLength(errors, form.WaybillNo, FieldLimits.WaybillNo, nameof(InspectionForm.WaybillNo), "İrsaliye Numarası");
        MaxLength(errors, form.PurchaseOrderNo, FieldLimits.PurchaseOrderNo, nameof(InspectionForm.PurchaseOrderNo), "Sipariş Emri Numarası");
        MaxLength(errors, form.InventoryGroup, FieldLimits.InventoryGroup, nameof(InspectionForm.InventoryGroup), "Envanter Grubu");
        MaxLength(errors, form.SupplierName, FieldLimits.SupplierName, nameof(InspectionForm.SupplierName), "Tedarikçi Adı");
        MaxLength(errors, form.NcrText, FieldLimits.NcrText, nameof(InspectionForm.NcrText), "NCR");
        MaxLength(errors, form.Notes, FieldLimits.Notes, nameof(InspectionForm.Notes), "Notlar");
        MaxLength(errors, form.InspectorName, FieldLimits.InspectorName, nameof(InspectionForm.InspectorName), "Adı Soyadı");
        MaxLength(errors, form.DocumentNo, FieldLimits.DocumentNo, nameof(InspectionForm.DocumentNo), "Doküman No");
        MaxLength(errors, form.DocRevision, FieldLimits.DocRevision, nameof(InspectionForm.DocRevision), "Revizyon");

        foreach (var item in form.Items.OrderBy(i => i.RowNo))
            ValidateItem(errors, item);

        if (form.Items.Any(i => i.IsUsed && i.Decision == InspectionDecision.Rejected)
            && string.IsNullOrWhiteSpace(form.NcrText))
        {
            errors.Add(new(nameof(InspectionForm.NcrText),
                "Red kararı verilen satır var; NCR alanı doldurulmalıdır."));
        }

        return errors;
    }

    private static void ValidateItem(List<ValidationError> errors, InspectionItem item)
    {
        var prefix = $"{item.RowNo}. satır";

        if (item.RowNo is < 1 or > FormConstants.LineCount)
        {
            errors.Add(new(ValidationError.ItemField(item.RowNo, nameof(InspectionItem.RowNo)),
                $"Geçersiz satır numarası: {item.RowNo}."));
            return;
        }

        if (!item.IsFixed && string.IsNullOrWhiteSpace(item.InspectionTypeTr) && item.HasAnyInput)
        {
            errors.Add(new(ValidationError.ItemField(item.RowNo, "InspectionType"),
                $"{prefix}: bilgi girilen serbest satırda Muayene Tipi de yazılmalıdır."));
        }

        ItemMaxLength(errors, item, item.InspectionTypeTr, FieldLimits.InspectionType, "InspectionType", "Muayene Tipi (Türkçe)");
        ItemMaxLength(errors, item, item.InspectionTypeEn, FieldLimits.InspectionType, "InspectionType", "Muayene Tipi (İngilizce)");
        ItemMaxLength(errors, item, item.AcceptCriteria, FieldLimits.AcceptCriteria, nameof(InspectionItem.AcceptCriteria), "Kabul Kriteri");
        ItemMaxLength(errors, item, item.Applied, FieldLimits.Applied, nameof(InspectionItem.Applied), "Uygulanan");
        ItemMaxLength(errors, item, item.Statement, FieldLimits.Statement, nameof(InspectionItem.Statement), "Açıklama");
    }

    private static void Required(List<ValidationError> errors, string? value, string field, string caption)
    {
        if (string.IsNullOrWhiteSpace(value))
            errors.Add(new(field, $"{caption} alanı zorunludur."));
    }

    private static void MaxLength(List<ValidationError> errors, string? value, int max, string field, string caption)
    {
        if (value is not null && value.Length > max)
            errors.Add(new(field, $"{caption} en fazla {max} karakter olabilir."));
    }

    private static void ItemMaxLength(List<ValidationError> errors, InspectionItem item, string? value, int max, string property, string caption)
    {
        if (value is not null && value.Length > max)
            errors.Add(new(ValidationError.ItemField(item.RowNo, property),
                $"{item.RowNo}. satır: {caption} en fazla {max} karakter olabilir."));
    }
}
