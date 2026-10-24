using Digitest.IncomingInspection.Core.Enums;
using Digitest.IncomingInspection.Core.Models;
using Digitest.IncomingInspection.WinForms.Infrastructure;

namespace Digitest.IncomingInspection.WinForms.Forms;

/// <summary>
/// Ana ekran: kayıtlı formların listesi, arama ve işlemler (yeni, aç, PDF, sil).
/// </summary>
public partial class MainForm : Form
{
    private readonly AppServices _services;
    private readonly BindingSource _binding = new();
    private bool _isBusy;

    private sealed record ResultOption(string Text, OverallResult? Value);

    internal MainForm(AppServices services)
    {
        _services = services ?? throw new ArgumentNullException(nameof(services));

        InitializeComponent();

        Icon = AppAssets.AppIcon ?? Icon;
        picLogo.Image = AppAssets.Logo;

        dgvForms.AutoGenerateColumns = false;
        dgvForms.DataSource = _binding;

        cmbResult.DisplayMember = nameof(ResultOption.Text);
        cmbResult.DataSource = new List<ResultOption>
        {
            new("Tümü", null),
            new(OverallResult.Pending.ToDisplayText(), OverallResult.Pending),
            new(OverallResult.Accepted.ToDisplayText(), OverallResult.Accepted),
            new(OverallResult.ConditionalAccepted.ToDisplayText(), OverallResult.ConditionalAccepted),
            new(OverallResult.Rejected.ToDisplayText(), OverallResult.Rejected)
        };

        lblStatusDb.Text = $"Veritabanı: {_services.ConnectionDescription}";
        UpdateActionButtons();
    }

    protected override async void OnShown(EventArgs e)
    {
        base.OnShown(e);
        await LoadListAsync();
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
        switch (keyData)
        {
            case Keys.Control | Keys.N:
                btnNew.PerformClick();
                return true;

            case Keys.Control | Keys.P:
                btnPdf.PerformClick();
                return true;

            case Keys.F5:
                btnRefresh.PerformClick();
                return true;

            case Keys.Enter when dgvForms.Focused:
                btnOpen.PerformClick();
                return true;

            case Keys.Delete when dgvForms.Focused:
                btnDelete.PerformClick();
                return true;
        }

        return base.ProcessCmdKey(ref msg, keyData);
    }

    // ------------------------------------------------------------------
    // Liste
    // ------------------------------------------------------------------

    private async Task LoadListAsync(int? selectId = null)
    {
        if (_isBusy)
            return;

        SetBusy(true);
        try
        {
            var keepId = selectId ?? (_binding.Current as InspectionFormListItem)?.Id;
            var filter = BuildFilter();
            var rows = await _services.Forms.SearchAsync(filter);

            _binding.DataSource = rows;
            SelectRow(keepId);

            lblStatusCount.Text = rows.Count >= filter.MaxRows
                ? $"{rows.Count} kayıt gösteriliyor (en fazla {filter.MaxRows}). Aramayı daraltın."
                : $"{rows.Count} kayıt";
        }
        catch (Exception ex)
        {
            UiHelper.ShowError(this, ex, "Kayıtlar listelenemedi.");
        }
        finally
        {
            SetBusy(false);
        }
    }

    private InspectionFormFilter BuildFilter() => new()
    {
        DateFrom = dtpFrom.Checked ? dtpFrom.Value.Date : null,
        DateTo = dtpTo.Checked ? dtpTo.Value.Date : null,
        FormNo = txtFormNo.Text,
        SupplierName = txtSupplier.Text,
        ProductName = txtProduct.Text,
        LotNo = txtLotNo.Text,
        Result = (cmbResult.SelectedItem as ResultOption)?.Value
    };

    private void SelectRow(int? id)
    {
        if (id is null)
            return;

        foreach (DataGridViewRow row in dgvForms.Rows)
        {
            if (row.DataBoundItem is InspectionFormListItem item && item.Id == id.Value)
            {
                row.Selected = true;
                dgvForms.CurrentCell = row.Cells[colFormNo.Index];
                return;
            }
        }
    }

    private InspectionFormListItem? SelectedItem => _binding.Current as InspectionFormListItem;

    private void SetBusy(bool busy)
    {
        _isBusy = busy;
        UseWaitCursor = busy;
        toolStrip.Enabled = !busy;
        btnSearch.Enabled = !busy;
        btnClear.Enabled = !busy;
        if (!busy)
            UpdateActionButtons();
    }

    private void UpdateActionButtons()
    {
        var hasSelection = SelectedItem is not null;
        btnOpen.Enabled = hasSelection;
        btnPdf.Enabled = hasSelection;
        btnDelete.Enabled = hasSelection;
    }

    // ------------------------------------------------------------------
    // İşlemler
    // ------------------------------------------------------------------

    private async Task OpenEditorAsync(int? formId)
    {
        if (_isBusy)
            return;

        int? savedId;
        bool changed;

        using (var editor = new InspectionEditForm(_services, formId))
        {
            editor.ShowDialog(this);
            changed = editor.DataChanged;
            savedId = editor.SavedFormId;
        }

        if (changed)
            await LoadListAsync(savedId);
    }

    private async Task ExportSelectedPdfAsync()
    {
        var selected = SelectedItem;
        if (selected is null || _isBusy)
            return;

        var notFound = false;

        SetBusy(true);
        try
        {
            var form = await _services.FormService.GetAsync(selected.Id);
            if (form is null)
            {
                notFound = true;
            }
            else
            {
                var path = await Task.Run(() => _services.PdfExporter.Export(form));
                ShowPdfResult(path);
            }
        }
        catch (Exception ex)
        {
            UiHelper.ShowError(this, ex, "PDF oluşturulamadı.");
        }
        finally
        {
            SetBusy(false);
        }

        if (notFound)
        {
            UiHelper.Warn(this, "Kayıt bulunamadı; silinmiş olabilir. Liste yenilenecek.");
            await LoadListAsync();
        }
    }

    private void ShowPdfResult(string path)
    {
        if (_services.Settings.OpenPdfAfterCreate)
        {
            try
            {
                UiHelper.OpenWithShell(path);
                return;
            }
            catch (Exception ex)
            {
                UiHelper.ShowError(this, ex, $"PDF oluşturuldu ancak açılamadı:\n{path}");
                return;
            }
        }

        UiHelper.Info(this, $"PDF oluşturuldu:\n{path}");
    }

    private async Task DeleteSelectedAsync()
    {
        var selected = SelectedItem;
        if (selected is null || _isBusy)
            return;

        var message =
            $"{selected.FormNo} numaralı kayıt kalıcı olarak silinecek.\n\n" +
            $"Tedarikçi: {selected.SupplierName}\nÜrün: {selected.ProductName}\nLot: {selected.LotNo}\n\n" +
            "Bu işlem geri alınamaz. Devam edilsin mi?";

        if (!UiHelper.Confirm(this, message))
            return;

        SetBusy(true);
        try
        {
            await _services.FormService.DeleteAsync(selected.Id);
        }
        catch (Exception ex)
        {
            UiHelper.ShowError(this, ex, "Kayıt silinemedi.");
        }
        finally
        {
            SetBusy(false);
        }

        await LoadListAsync();
    }

    // ------------------------------------------------------------------
    // Olaylar
    // ------------------------------------------------------------------

    private async void btnNew_Click(object? sender, EventArgs e) => await OpenEditorAsync(null);

    private async void btnOpen_Click(object? sender, EventArgs e)
    {
        if (SelectedItem is { } item)
            await OpenEditorAsync(item.Id);
    }

    private async void btnPdf_Click(object? sender, EventArgs e) => await ExportSelectedPdfAsync();

    private async void btnDelete_Click(object? sender, EventArgs e) => await DeleteSelectedAsync();

    private async void btnRefresh_Click(object? sender, EventArgs e) => await LoadListAsync();

    private async void btnSearch_Click(object? sender, EventArgs e) => await LoadListAsync();

    private async void btnClear_Click(object? sender, EventArgs e)
    {
        dtpFrom.Checked = false;
        dtpTo.Checked = false;
        txtFormNo.Clear();
        txtSupplier.Clear();
        txtProduct.Clear();
        txtLotNo.Clear();
        cmbResult.SelectedIndex = 0;
        await LoadListAsync();
    }

    private void btnDocumentInfo_Click(object? sender, EventArgs e)
    {
        using var dialog = new DocumentInfoForm(_services);
        dialog.ShowDialog(this);
    }

    private async void dgvForms_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0 && SelectedItem is { } item)
            await OpenEditorAsync(item.Id);
    }

    private void dgvForms_SelectionChanged(object? sender, EventArgs e) => UpdateActionButtons();

    private void dgvForms_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
    {
        if (e.RowIndex < 0 || e.ColumnIndex != colResult.Index || e.CellStyle is null)
            return;

        if (dgvForms.Rows[e.RowIndex].DataBoundItem is not InspectionFormListItem item)
            return;

        e.CellStyle.BackColor = ResultColors.Back(item.OverallResult);
        e.CellStyle.ForeColor = ResultColors.Fore(item.OverallResult);
        e.CellStyle.SelectionBackColor = ResultColors.Back(item.OverallResult);
        e.CellStyle.SelectionForeColor = ResultColors.Fore(item.OverallResult);
    }
}
