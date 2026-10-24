using System.Globalization;
using System.Text.RegularExpressions;
using Digitest.IncomingInspection.Core;
using Digitest.IncomingInspection.Core.Enums;
using Digitest.IncomingInspection.Core.Models;
using Digitest.IncomingInspection.Core.Validation;
using Digitest.IncomingInspection.WinForms.Infrastructure;

namespace Digitest.IncomingInspection.WinForms.Forms;

/// <summary>
/// Girdi Muayene ve Sonuç Formu giriş ekranı. Yeni kayıt açar veya var olan kaydı dolu getirir;
/// kaydeder ve PDF çıktısı alır.
/// </summary>
public partial class InspectionEditForm : Form
{
    private static readonly Regex ItemFieldPattern = new(@"^Items\[(\d+)\]", RegexOptions.CultureInvariant);
    private static readonly Color FixedTypeBackColor = Color.FromArgb(245, 245, 245);

    private readonly AppServices _services;
    private readonly int? _formId;
    private readonly DataGridViewCheckBoxColumn[] _decisionColumns;

    private InspectionForm _form = new();
    private bool _isBinding;
    private bool _isDirty;
    private bool _isBusy;
    private bool _suppressDecisionSync;
    private DateTime? _lastSavedAt;

    internal InspectionEditForm(AppServices services, int? formId)
    {
        _services = services ?? throw new ArgumentNullException(nameof(services));
        _formId = formId;

        InitializeComponent();

        _decisionColumns = new[] { colAccepted, colConditional, colRejected };

        Icon = AppAssets.AppIcon ?? Icon;
        picLogo.Image = AppAssets.Logo;

        ApplyFieldLimits();
        WireDirtyTracking();
        SetupToolTips();
    }

    /// <summary>Bu ekranda en az bir kez kayıt yapıldı mı? (liste yenilensin diye)</summary>
    public bool DataChanged { get; private set; }

    /// <summary>Kaydedilmiş formun Id'si; hiç kaydedilmediyse null.</summary>
    public int? SavedFormId => _form.IsNew ? null : _form.Id;

    // ==================================================================
    // Yaşam döngüsü
    // ==================================================================

    protected override async void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        await LoadDataAsync();
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        if (_isBusy)
        {
            e.Cancel = true;
            return;
        }

        if (_isDirty && e.CloseReason == CloseReason.UserClosing)
        {
            var answer = MessageBox.Show(this,
                "Kaydedilmemiş değişiklikler var.\n\nKaydetmeden kapatmak istiyor musunuz?",
                UiHelper.AppTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

            if (answer != DialogResult.Yes)
                e.Cancel = true;
        }

        base.OnFormClosing(e);
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
        switch (keyData)
        {
            case Keys.Control | Keys.S:
                btnSave.PerformClick();
                return true;

            case Keys.Control | Keys.P:
                btnPrint.PerformClick();
                return true;
        }

        return base.ProcessCmdKey(ref msg, keyData);
    }

    private async Task LoadDataAsync()
    {
        var failed = false;

        SetBusy(true);
        try
        {
            var autoCompleteTask = _services.Forms.GetAutoCompleteDataAsync();

            InspectionForm? form;
            if (_formId is int id)
                form = await _services.FormService.GetAsync(id);
            else
                form = await _services.FormService.CreateNewAsync();

            ApplyAutoComplete(await autoCompleteTask);

            if (form is null)
            {
                UiHelper.Warn(this, "Kayıt bulunamadı. Başka bir oturumda silinmiş olabilir.");
                failed = true;
            }
            else
            {
                BindForm(form);
            }
        }
        catch (Exception ex)
        {
            UiHelper.ShowError(this, ex, "Form yüklenemedi.");
            failed = true;
        }
        finally
        {
            SetBusy(false);
        }

        if (failed)
            Close();
        else
            ActiveControl = _form.IsNew ? txtProjectNo : (Control)dgvItems;
    }

    // ==================================================================
    // Ekran <-> model
    // ==================================================================

    private void BindForm(InspectionForm form)
    {
        _isBinding = true;
        try
        {
            _form = form;

            txtProjectNo.Text = form.ProjectNo ?? string.Empty;
            txtProductName.Text = form.ProductName;
            txtLotNo.Text = form.LotNo;
            dtpInspectionDate.Value = ClampDate(dtpInspectionDate, form.InspectionDate);
            txtInventoryCode.Text = form.InventoryCode ?? string.Empty;
            txtIncomingQty.Text = form.IncomingQuantity?.ToString("0.###", CultureInfo.CurrentCulture) ?? string.Empty;
            cmbUnit.Text = form.QuantityUnit ?? string.Empty;
            txtDrawingNo.Text = form.TechnicalDrawingNo ?? string.Empty;
            txtDrawingRev.Text = form.TechnicalDrawingRev ?? string.Empty;
            txtOther.Text = form.Other ?? string.Empty;

            txtWaybillNo.Text = form.WaybillNo ?? string.Empty;
            txtPoNo.Text = form.PurchaseOrderNo ?? string.Empty;
            if (form.ArrivalDate is DateTime arrival)
            {
                dtpArrivalDate.Value = ClampDate(dtpArrivalDate, arrival);
                dtpArrivalDate.Checked = true;
            }
            else
            {
                dtpArrivalDate.Value = DateTime.Today;
                dtpArrivalDate.Checked = false;
            }
            cmbInventoryGroup.Text = form.InventoryGroup ?? string.Empty;
            cmbSupplier.Text = form.SupplierName;

            txtNcr.Text = form.NcrText ?? string.Empty;
            txtNotes.Text = form.Notes ?? string.Empty;
            cmbInspector.Text = form.InspectorName ?? string.Empty;

            txtDocumentNo.Text = form.DocumentNo;
            dtpDocPublishDate.Value = ClampDate(dtpDocPublishDate,
                form.DocPublishDate == default ? DateTime.Today : form.DocPublishDate);
            txtDocRevision.Text = form.DocRevision;
            if (form.DocRevisionDate is DateTime revisionDate)
            {
                dtpDocRevisionDate.Value = ClampDate(dtpDocRevisionDate, revisionDate);
                dtpDocRevisionDate.Checked = true;
            }
            else
            {
                dtpDocRevisionDate.Value = DateTime.Today;
                dtpDocRevisionDate.Checked = false;
            }

            BindItems(form.Items);
            ClearErrors();
        }
        finally
        {
            _isBinding = false;
        }

        _isDirty = false;
        UpdateTitle();
        UpdateStatus();
    }

    private void BindItems(IEnumerable<InspectionItem> items)
    {
        dgvItems.EndEdit();
        dgvItems.CurrentCell = null;
        dgvItems.Rows.Clear();

        foreach (var item in items.OrderBy(i => i.RowNo))
        {
            var index = dgvItems.Rows.Add();
            var row = dgvItems.Rows[index];
            row.Tag = item;

            row.Cells[colRowNo.Index].Value = item.RowNo;
            row.Cells[colInspectionType.Index].Value = item.InspectionTypeDisplay;
            row.Cells[colAcceptCriteria.Index].Value = item.AcceptCriteria;
            row.Cells[colApplied.Index].Value = item.Applied;
            row.Cells[colAccepted.Index].Value = item.Decision == InspectionDecision.Accepted;
            row.Cells[colConditional.Index].Value = item.Decision == InspectionDecision.ConditionalAccepted;
            row.Cells[colRejected.Index].Value = item.Decision == InspectionDecision.Rejected;
            row.Cells[colStatement.Index].Value = item.Statement;

            var typeCell = row.Cells[colInspectionType.Index];
            typeCell.ReadOnly = item.IsFixed;
            if (item.IsFixed)
            {
                typeCell.Style.BackColor = FixedTypeBackColor;
                typeCell.ToolTipText = "Sabit satır (şablondan gelir).";
            }
            else
            {
                typeCell.ToolTipText = "Serbest satır: muayene tipini \"Türkçe / English\" biçiminde yazabilirsiniz.";
            }

            UpdateRowStyle(row);
        }
    }

    /// <summary>Ekrandaki değerleri forma aktarır. Miktar önceden ayrıştırılmış olarak verilir.</summary>
    private InspectionForm ReadFromControls(decimal? incomingQuantity)
    {
        var form = _form;

        form.ProjectNo = txtProjectNo.Text;
        form.ProductName = txtProductName.Text;
        form.LotNo = txtLotNo.Text;
        form.InspectionDate = dtpInspectionDate.Value.Date;
        form.InventoryCode = txtInventoryCode.Text;
        form.IncomingQuantity = incomingQuantity;
        form.QuantityUnit = cmbUnit.Text;
        form.TechnicalDrawingNo = txtDrawingNo.Text;
        form.TechnicalDrawingRev = txtDrawingRev.Text;
        form.Other = txtOther.Text;

        form.WaybillNo = txtWaybillNo.Text;
        form.PurchaseOrderNo = txtPoNo.Text;
        form.ArrivalDate = dtpArrivalDate.Checked ? dtpArrivalDate.Value.Date : null;
        form.InventoryGroup = cmbInventoryGroup.Text;
        form.SupplierName = cmbSupplier.Text;

        form.NcrText = txtNcr.Text;
        form.Notes = txtNotes.Text;
        form.InspectorName = cmbInspector.Text;

        form.DocumentNo = txtDocumentNo.Text;
        form.DocPublishDate = dtpDocPublishDate.Value.Date;
        form.DocRevision = txtDocRevision.Text;
        form.DocRevisionDate = dtpDocRevisionDate.Checked ? dtpDocRevisionDate.Value.Date : null;

        form.Items = ReadItems();
        return form;
    }

    private List<InspectionItem> ReadItems()
    {
        var items = new List<InspectionItem>();

        foreach (DataGridViewRow row in dgvItems.Rows)
        {
            if (row.Tag is not InspectionItem source)
                continue;

            var item = new InspectionItem
            {
                RowNo = source.RowNo,
                IsFixed = source.IsFixed,
                InspectionTypeTr = source.InspectionTypeTr,
                InspectionTypeEn = source.InspectionTypeEn,
                AcceptCriteria = CellText(row, colAcceptCriteria),
                Applied = CellText(row, colApplied),
                Statement = CellText(row, colStatement),
                Decision = GetDecision(row)
            };

            if (!source.IsFixed)
            {
                var (tr, en) = SplitBilingual(CellText(row, colInspectionType));
                item.InspectionTypeTr = tr;
                item.InspectionTypeEn = en;
            }

            items.Add(item);
        }

        return items;
    }

    private static string? CellText(DataGridViewRow row, DataGridViewColumn column) =>
        row.Cells[column.Index].Value?.ToString();

    private InspectionDecision GetDecision(DataGridViewRow row)
    {
        if (row.Cells[colAccepted.Index].Value is true)
            return InspectionDecision.Accepted;
        if (row.Cells[colConditional.Index].Value is true)
            return InspectionDecision.ConditionalAccepted;
        if (row.Cells[colRejected.Index].Value is true)
            return InspectionDecision.Rejected;

        return InspectionDecision.None;
    }

    /// <summary>"Ölçüm / Measurement" -> ("Ölçüm", "Measurement")</summary>
    private static (string Tr, string? En) SplitBilingual(string? text)
    {
        var value = (text ?? string.Empty).Trim();
        var index = value.IndexOf(FormConstants.BilingualSeparator, StringComparison.Ordinal);

        if (index <= 0)
            return (value, null);

        var tr = value[..index].Trim();
        var en = value[(index + FormConstants.BilingualSeparator.Length)..].Trim();
        return (tr, en.Length == 0 ? null : en);
    }

    // ==================================================================
    // Kaydet / PDF
    // ==================================================================

    private async Task<bool> SaveAsync()
    {
        if (_isBusy)
            return false;

        dgvItems.EndEdit();
        ClearErrors();

        if (!TryParseQuantity(txtIncomingQty.Text, out var quantity))
        {
            errorProvider.SetError(tlpQuantity, "Geçerli bir sayı girin (ör. 150 veya 12,5).");
            UiHelper.Warn(this, "Gelen Miktar alanına geçerli bir sayı girin (ör. 150 veya 12,5).");
            txtIncomingQty.Focus();
            return false;
        }

        var form = ReadFromControls(quantity);

        SetBusy(true);
        try
        {
            var result = await _services.FormService.SaveAsync(form);
            if (!result.Success)
            {
                ShowValidationErrors(result.Errors);
                return false;
            }

            _lastSavedAt = DateTime.Now;
            DataChanged = true;
            BindForm(result.Form);
            return true;
        }
        catch (Exception ex)
        {
            UiHelper.ShowError(this, ex, "Kayıt sırasında hata oluştu.");
            return false;
        }
        finally
        {
            SetBusy(false);
        }
    }

    private async Task SaveAndExportPdfAsync()
    {
        if (_isBusy)
            return;

        // Değişiklik yoksa ve kayıt zaten varsa yeniden kaydetmeye gerek yok.
        if ((_isDirty || _form.IsNew) && !await SaveAsync())
            return;

        if (_form.OverallResult == OverallResult.Pending &&
            !UiHelper.Confirm(this, "Bazı muayene satırlarında karar seçilmemiş.\n\nPDF yine de oluşturulsun mu?"))
        {
            return;
        }

        string path;
        SetBusy(true);
        try
        {
            var form = _form;
            path = await Task.Run(() => _services.PdfExporter.Export(form));
        }
        catch (Exception ex)
        {
            UiHelper.ShowError(this, ex, "PDF oluşturulamadı.");
            return;
        }
        finally
        {
            SetBusy(false);
        }

        if (_services.Settings.OpenPdfAfterCreate)
        {
            try
            {
                UiHelper.OpenWithShell(path);
            }
            catch (Exception ex)
            {
                UiHelper.ShowError(this, ex, $"PDF oluşturuldu ancak açılamadı:\n{path}");
            }
        }
        else
        {
            UiHelper.Info(this, $"PDF oluşturuldu:\n{path}");
        }
    }

    /// <summary>
    /// Miktarı ayrıştırır. Ondalık ayırıcı olarak virgül veya nokta kabul edilir (12,5 = 12.5);
    /// binlik ayırıcı kabul edilmez (1.500 gibi girişler belirsiz olduğu için tek ayırıcıya izin verilir).
    /// Boş metin geçerlidir (miktar girilmemiş).
    /// </summary>
    private static bool TryParseQuantity(string text, out decimal? quantity)
    {
        quantity = null;
        var value = text.Trim().Replace(" ", string.Empty, StringComparison.Ordinal);

        if (value.Length == 0)
            return true;

        value = value.Replace(',', '.');
        if (value.Count(ch => ch == '.') > 1)
            return false;

        const NumberStyles styles = NumberStyles.AllowDecimalPoint;
        if (!decimal.TryParse(value, styles, CultureInfo.InvariantCulture, out var parsed))
            return false;

        quantity = parsed;
        return true;
    }

    // ==================================================================
    // Doğrulama hataları
    // ==================================================================

    private void ShowValidationErrors(IReadOnlyList<ValidationError> errors)
    {
        var controls = new Dictionary<string, Control>(StringComparer.Ordinal)
        {
            [nameof(InspectionForm.ProjectNo)] = txtProjectNo,
            [nameof(InspectionForm.ProductName)] = txtProductName,
            [nameof(InspectionForm.LotNo)] = txtLotNo,
            [nameof(InspectionForm.InspectionDate)] = dtpInspectionDate,
            [nameof(InspectionForm.InventoryCode)] = txtInventoryCode,
            [nameof(InspectionForm.IncomingQuantity)] = tlpQuantity,
            [nameof(InspectionForm.QuantityUnit)] = tlpQuantity,
            [nameof(InspectionForm.TechnicalDrawingNo)] = txtDrawingNo,
            [nameof(InspectionForm.TechnicalDrawingRev)] = txtDrawingRev,
            [nameof(InspectionForm.Other)] = txtOther,
            [nameof(InspectionForm.WaybillNo)] = txtWaybillNo,
            [nameof(InspectionForm.PurchaseOrderNo)] = txtPoNo,
            [nameof(InspectionForm.ArrivalDate)] = dtpArrivalDate,
            [nameof(InspectionForm.InventoryGroup)] = cmbInventoryGroup,
            [nameof(InspectionForm.SupplierName)] = cmbSupplier,
            [nameof(InspectionForm.NcrText)] = txtNcr,
            [nameof(InspectionForm.Notes)] = txtNotes,
            [nameof(InspectionForm.InspectorName)] = cmbInspector,
            [nameof(InspectionForm.DocumentNo)] = txtDocumentNo,
            [nameof(InspectionForm.DocPublishDate)] = dtpDocPublishDate,
            [nameof(InspectionForm.DocRevision)] = txtDocRevision,
            [nameof(InspectionForm.DocRevisionDate)] = dtpDocRevisionDate
        };

        Control? firstControl = null;

        foreach (var error in errors)
        {
            var match = ItemFieldPattern.Match(error.Field);
            if (match.Success && int.TryParse(match.Groups[1].Value, out var rowNo))
            {
                MarkItemError(rowNo, error.Message);
                firstControl ??= dgvItems;
                continue;
            }

            if (controls.TryGetValue(error.Field, out var control))
            {
                var existing = errorProvider.GetError(control);
                errorProvider.SetError(control, string.IsNullOrEmpty(existing) ? error.Message : existing + "\n" + error.Message);
                firstControl ??= control;
            }
        }

        UiHelper.Warn(this,
            "Kayıt yapılamadı. Lütfen aşağıdaki alanları kontrol edin:\n\n• " +
            string.Join("\n• ", errors.Select(e => e.Message)));

        firstControl?.Focus();
    }

    private void MarkItemError(int rowNo, string message)
    {
        foreach (DataGridViewRow row in dgvItems.Rows)
        {
            if (row.Tag is InspectionItem item && item.RowNo == rowNo)
            {
                row.Cells[colInspectionType.Index].ErrorText = message;
                return;
            }
        }
    }

    private void ClearErrors()
    {
        errorProvider.Clear();

        foreach (DataGridViewRow row in dgvItems.Rows)
        {
            foreach (DataGridViewCell cell in row.Cells)
                cell.ErrorText = string.Empty;
        }
    }

    // ==================================================================
    // Görünüm yardımcıları
    // ==================================================================

    private void UpdateTitle()
    {
        var name = _form.IsNew ? "Yeni Girdi Muayene Formu" : $"{_form.FormNo} - Girdi Muayene Formu";
        Text = _isDirty ? name + " *" : name;
    }

    private void UpdateStatus()
    {
        // Genel sonuç ekrandaki güncel kararlardan anlık hesaplanır.
        var preview = new InspectionForm { Items = ReadItems() }.CalculateOverallResult();

        var prefix = _form.IsNew ? "Yeni kayıt (henüz kaydedilmedi)" : $"Kayıt No: {_form.FormNo}";
        var saved = _lastSavedAt is { } at ? $"   ·   Kaydedildi {at:HH:mm}" : string.Empty;

        lblStatus.Text = $"{prefix}   ·   Genel Sonuç: {preview.ToDisplayText()}{saved}";
        lblStatus.ForeColor = ResultColors.Fore(preview);
    }

    private void UpdateRowStyle(DataGridViewRow row)
    {
        var decision = GetDecision(row);
        var back = ResultColors.Back(decision);

        foreach (var column in _decisionColumns)
            row.Cells[column.Index].Style.BackColor = back;
    }

    private void SetBusy(bool busy)
    {
        _isBusy = busy;
        UseWaitCursor = busy;
        btnSave.Enabled = !busy;
        btnPrint.Enabled = !busy;
        btnClose.Enabled = !busy;
        tlpInfo.Enabled = !busy;
        grpItems.Enabled = !busy;
        tlpBottom.Enabled = !busy;
    }

    private void MarkDirty()
    {
        if (_isBinding || _isDirty)
            return;

        _isDirty = true;
        UpdateTitle();
    }

    private static DateTime ClampDate(DateTimePicker picker, DateTime value)
    {
        if (value < picker.MinDate)
            return picker.MinDate;

        return value > picker.MaxDate ? picker.MaxDate : value;
    }

    private void ApplyAutoComplete(AutoCompleteData data)
    {
        FillComboItems(cmbSupplier, data.Suppliers);
        FillComboItems(cmbInventoryGroup, data.InventoryGroups);
        FillComboItems(cmbInspector, data.Inspectors);
        FillComboItems(cmbUnit, data.QuantityUnits);
    }

    /// <summary>Var olan öğeleri koruyarak eksik değerleri ekler (büyük/küçük harf duyarsız).</summary>
    private static void FillComboItems(ComboBox combo, IEnumerable<string> values)
    {
        var existing = new HashSet<string>(combo.Items.Cast<object>().Select(o => o.ToString() ?? string.Empty),
            StringComparer.CurrentCultureIgnoreCase);

        combo.BeginUpdate();
        try
        {
            foreach (var value in values)
            {
                if (!string.IsNullOrWhiteSpace(value) && existing.Add(value))
                    combo.Items.Add(value);
            }
        }
        finally
        {
            combo.EndUpdate();
        }
    }

    private void ApplyFieldLimits()
    {
        txtProjectNo.MaxLength = FieldLimits.ProjectNo;
        txtProductName.MaxLength = FieldLimits.ProductName;
        txtLotNo.MaxLength = FieldLimits.LotNo;
        txtInventoryCode.MaxLength = FieldLimits.InventoryCode;
        txtIncomingQty.MaxLength = 20;
        cmbUnit.MaxLength = FieldLimits.QuantityUnit;
        txtDrawingNo.MaxLength = FieldLimits.TechnicalDrawingNo;
        txtDrawingRev.MaxLength = FieldLimits.TechnicalDrawingRev;
        txtOther.MaxLength = FieldLimits.Other;

        txtWaybillNo.MaxLength = FieldLimits.WaybillNo;
        txtPoNo.MaxLength = FieldLimits.PurchaseOrderNo;
        cmbInventoryGroup.MaxLength = FieldLimits.InventoryGroup;
        cmbSupplier.MaxLength = FieldLimits.SupplierName;

        txtNcr.MaxLength = FieldLimits.NcrText;
        txtNotes.MaxLength = FieldLimits.Notes;
        cmbInspector.MaxLength = FieldLimits.InspectorName;

        txtDocumentNo.MaxLength = FieldLimits.DocumentNo;
        txtDocRevision.MaxLength = FieldLimits.DocRevision;

        // Serbest satırda "Türkçe / English" yazılabildiği için iki alanın toplamı kadar.
        colInspectionType.MaxInputLength = FieldLimits.InspectionType * 2 + FormConstants.BilingualSeparator.Length;
        colAcceptCriteria.MaxInputLength = FieldLimits.AcceptCriteria;
        colApplied.MaxInputLength = FieldLimits.Applied;
        colStatement.MaxInputLength = FieldLimits.Statement;
    }

    private void WireDirtyTracking()
    {
        Control[] textControls =
        {
            txtProjectNo, txtProductName, txtLotNo, txtInventoryCode, txtIncomingQty, cmbUnit,
            txtDrawingNo, txtDrawingRev, txtOther,
            txtWaybillNo, txtPoNo, cmbInventoryGroup, cmbSupplier,
            txtNcr, txtNotes, cmbInspector,
            txtDocumentNo, txtDocRevision
        };

        foreach (var control in textControls)
            control.TextChanged += (_, _) => MarkDirty();

        dtpInspectionDate.ValueChanged += (_, _) => MarkDirty();
        dtpArrivalDate.ValueChanged += (_, _) => MarkDirty();
        dtpDocPublishDate.ValueChanged += (_, _) => MarkDirty();
        dtpDocRevisionDate.ValueChanged += (_, _) => MarkDirty();
    }

    private void SetupToolTips()
    {
        toolTip.SetToolTip(txtIncomingQty, "Sayı girin (ör. 1500 veya 12,5). Binlik ayırıcı kullanmayın. Birimi yandaki kutudan seçin veya yazın.");
        toolTip.SetToolTip(dtpArrivalDate, "Geliş tarihi biliniyorsa kutucuğu işaretleyin.");
        toolTip.SetToolTip(cmbSupplier, "Daha önce girilen tedarikçiler önerilir; yeni bir ad da yazabilirsiniz.");
        toolTip.SetToolTip(txtNcr, "Herhangi bir satırda Red kararı varsa NCR açıklaması zorunludur.");
        toolTip.SetToolTip(txtDocumentNo, "Yeni formda Doküman Bilgileri ekranındaki varsayılan değer gelir; bu forma özel değiştirebilirsiniz.");
        toolTip.SetToolTip(dtpDocRevisionDate, "Revizyon tarihi varsa kutucuğu işaretleyin.");
        toolTip.SetToolTip(btnPrint, "Formu kaydeder ve kağıt formun aynısı olan PDF çıktısını oluşturur.");
    }

    // ==================================================================
    // Olaylar
    // ==================================================================

    private async void btnSave_Click(object? sender, EventArgs e) => await SaveAsync();

    private async void btnPrint_Click(object? sender, EventArgs e) => await SaveAndExportPdfAsync();

    private void btnClose_Click(object? sender, EventArgs e) => Close();

    /// <summary>Onay kutusu tıklanınca değer hemen işlensin (aksi halde hücreden çıkınca işlenir).</summary>
    private void dgvItems_CurrentCellDirtyStateChanged(object? sender, EventArgs e)
    {
        if (dgvItems.IsCurrentCellDirty && dgvItems.CurrentCell is DataGridViewCheckBoxCell)
            dgvItems.CommitEdit(DataGridViewDataErrorContexts.Commit);
    }

    /// <summary>Bir satırda yalnızca bir karar seçilebilir: biri işaretlenince diğerleri kalkar.</summary>
    private void dgvItems_CellValueChanged(object? sender, DataGridViewCellEventArgs e)
    {
        if (_isBinding || e.RowIndex < 0 || e.ColumnIndex < 0)
            return;

        var row = dgvItems.Rows[e.RowIndex];
        var isDecisionColumn = _decisionColumns.Any(c => c.Index == e.ColumnIndex);

        if (isDecisionColumn && !_suppressDecisionSync && row.Cells[e.ColumnIndex].Value is true)
        {
            _suppressDecisionSync = true;
            try
            {
                foreach (var column in _decisionColumns)
                {
                    if (column.Index != e.ColumnIndex)
                        row.Cells[column.Index].Value = false;
                }
            }
            finally
            {
                _suppressDecisionSync = false;
            }
        }

        if (isDecisionColumn)
        {
            UpdateRowStyle(row);
            UpdateStatus();
        }

        MarkDirty();
    }

    private void dgvItems_DataError(object? sender, DataGridViewDataErrorEventArgs e)
    {
        // Metin hücrelerinde beklenmez; yine de kullanıcıya varsayılan hata penceresi gösterilmesin.
        e.ThrowException = false;
    }
}
