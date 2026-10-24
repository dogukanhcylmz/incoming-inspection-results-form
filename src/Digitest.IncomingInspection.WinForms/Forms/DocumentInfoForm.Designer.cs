#nullable disable

namespace Digitest.IncomingInspection.WinForms.Forms
{
    partial class DocumentInfoForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            tlpMain = new TableLayoutPanel();
            lblDocumentNo = new Label();
            txtDocumentNo = new TextBox();
            lblPublishDate = new Label();
            dtpPublishDate = new DateTimePicker();
            lblRevision = new Label();
            txtRevision = new TextBox();
            lblRevisionDate = new Label();
            dtpRevisionDate = new DateTimePicker();
            lblInfo = new Label();
            flpButtons = new FlowLayoutPanel();
            btnCancel = new Button();
            btnSave = new Button();
            errorProvider = new ErrorProvider(components);
            tlpMain.SuspendLayout();
            flpButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            SuspendLayout();
            //
            // tlpMain
            //
            tlpMain.ColumnCount = 2;
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130F));
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpMain.Controls.Add(lblDocumentNo, 0, 0);
            tlpMain.Controls.Add(txtDocumentNo, 1, 0);
            tlpMain.Controls.Add(lblPublishDate, 0, 1);
            tlpMain.Controls.Add(dtpPublishDate, 1, 1);
            tlpMain.Controls.Add(lblRevision, 0, 2);
            tlpMain.Controls.Add(txtRevision, 1, 2);
            tlpMain.Controls.Add(lblRevisionDate, 0, 3);
            tlpMain.Controls.Add(dtpRevisionDate, 1, 3);
            tlpMain.Controls.Add(lblInfo, 0, 4);
            tlpMain.Controls.Add(flpButtons, 0, 5);
            tlpMain.Dock = DockStyle.Fill;
            tlpMain.Location = new Point(0, 0);
            tlpMain.Name = "tlpMain";
            tlpMain.Padding = new Padding(14, 12, 14, 10);
            tlpMain.RowCount = 6;
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
            tlpMain.Size = new Size(430, 290);
            tlpMain.TabIndex = 0;
            //
            // lblDocumentNo
            //
            lblDocumentNo.Anchor = AnchorStyles.Left;
            lblDocumentNo.AutoSize = true;
            lblDocumentNo.Location = new Point(17, 21);
            lblDocumentNo.Name = "lblDocumentNo";
            lblDocumentNo.Size = new Size(79, 15);
            lblDocumentNo.TabIndex = 0;
            lblDocumentNo.Text = "Doküman No *";
            //
            // txtDocumentNo
            //
            txtDocumentNo.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtDocumentNo.Location = new Point(147, 17);
            txtDocumentNo.Name = "txtDocumentNo";
            txtDocumentNo.Size = new Size(266, 23);
            txtDocumentNo.TabIndex = 1;
            //
            // lblPublishDate
            //
            lblPublishDate.Anchor = AnchorStyles.Left;
            lblPublishDate.AutoSize = true;
            lblPublishDate.Location = new Point(17, 55);
            lblPublishDate.Name = "lblPublishDate";
            lblPublishDate.Size = new Size(71, 15);
            lblPublishDate.TabIndex = 2;
            lblPublishDate.Text = "Yayın Tarihi *";
            //
            // dtpPublishDate
            //
            dtpPublishDate.Anchor = AnchorStyles.Left;
            dtpPublishDate.CustomFormat = "dd.MM.yyyy";
            dtpPublishDate.Format = DateTimePickerFormat.Custom;
            dtpPublishDate.Location = new Point(147, 51);
            dtpPublishDate.Name = "dtpPublishDate";
            dtpPublishDate.Size = new Size(140, 23);
            dtpPublishDate.TabIndex = 3;
            //
            // lblRevision
            //
            lblRevision.Anchor = AnchorStyles.Left;
            lblRevision.AutoSize = true;
            lblRevision.Location = new Point(17, 89);
            lblRevision.Name = "lblRevision";
            lblRevision.Size = new Size(62, 15);
            lblRevision.TabIndex = 4;
            lblRevision.Text = "Revizyon *";
            //
            // txtRevision
            //
            txtRevision.Anchor = AnchorStyles.Left;
            txtRevision.Location = new Point(147, 85);
            txtRevision.Name = "txtRevision";
            txtRevision.Size = new Size(80, 23);
            txtRevision.TabIndex = 5;
            //
            // lblRevisionDate
            //
            lblRevisionDate.Anchor = AnchorStyles.Left;
            lblRevisionDate.AutoSize = true;
            lblRevisionDate.Location = new Point(17, 123);
            lblRevisionDate.Name = "lblRevisionDate";
            lblRevisionDate.Size = new Size(87, 15);
            lblRevisionDate.TabIndex = 6;
            lblRevisionDate.Text = "Revizyon Tarihi";
            //
            // dtpRevisionDate
            //
            dtpRevisionDate.Anchor = AnchorStyles.Left;
            dtpRevisionDate.Checked = false;
            dtpRevisionDate.CustomFormat = "dd.MM.yyyy";
            dtpRevisionDate.Format = DateTimePickerFormat.Custom;
            dtpRevisionDate.Location = new Point(147, 119);
            dtpRevisionDate.Name = "dtpRevisionDate";
            dtpRevisionDate.ShowCheckBox = true;
            dtpRevisionDate.Size = new Size(140, 23);
            dtpRevisionDate.TabIndex = 7;
            //
            // lblInfo
            //
            tlpMain.SetColumnSpan(lblInfo, 2);
            lblInfo.Dock = DockStyle.Fill;
            lblInfo.ForeColor = Color.DimGray;
            lblInfo.Location = new Point(17, 148);
            lblInfo.Name = "lblInfo";
            lblInfo.Padding = new Padding(0, 6, 0, 0);
            lblInfo.Size = new Size(396, 88);
            lblInfo.TabIndex = 8;
            lblInfo.Text = "Bu bilgiler form başlığının sağ üst köşesine basılır ve bundan sonra oluşturulacak yeni formlara uygulanır. Daha önce kaydedilmiş formlar, oluşturuldukları andaki doküman bilgisiyle basılmaya devam eder.";
            //
            // flpButtons
            //
            tlpMain.SetColumnSpan(flpButtons, 2);
            flpButtons.Controls.Add(btnCancel);
            flpButtons.Controls.Add(btnSave);
            flpButtons.Dock = DockStyle.Fill;
            flpButtons.FlowDirection = FlowDirection.RightToLeft;
            flpButtons.Location = new Point(14, 236);
            flpButtons.Margin = new Padding(0);
            flpButtons.Name = "flpButtons";
            flpButtons.Size = new Size(402, 44);
            flpButtons.TabIndex = 9;
            flpButtons.WrapContents = false;
            //
            // btnCancel
            //
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(305, 6);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(94, 32);
            btnCancel.TabIndex = 1;
            btnCancel.Text = "Vazgeç";
            btnCancel.UseVisualStyleBackColor = true;
            //
            // btnSave
            //
            btnSave.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSave.Location = new Point(205, 6);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(94, 32);
            btnSave.TabIndex = 0;
            btnSave.Text = "Kaydet";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            //
            // errorProvider
            //
            errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            errorProvider.ContainerControl = this;
            //
            // DocumentInfoForm
            //
            AcceptButton = btnSave;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(430, 290);
            Controls.Add(tlpMain);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "DocumentInfoForm";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Doküman Bilgileri";
            tlpMain.ResumeLayout(false);
            tlpMain.PerformLayout();
            flpButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tlpMain;
        private Label lblDocumentNo;
        private TextBox txtDocumentNo;
        private Label lblPublishDate;
        private DateTimePicker dtpPublishDate;
        private Label lblRevision;
        private TextBox txtRevision;
        private Label lblRevisionDate;
        private DateTimePicker dtpRevisionDate;
        private Label lblInfo;
        private FlowLayoutPanel flpButtons;
        private Button btnCancel;
        private Button btnSave;
        private ErrorProvider errorProvider;
    }
}
