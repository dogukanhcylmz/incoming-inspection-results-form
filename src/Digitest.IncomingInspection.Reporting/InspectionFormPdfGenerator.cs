using System.Globalization;
using Digitest.IncomingInspection.Core;
using Digitest.IncomingInspection.Core.Enums;
using Digitest.IncomingInspection.Core.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Digitest.IncomingInspection.Reporting;


public sealed class InspectionFormPdfGenerator
{
    private const string FontFamily = "Arial";
    private const string LineColor = "#000000";
    private const string BandColor = "#BFBFBF";    // "Ürün Parça Bilgileri", tablo başlıkları
    private const string LabelColor = "#EDEDED";   // Etiket hücreleri
    private const float LineWidth = 0.6f;
    private const float CellPadding = 3f;

    private const float DocInfoRowHeight = 15f;
    private const float BandRowHeight = 20f;
    private const float InfoRowHeight = 24f;
    private const float ItemHeaderRowHeight = 18f;
    private const float ItemRowHeight = 24f;
    private const float NcrHeight = 42f;
    private const float SignatureHeaderHeight = 20f;
    private const float SignatureBodyHeight = 66f;

    private static readonly CultureInfo Turkish = CultureInfo.GetCultureInfo("tr-TR");

    private readonly byte[]? _logo;

    public InspectionFormPdfGenerator()
    {
        _logo = LoadEmbeddedLogo();
    }

    //PDF içeriğini üretiM
    public byte[] Generate(InspectionForm form)
    {
        ArgumentNullException.ThrowIfNull(form);

        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4.Landscape());
                page.Margin(18);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(style => style.FontFamily(FontFamily).FontSize(8).FontColor(Colors.Black));

                page.Content().ScaleToFit().Column(column =>
                {
                    column.Item().Element(c => ComposeHeader(c, form));
                    column.Item().Element(c => ComposeProductAndCommercialInfo(c, form));
                    column.Item().Element(c => ComposeInspectionItems(c, form));
                    column.Item().Element(c => ComposeNcr(c, form));
                    column.Item().Element(c => ComposeNotesAndSignature(c, form));
                });

                page.Footer().PaddingTop(3).Row(row =>
                {
                    row.RelativeItem().Text(text =>
                    {
                        text.Span($"Kayıt No: {form.FormNo}").FontSize(6.5f).FontColor(Colors.Grey.Darken1);
                    });

                    row.RelativeItem().Text(text =>
                    {
                        text.AlignRight();
                        text.Span($"Çıktı tarihi: {DateTime.Now.ToString("dd.MM.yyyy HH:mm", Turkish)}")
                            .FontSize(6.5f).FontColor(Colors.Grey.Darken1);
                    });
                });
            });
        });

        document = document.WithMetadata(new DocumentMetadata
        {
            Title = $"{form.DocumentNo} Girdi Muayene ve Sonuç Formu - {form.FormNo}",
            Subject = "Girdi Muayene ve Sonuç Formu / Incoming Inspection and Results Form",
            Author = "DIGITEST",
            Creator = "DIGITEST Girdi Muayene"
        });

        return document.GeneratePdf();
    }

    private void ComposeHeader(IContainer container, InspectionForm form)
    {
        container.Table(table =>
        {
            table.ColumnsDefinition(columns =>
            {
                columns.RelativeColumn(305);   // logo
                columns.RelativeColumn(1115);  // başlık
                columns.RelativeColumn(215);   // doküman etiketleri
                columns.RelativeColumn(205);   // doküman değerleri
            });

            table.Cell().Row(1).Column(1).RowSpan(4)
                .Element(c => Box(c))
                .Element(ComposeLogo);

            table.Cell().Row(1).Column(2).RowSpan(4)
                .Element(c => Box(c))
                .AlignMiddle()
                .Text(text =>
                {
                    text.AlignCenter();
                    text.Line("Girdi Muayene ve Sonuç Formu").FontSize(14).Bold();
                    text.Span("Incoming Inspection and Results Form").FontSize(12).Italic();
                });

            DocInfoRow(table, 1, "Doküman No", form.DocumentNo);
            DocInfoRow(table, 2, "Yayın Tarihi", FormatDate(form.DocPublishDate));
            DocInfoRow(table, 3, "Revizyon", form.DocRevision);
            DocInfoRow(table, 4, "Revizyon Tarihi", form.DocRevisionDate.HasValue ? FormatDate(form.DocRevisionDate.Value) : "-");
        });
    }

    private void ComposeLogo(IContainer container)
    {
        if (_logo is null)
            return;

        container.Padding(6).AlignCenter().AlignMiddle().MaxHeight(46).Image(_logo).FitArea();
    }

    private static void DocInfoRow(TableDescriptor table, uint row, string label, string? value)
    {
        table.Cell().Row(row).Column(3)
            .Element(c => Box(c, LabelColor, DocInfoRowHeight))
            .Element(c => CenteredText(c, label, 7.5f, bold: true));

        table.Cell().Row(row).Column(4)
            .Element(c => Box(c, null, DocInfoRowHeight))
            .Element(c => CenteredText(c, value, 7.5f));
    }

    private static void ComposeProductAndCommercialInfo(IContainer container, InspectionForm f)
    {
        container.Table(table =>
        {
            table.ColumnsDefinition(columns =>
            {
                columns.RelativeColumn(305);   //  1
                columns.RelativeColumn(297);   //  1
                columns.RelativeColumn(293);   //  2
                columns.RelativeColumn(292);   //  2
                columns.RelativeColumn(291);   
                columns.RelativeColumn(357);   
            });

            table.Cell().Row(1).Column(1).ColumnSpan(4)
                .Element(c => Box(c, BandColor, BandRowHeight))
                .Element(c => CenteredText(c, "Ürün Parça Bilgileri", 9, bold: true));

            table.Cell().Row(1).Column(5).ColumnSpan(2)
                .Element(c => Box(c, BandColor, BandRowHeight))
                .Element(c => CenteredText(c, "Ticari Bilgiler", 9, bold: true));

            Label(table, 2, 1, "Proje Numarası", "Project Number");
            Value(table, 2, 2, f.ProjectNo);
            Label(table, 2, 3, "Envanter Kodu", "Inventory Code");
            Value(table, 2, 4, f.InventoryCode);
            Label(table, 2, 5, "İrsaliye Numarası", "Waybill Number");
            Value(table, 2, 6, f.WaybillNo);

            Label(table, 3, 1, "Ürün/Parça Adı", "Product/Part Name");
            Value(table, 3, 2, f.ProductName);
            Label(table, 3, 3, "Gelen Miktar", "Incoming Quantity");
            Value(table, 3, 4, FormatQuantity(f.IncomingQuantity, f.QuantityUnit));
            Label(table, 3, 5, "Sipariş Emri Numarası", "PO Number");
            Value(table, 3, 6, f.PurchaseOrderNo);

            Label(table, 4, 1, "Lot/Parti/Seri No", "Lot/Batch/Serial No");
            Value(table, 4, 2, f.LotNo);
            Label(table, 4, 3, "Teknik Resim No", "Technical Drawing No");
            Value(table, 4, 4, f.TechnicalDrawingNo);
            Label(table, 4, 5, "Geliş Tarihi", "Arrival Date");
            Value(table, 4, 6, f.ArrivalDate.HasValue ? FormatDate(f.ArrivalDate.Value) : null);

            Label(table, 5, 1, "Muayene Tarihi", "Date of Inspection");
            Value(table, 5, 2, FormatDate(f.InspectionDate));
            Label(table, 5, 3, "Teknik Resim Rev", "Technical Drawing Rev");
            Value(table, 5, 4, f.TechnicalDrawingRev);
            Label(table, 5, 5, "Envanter Grubu", "Inventory Group");
            Value(table, 5, 6, f.InventoryGroup);

            Label(table, 6, 1, "Diğer", "Other");
            Value(table, 6, 2, f.Other, columnSpan: 3);
            Label(table, 6, 5, "Tedarikçi Adı", "Supplier Name");
            Value(table, 6, 6, f.SupplierName);
        });
    }

    private static void Label(TableDescriptor table, uint row, uint column, string tr, string en)
    {
        table.Cell().Row(row).Column(column)
            .Element(c => Box(c, LabelColor, InfoRowHeight))
            .Element(c => Bilingual(c, tr, en));
    }

    private static void Value(TableDescriptor table, uint row, uint column, string? value, uint columnSpan = 1)
    {
        table.Cell().Row(row).Column(column).ColumnSpan(columnSpan)
            .Element(c => Box(c, null, InfoRowHeight))
            .Element(c => CenteredText(c, value, 8.5f));
    }

    //  Muayene tablosu
   
    private static void ComposeInspectionItems(IContainer container, InspectionForm f)
    {
        var rows = Enumerable.Range(1, FormConstants.LineCount)
            .Select(n => f.Items.FirstOrDefault(i => i.RowNo == n) ?? InspectionItem.CreateEmpty(n))
            .ToList();

        container.Table(table =>
        {
            table.ColumnsDefinition(columns =>
            {
                columns.RelativeColumn(59);    // No
                columns.RelativeColumn(246);   // Muayene Tipi
                columns.RelativeColumn(294);   // Kabul Kriteri
                columns.RelativeColumn(291);   // Uygulanan
                columns.RelativeColumn(143);   // Kabul
                columns.RelativeColumn(147);   // Şartlı Kabul
                columns.RelativeColumn(146);   // Red
                columns.RelativeColumn(498);   // Açıklama
            });

            // ---- Başlık (2 satır) ----
            table.Cell().Row(1).Column(1).RowSpan(2)
                .Element(c => Box(c, BandColor, ItemHeaderRowHeight))
                .Element(c => CenteredText(c, "No", 8, bold: true));

            HeaderCell(table, 1, 2, "Muayene Tipi", "Type of Inspection", rowSpan: 2);
            HeaderCell(table, 1, 3, "Kabul Kriteri", "Accept. Criteria", rowSpan: 2);
            HeaderCell(table, 1, 4, "Uygulanan", "Applied", rowSpan: 2);

            table.Cell().Row(1).Column(5).ColumnSpan(3)
                .Element(c => Box(c, BandColor, ItemHeaderRowHeight))
                .Element(c => Bilingual(c, "Karar", "Decision"));

            HeaderCell(table, 1, 8, "Açıklama", "Statement", rowSpan: 2);

            HeaderCell(table, 2, 5, "Kabul", "Acc.");
            HeaderCell(table, 2, 6, "Şartlı Kabul", "Conditional Acc.");
            HeaderCell(table, 2, 7, "Red", "Rej.");

            // ---- Satırlar ----
            uint rowIndex = 3;
            foreach (var item in rows)
            {
                var r = rowIndex++;

                table.Cell().Row(r).Column(1)
                    .Element(c => Box(c, null, ItemRowHeight))
                    .Element(c => CenteredText(c, item.RowNo.ToString(Turkish), 8));

                table.Cell().Row(r).Column(2)
                    .Element(c => Box(c, null, ItemRowHeight))
                    .Element(c => Bilingual(c, item.InspectionTypeTr, item.InspectionTypeEn, 8.5f));

                table.Cell().Row(r).Column(3)
                    .Element(c => Box(c, null, ItemRowHeight))
                    .Element(c => CenteredText(c, item.AcceptCriteria, 8.5f));

                table.Cell().Row(r).Column(4)
                    .Element(c => Box(c, null, ItemRowHeight))
                    .Element(c => CenteredText(c, item.Applied, 8.5f));

                table.Cell().Row(r).Column(5)
                    .Element(c => Box(c, null, ItemRowHeight))
                    .Element(c => CheckBox(c, item.Decision == InspectionDecision.Accepted));

                table.Cell().Row(r).Column(6)
                    .Element(c => Box(c, null, ItemRowHeight))
                    .Element(c => CheckBox(c, item.Decision == InspectionDecision.ConditionalAccepted));

                table.Cell().Row(r).Column(7)
                    .Element(c => Box(c, null, ItemRowHeight))
                    .Element(c => CheckBox(c, item.Decision == InspectionDecision.Rejected));

                table.Cell().Row(r).Column(8)
                    .Element(c => Box(c, null, ItemRowHeight))
                    .AlignMiddle()
                    .Text(text => text.Span(item.Statement ?? string.Empty).FontSize(8));
            }
        });
    }

    private static void HeaderCell(TableDescriptor table, uint row, uint column, string tr, string en, uint rowSpan = 1)
    {
        table.Cell().Row(row).Column(column).RowSpan(rowSpan)
            .Element(c => Box(c, BandColor, ItemHeaderRowHeight))
            .Element(c => Bilingual(c, tr, en));
    }

    private static void ComposeNcr(IContainer container, InspectionForm f)
    {
        container
            .Border(LineWidth).BorderColor(LineColor)
            .MinHeight(NcrHeight)
            .Padding(CellPadding + 1)
            .Text(text =>
            {
                text.Span("NCR / NCR: ").Bold();
                text.Span(f.NcrText ?? string.Empty);
            });
    }

    private static void ComposeNotesAndSignature(IContainer container, InspectionForm f)
    {
        container.Table(table =>
        {
            table.ColumnsDefinition(columns =>
            {
                columns.RelativeColumn(884);   // Notlar
                columns.RelativeColumn(433);   // Adı Soyadı
                columns.RelativeColumn(490);   // İmza
            });

            table.Cell().Row(1).Column(1).RowSpan(2)
                .Element(c => Box(c))
                .Padding(1)
                .Text(text =>
                {
                    text.Span("NOT / ").Bold();
                    text.Span("Notes: ").Bold().Italic();
                    text.Span(f.Notes ?? string.Empty);
                });

            table.Cell().Row(1).Column(2)
                .Element(c => Box(c, null, SignatureHeaderHeight))
                .Element(c => Bilingual(c, "Adı Soyadı", "Name Surname"));

            table.Cell().Row(1).Column(3)
                .Element(c => Box(c, null, SignatureHeaderHeight))
                .Element(c => Bilingual(c, "İmza", "Signature"));

            table.Cell().Row(2).Column(2)
                .Element(c => Box(c, null, SignatureBodyHeight))
                .Element(c => CenteredText(c, f.InspectorName, 9));

            // imza için
            table.Cell().Row(2).Column(3)
                .Element(c => Box(c, null, SignatureBodyHeight));
        });
    }

    // =====================================================================
    // Yardımcılar
    // =====================================================================

    /// <summary>Kenarlıklı hücre kutusu: kenarlık, isteğe bağlı zemin rengi, en az yükseklik ve iç boşluk.</summary>
    private static IContainer Box(IContainer container, string? background = null, float minHeight = 0)
    {
        container = container.Border(LineWidth).BorderColor(LineColor);

        if (background is not null)
            container = container.Background(background);

        if (minHeight > 0)
            container = container.MinHeight(minHeight);

        return container.Padding(CellPadding);
    }

    /// <summary>Üstte kalın Türkçe, altta italik İngilizce iki satırlı başlık.</summary>
    private static void Bilingual(IContainer container, string? tr, string? en, float size = 8)
    {
        container.AlignMiddle().Text(text =>
        {
            text.AlignCenter();

            if (string.IsNullOrWhiteSpace(en))
            {
                text.Span(tr ?? string.Empty).FontSize(size).Bold();
                return;
            }

            text.Line(tr ?? string.Empty).FontSize(size).Bold();
            text.Span(en).FontSize(size - 1).Italic();
        });
    }

    private static void CenteredText(IContainer container, string? value, float size, bool bold = false)
    {
        container.AlignMiddle().Text(text =>
        {
            text.AlignCenter();
            var span = text.Span(value ?? string.Empty).FontSize(size);
            if (bold)
                span.Bold();
        });
    }

    /// <summary>Kağıt formdaki kare kutucuk; seçiliyse içine X basılır.</summary>
    private static void CheckBox(IContainer container, bool isChecked)
    {
        var box = container
            .AlignCenter()
            .AlignMiddle()
            .Width(9)
            .Height(9)
            .Border(0.8f)
            .BorderColor(LineColor)
            .Background(Colors.White);

        if (!isChecked)
            return;

        box.AlignCenter().AlignMiddle().Text(text =>
        {
            text.Span("X").FontSize(7.5f).Bold().LineHeight(1);
        });
    }

    private static string FormatDate(DateTime date) => date.ToString("dd.MM.yyyy", Turkish);

    private static string? FormatQuantity(decimal? quantity, string? unit)
    {
        if (quantity is null)
            return null;

        var number = quantity.Value.ToString("#,0.###", Turkish);
        return string.IsNullOrWhiteSpace(unit) ? number : $"{number} {unit}";
    }

    private static byte[]? LoadEmbeddedLogo()
    {
        using var stream = typeof(InspectionFormPdfGenerator).Assembly.GetManifestResourceStream("Digitest.Logo.png");
        if (stream is null)
            return null;

        using var memory = new MemoryStream();
        stream.CopyTo(memory);
        return memory.ToArray();
    }
}
