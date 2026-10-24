using Digitest.IncomingInspection.Core.Models;
using Digitest.IncomingInspection.Core.Validation;
using Digitest.IncomingInspection.WinForms.Infrastructure;

namespace Digitest.IncomingInspection.WinForms.Forms;

/// <summary>
/// Doküman No, Yayın Tarihi, Revizyon ve Revizyon Tarihi ayarları.
/// </summary>
public partial class DocumentInfoForm : Form
{
    private readonly AppServices _services;
    private bool _isBusy;

    internal DocumentInfoForm(AppServices services)
    {
        _services = services ?? throw new ArgumentNullException(nameof(services));

        InitializeComponent();

        Icon = AppAssets.AppIcon ?? Icon;
        txtDocumentNo.MaxLength = FieldLimits.DocumentNo;
        txtRevision.MaxLength = FieldLimits.DocRevision;
    }

    protected override async void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        SetBusy(true);
        try
        {
            var info = await _services.DocumentInfo.GetAsync();

            txtDocumentNo.Text = info.DocumentNo;
            dtpPublishDate.Value = info.PublishDate;
            txtRevision.Text = info.Revision;

            if (info.RevisionDate is DateTime revisionDate)
            {
                dtpRevisionDate.Value = revisionDate;
                dtpRevisionDate.Checked = true;
            }
            else
            {
                dtpRevisionDate.Value = DateTime.Today;
                dtpRevisionDate.Checked = false;
            }
        }
        catch (Exception ex)
        {
            UiHelper.ShowError(this, ex, "Doküman bilgileri okunamadı.");
        }
        finally
        {
            SetBusy(false);
        }
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        if (_isBusy)
            e.Cancel = true;

        base.OnFormClosing(e);
    }

    private async void btnSave_Click(object? sender, EventArgs e)
    {
        if (_isBusy || !ValidateInputs())
            return;

        var info = new FormDocumentInfo
        {
            DocumentNo = txtDocumentNo.Text.Trim(),
            PublishDate = dtpPublishDate.Value.Date,
            Revision = txtRevision.Text.Trim(),
            RevisionDate = dtpRevisionDate.Checked ? dtpRevisionDate.Value.Date : null
        };

        SetBusy(true);
        try
        {
            await _services.DocumentInfo.SaveAsync(info);
        }
        catch (Exception ex)
        {
            UiHelper.ShowError(this, ex, "Doküman bilgileri kaydedilemedi.");
            return;
        }
        finally
        {
            SetBusy(false);
        }

        DialogResult = DialogResult.OK;
        Close();
    }

    private bool ValidateInputs()
    {
        errorProvider.Clear();
        var valid = true;

        if (string.IsNullOrWhiteSpace(txtDocumentNo.Text))
        {
            errorProvider.SetError(txtDocumentNo, "Doküman No zorunludur.");
            valid = false;
        }

        if (string.IsNullOrWhiteSpace(txtRevision.Text))
        {
            errorProvider.SetError(txtRevision, "Revizyon zorunludur (ör. 00).");
            valid = false;
        }

        if (dtpRevisionDate.Checked && dtpRevisionDate.Value.Date < dtpPublishDate.Value.Date)
        {
            errorProvider.SetError(dtpRevisionDate, "Revizyon tarihi yayın tarihinden önce olamaz.");
            valid = false;
        }

        return valid;
    }

    private void SetBusy(bool busy)
    {
        _isBusy = busy;
        UseWaitCursor = busy;
        tlpMain.Enabled = !busy;
    }
}
