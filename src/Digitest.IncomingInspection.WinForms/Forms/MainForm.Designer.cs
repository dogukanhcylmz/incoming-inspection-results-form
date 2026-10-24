#nullable disable

namespace Digitest.IncomingInspection.WinForms.Forms
{
    partial class MainForm
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
            DataGridViewCellStyle headerCellStyle = new DataGridViewCellStyle();
            DataGridViewCellStyle alternatingRowStyle = new DataGridViewCellStyle();
            DataGridViewCellStyle dateCellStyle = new DataGridViewCellStyle();
            DataGridViewCellStyle quantityCellStyle = new DataGridViewCellStyle();
            DataGridViewCellStyle resultCellStyle = new DataGridViewCellStyle();
            DataGridViewCellStyle dateTimeCellStyle = new DataGridViewCellStyle();
            pnlHeader = new Panel();
            lblAppSubtitle = new Label();
            lblAppTitle = new Label();
            picLogo = new PictureBox();
            toolStrip = new ToolStrip();
            btnNew = new ToolStripButton();
            btnOpen = new ToolStripButton();
            btnPdf = new ToolStripButton();
            sepActions = new ToolStripSeparator();
            btnDelete = new ToolStripButton();
            btnRefresh = new ToolStripButton();
            grpFilter = new GroupBox();
            tlpFilter = new TableLayoutPanel();
            lblDate = new Label();
            dtpFrom = new DateTimePicker();
            lblDateSeparator = new Label();
            dtpTo = new DateTimePicker();
            lblFormNo = new Label();
            txtFormNo = new TextBox();
            lblSupplier = new Label();
            txtSupplier = new TextBox();
            btnSearch = new Button();
            lblResult = new Label();
            cmbResult = new ComboBox();
            lblProduct = new Label();
            txtProduct = new TextBox();
            lblLotNo = new Label();
            txtLotNo = new TextBox();
            btnClear = new Button();
            dgvForms = new DataGridView();
            colFormNo = new DataGridViewTextBoxColumn();
            colInspectionDate = new DataGridViewTextBoxColumn();
            colSupplier = new DataGridViewTextBoxColumn();
            colProduct = new DataGridViewTextBoxColumn();
            colLotNo = new DataGridViewTextBoxColumn();
            colPurchaseOrderNo = new DataGridViewTextBoxColumn();
            colQuantity = new DataGridViewTextBoxColumn();
            colResult = new DataGridViewTextBoxColumn();
            colInspector = new DataGridViewTextBoxColumn();
            colLastChanged = new DataGridViewTextBoxColumn();
            statusStrip = new StatusStrip();
            lblStatusCount = new ToolStripStatusLabel();
            lblStatusDb = new ToolStripStatusLabel();
            pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picLogo).BeginInit();
            toolStrip.SuspendLayout();
            grpFilter.SuspendLayout();
            tlpFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvForms).BeginInit();
            statusStrip.SuspendLayout();
            SuspendLayout();
            //
            // pnlHeader
            //
            pnlHeader.BackColor = Color.White;
            pnlHeader.Controls.Add(lblAppSubtitle);
            pnlHeader.Controls.Add(lblAppTitle);
            pnlHeader.Controls.Add(picLogo);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(1200, 64);
            pnlHeader.TabIndex = 0;
            //
            // lblAppSubtitle
            //
            lblAppSubtitle.AutoSize = true;
            lblAppSubtitle.ForeColor = Color.DimGray;
            lblAppSubtitle.Location = new Point(222, 37);
            lblAppSubtitle.Name = "lblAppSubtitle";
            lblAppSubtitle.Size = new Size(260, 15);
            lblAppSubtitle.TabIndex = 2;
            lblAppSubtitle.Text = "Incoming Inspection and Results";
            //
            // lblAppTitle
            //
            lblAppTitle.AutoSize = true;
            lblAppTitle.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblAppTitle.ForeColor = Color.FromArgb(13, 27, 62);
            lblAppTitle.Location = new Point(218, 7);
            lblAppTitle.Name = "lblAppTitle";
            lblAppTitle.Size = new Size(345, 28);
            lblAppTitle.TabIndex = 1;
            lblAppTitle.Text = "Girdi Muayene ve Sonuç Kayıtları";
            //
            // picLogo
            //
            picLogo.Location = new Point(14, 10);
            picLogo.Name = "picLogo";
            picLogo.Size = new Size(190, 44);
            picLogo.SizeMode = PictureBoxSizeMode.Zoom;
            picLogo.TabIndex = 0;
            picLogo.TabStop = false;
            //
            // toolStrip
            //
            toolStrip.GripStyle = ToolStripGripStyle.Hidden;
            toolStrip.Items.AddRange(new ToolStripItem[] { btnNew, btnOpen, btnPdf, sepActions, btnDelete, btnRefresh });
            toolStrip.Location = new Point(0, 64);
            toolStrip.Name = "toolStrip";
            toolStrip.Padding = new Padding(8, 2, 8, 2);
            toolStrip.Size = new Size(1200, 29);
            toolStrip.TabIndex = 1;
            //
            // btnNew
            //
            btnNew.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnNew.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnNew.Name = "btnNew";
            btnNew.Size = new Size(110, 22);
            btnNew.Text = "+ Yeni Form";
            btnNew.ToolTipText = "Yeni girdi muayene formu (Ctrl+N)";
            btnNew.Click += btnNew_Click;
            //
            // btnOpen
            //
            btnOpen.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnOpen.Name = "btnOpen";
            btnOpen.Size = new Size(80, 22);
            btnOpen.Text = "Aç / Düzenle";
            btnOpen.ToolTipText = "Seçili kaydı aç (Enter veya çift tık)";
            btnOpen.Click += btnOpen_Click;
            //
            // btnPdf
            //
            btnPdf.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnPdf.Name = "btnPdf";
            btnPdf.Size = new Size(50, 22);
            btnPdf.Text = "PDF Al";
            btnPdf.ToolTipText = "Seçili kaydın PDF çıktısını oluştur (Ctrl+P)";
            btnPdf.Click += btnPdf_Click;
            //
            // sepActions
            //
            sepActions.Name = "sepActions";
            sepActions.Size = new Size(6, 25);
            //
            // btnDelete
            //
            btnDelete.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnDelete.ForeColor = Color.FromArgb(132, 32, 41);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(28, 22);
            btnDelete.Text = "Sil";
            btnDelete.ToolTipText = "Seçili kaydı sil (Delete)";
            btnDelete.Click += btnDelete_Click;
            //
            // btnRefresh
            //
            btnRefresh.Alignment = ToolStripItemAlignment.Right;
            btnRefresh.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(75, 22);
            btnRefresh.Text = "Yenile (F5)";
            btnRefresh.Click += btnRefresh_Click;
            //
            // grpFilter
            //
            grpFilter.Controls.Add(tlpFilter);
            grpFilter.Dock = DockStyle.Top;
            grpFilter.Location = new Point(0, 93);
            grpFilter.Name = "grpFilter";
            grpFilter.Padding = new Padding(8, 4, 8, 6);
            grpFilter.Size = new Size(1200, 96);
            grpFilter.TabIndex = 2;
            grpFilter.TabStop = false;
            grpFilter.Text = "Arama";
            //
            // tlpFilter
            //
            tlpFilter.ColumnCount = 9;
            tlpFilter.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            tlpFilter.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 125F));
            tlpFilter.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 22F));
            tlpFilter.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 125F));
            tlpFilter.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
            tlpFilter.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpFilter.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
            tlpFilter.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpFilter.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            tlpFilter.Controls.Add(lblDate, 0, 0);
            tlpFilter.Controls.Add(dtpFrom, 1, 0);
            tlpFilter.Controls.Add(lblDateSeparator, 2, 0);
            tlpFilter.Controls.Add(dtpTo, 3, 0);
            tlpFilter.Controls.Add(lblFormNo, 4, 0);
            tlpFilter.Controls.Add(txtFormNo, 5, 0);
            tlpFilter.Controls.Add(lblSupplier, 6, 0);
            tlpFilter.Controls.Add(txtSupplier, 7, 0);
            tlpFilter.Controls.Add(btnSearch, 8, 0);
            tlpFilter.Controls.Add(lblResult, 0, 1);
            tlpFilter.Controls.Add(cmbResult, 1, 1);
            tlpFilter.Controls.Add(lblProduct, 4, 1);
            tlpFilter.Controls.Add(txtProduct, 5, 1);
            tlpFilter.Controls.Add(lblLotNo, 6, 1);
            tlpFilter.Controls.Add(txtLotNo, 7, 1);
            tlpFilter.Controls.Add(btnClear, 8, 1);
            tlpFilter.Dock = DockStyle.Fill;
            tlpFilter.Location = new Point(8, 20);
            tlpFilter.Name = "tlpFilter";
            tlpFilter.RowCount = 2;
            tlpFilter.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpFilter.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpFilter.Size = new Size(1184, 70);
            tlpFilter.TabIndex = 0;
            //
            // lblDate
            //
            lblDate.Anchor = AnchorStyles.Left;
            lblDate.AutoSize = true;
            lblDate.Location = new Point(3, 10);
            lblDate.Name = "lblDate";
            lblDate.Size = new Size(88, 15);
            lblDate.TabIndex = 0;
            lblDate.Text = "Muayene Tarihi";
            //
            // dtpFrom
            //
            dtpFrom.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            dtpFrom.Checked = false;
            dtpFrom.CustomFormat = "dd.MM.yyyy";
            dtpFrom.Format = DateTimePickerFormat.Custom;
            dtpFrom.Location = new Point(103, 6);
            dtpFrom.Name = "dtpFrom";
            dtpFrom.ShowCheckBox = true;
            dtpFrom.Size = new Size(119, 23);
            dtpFrom.TabIndex = 1;
            //
            // lblDateSeparator
            //
            lblDateSeparator.Anchor = AnchorStyles.None;
            lblDateSeparator.AutoSize = true;
            lblDateSeparator.Location = new Point(228, 10);
            lblDateSeparator.Name = "lblDateSeparator";
            lblDateSeparator.Size = new Size(12, 15);
            lblDateSeparator.TabIndex = 2;
            lblDateSeparator.Text = "–";
            //
            // dtpTo
            //
            dtpTo.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            dtpTo.Checked = false;
            dtpTo.CustomFormat = "dd.MM.yyyy";
            dtpTo.Format = DateTimePickerFormat.Custom;
            dtpTo.Location = new Point(250, 6);
            dtpTo.Name = "dtpTo";
            dtpTo.ShowCheckBox = true;
            dtpTo.Size = new Size(119, 23);
            dtpTo.TabIndex = 3;
            //
            // lblFormNo
            //
            lblFormNo.Anchor = AnchorStyles.Left;
            lblFormNo.AutoSize = true;
            lblFormNo.Location = new Point(375, 10);
            lblFormNo.Name = "lblFormNo";
            lblFormNo.Size = new Size(52, 15);
            lblFormNo.TabIndex = 4;
            lblFormNo.Text = "Kayıt No";
            //
            // txtFormNo
            //
            txtFormNo.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtFormNo.Location = new Point(455, 6);
            txtFormNo.Name = "txtFormNo";
            txtFormNo.PlaceholderText = "GM-2026-...";
            txtFormNo.Size = new Size(279, 23);
            txtFormNo.TabIndex = 5;
            //
            // lblSupplier
            //
            lblSupplier.Anchor = AnchorStyles.Left;
            lblSupplier.AutoSize = true;
            lblSupplier.Location = new Point(740, 10);
            lblSupplier.Name = "lblSupplier";
            lblSupplier.Size = new Size(56, 15);
            lblSupplier.TabIndex = 6;
            lblSupplier.Text = "Tedarikçi";
            //
            // txtSupplier
            //
            txtSupplier.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtSupplier.Location = new Point(820, 6);
            txtSupplier.Name = "txtSupplier";
            txtSupplier.Size = new Size(251, 23);
            txtSupplier.TabIndex = 7;
            //
            // btnSearch
            //
            btnSearch.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            btnSearch.Location = new Point(1077, 4);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(104, 27);
            btnSearch.TabIndex = 8;
            btnSearch.Text = "Ara";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            //
            // lblResult
            //
            lblResult.Anchor = AnchorStyles.Left;
            lblResult.AutoSize = true;
            lblResult.Location = new Point(3, 45);
            lblResult.Name = "lblResult";
            lblResult.Size = new Size(72, 15);
            lblResult.TabIndex = 9;
            lblResult.Text = "Genel Sonuç";
            //
            // cmbResult
            //
            cmbResult.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            tlpFilter.SetColumnSpan(cmbResult, 3);
            cmbResult.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbResult.FormattingEnabled = true;
            cmbResult.Location = new Point(103, 41);
            cmbResult.Name = "cmbResult";
            cmbResult.Size = new Size(266, 23);
            cmbResult.TabIndex = 10;
            //
            // lblProduct
            //
            lblProduct.Anchor = AnchorStyles.Left;
            lblProduct.AutoSize = true;
            lblProduct.Location = new Point(375, 45);
            lblProduct.Name = "lblProduct";
            lblProduct.Size = new Size(66, 15);
            lblProduct.TabIndex = 11;
            lblProduct.Text = "Ürün/Parça";
            //
            // txtProduct
            //
            txtProduct.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtProduct.Location = new Point(455, 41);
            txtProduct.Name = "txtProduct";
            txtProduct.Size = new Size(279, 23);
            txtProduct.TabIndex = 12;
            //
            // lblLotNo
            //
            lblLotNo.Anchor = AnchorStyles.Left;
            lblLotNo.AutoSize = true;
            lblLotNo.Location = new Point(740, 45);
            lblLotNo.Name = "lblLotNo";
            lblLotNo.Size = new Size(43, 15);
            lblLotNo.TabIndex = 13;
            lblLotNo.Text = "Lot No";
            //
            // txtLotNo
            //
            txtLotNo.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtLotNo.Location = new Point(820, 41);
            txtLotNo.Name = "txtLotNo";
            txtLotNo.Size = new Size(251, 23);
            txtLotNo.TabIndex = 14;
            //
            // btnClear
            //
            btnClear.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            btnClear.Location = new Point(1077, 39);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(104, 27);
            btnClear.TabIndex = 15;
            btnClear.Text = "Temizle";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            //
            // dgvForms
            //
            dgvForms.AllowUserToAddRows = false;
            dgvForms.AllowUserToDeleteRows = false;
            dgvForms.AllowUserToResizeRows = false;
            alternatingRowStyle.BackColor = Color.FromArgb(248, 249, 251);
            dgvForms.AlternatingRowsDefaultCellStyle = alternatingRowStyle;
            dgvForms.BackgroundColor = Color.White;
            dgvForms.BorderStyle = BorderStyle.None;
            headerCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            headerCellStyle.BackColor = Color.FromArgb(235, 237, 242);
            headerCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            headerCellStyle.ForeColor = Color.FromArgb(13, 27, 62);
            headerCellStyle.SelectionBackColor = Color.FromArgb(235, 237, 242);
            headerCellStyle.SelectionForeColor = Color.FromArgb(13, 27, 62);
            headerCellStyle.WrapMode = DataGridViewTriState.True;
            dgvForms.ColumnHeadersDefaultCellStyle = headerCellStyle;
            dgvForms.ColumnHeadersHeight = 34;
            dgvForms.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvForms.Columns.AddRange(new DataGridViewColumn[] { colFormNo, colInspectionDate, colSupplier, colProduct, colLotNo, colPurchaseOrderNo, colQuantity, colResult, colInspector, colLastChanged });
            dgvForms.Dock = DockStyle.Fill;
            dgvForms.EnableHeadersVisualStyles = false;
            dgvForms.Location = new Point(0, 189);
            dgvForms.MultiSelect = false;
            dgvForms.Name = "dgvForms";
            dgvForms.ReadOnly = true;
            dgvForms.RowHeadersVisible = false;
            dgvForms.RowTemplate.Height = 28;
            dgvForms.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvForms.Size = new Size(1200, 509);
            dgvForms.TabIndex = 3;
            dgvForms.CellDoubleClick += dgvForms_CellDoubleClick;
            dgvForms.CellFormatting += dgvForms_CellFormatting;
            dgvForms.SelectionChanged += dgvForms_SelectionChanged;
            //
            // colFormNo
            //
            colFormNo.DataPropertyName = "FormNo";
            colFormNo.HeaderText = "Kayıt No";
            colFormNo.Name = "colFormNo";
            colFormNo.ReadOnly = true;
            colFormNo.Width = 115;
            //
            // colInspectionDate
            //
            colInspectionDate.DataPropertyName = "InspectionDate";
            dateCellStyle.Format = "dd.MM.yyyy";
            colInspectionDate.DefaultCellStyle = dateCellStyle;
            colInspectionDate.HeaderText = "Muayene Tarihi";
            colInspectionDate.Name = "colInspectionDate";
            colInspectionDate.ReadOnly = true;
            colInspectionDate.Width = 105;
            //
            // colSupplier
            //
            colSupplier.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colSupplier.DataPropertyName = "SupplierName";
            colSupplier.FillWeight = 50F;
            colSupplier.HeaderText = "Tedarikçi";
            colSupplier.MinimumWidth = 120;
            colSupplier.Name = "colSupplier";
            colSupplier.ReadOnly = true;
            //
            // colProduct
            //
            colProduct.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colProduct.DataPropertyName = "ProductName";
            colProduct.FillWeight = 50F;
            colProduct.HeaderText = "Ürün/Parça Adı";
            colProduct.MinimumWidth = 120;
            colProduct.Name = "colProduct";
            colProduct.ReadOnly = true;
            //
            // colLotNo
            //
            colLotNo.DataPropertyName = "LotNo";
            colLotNo.HeaderText = "Lot/Parti/Seri No";
            colLotNo.Name = "colLotNo";
            colLotNo.ReadOnly = true;
            colLotNo.Width = 125;
            //
            // colPurchaseOrderNo
            //
            colPurchaseOrderNo.DataPropertyName = "PurchaseOrderNo";
            colPurchaseOrderNo.HeaderText = "Sipariş No";
            colPurchaseOrderNo.Name = "colPurchaseOrderNo";
            colPurchaseOrderNo.ReadOnly = true;
            colPurchaseOrderNo.Width = 105;
            //
            // colQuantity
            //
            colQuantity.DataPropertyName = "QuantityText";
            quantityCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colQuantity.DefaultCellStyle = quantityCellStyle;
            colQuantity.HeaderText = "Gelen Miktar";
            colQuantity.Name = "colQuantity";
            colQuantity.ReadOnly = true;
            colQuantity.Width = 95;
            //
            // colResult
            //
            colResult.DataPropertyName = "OverallResultText";
            resultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            resultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            colResult.DefaultCellStyle = resultCellStyle;
            colResult.HeaderText = "Genel Sonuç";
            colResult.Name = "colResult";
            colResult.ReadOnly = true;
            colResult.Width = 100;
            //
            // colInspector
            //
            colInspector.DataPropertyName = "InspectorName";
            colInspector.HeaderText = "Muayene Eden";
            colInspector.Name = "colInspector";
            colInspector.ReadOnly = true;
            colInspector.Width = 130;
            //
            // colLastChanged
            //
            colLastChanged.DataPropertyName = "LastChangedAt";
            dateTimeCellStyle.Format = "dd.MM.yyyy HH:mm";
            colLastChanged.DefaultCellStyle = dateTimeCellStyle;
            colLastChanged.HeaderText = "Son Değişiklik";
            colLastChanged.Name = "colLastChanged";
            colLastChanged.ReadOnly = true;
            colLastChanged.Width = 120;
            //
            // statusStrip
            //
            statusStrip.Items.AddRange(new ToolStripItem[] { lblStatusCount, lblStatusDb });
            statusStrip.Location = new Point(0, 698);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(1200, 22);
            statusStrip.TabIndex = 4;
            //
            // lblStatusCount
            //
            lblStatusCount.Name = "lblStatusCount";
            lblStatusCount.Size = new Size(1080, 17);
            lblStatusCount.Spring = true;
            lblStatusCount.Text = "Hazır";
            lblStatusCount.TextAlign = ContentAlignment.MiddleLeft;
            //
            // lblStatusDb
            //
            lblStatusDb.ForeColor = Color.DimGray;
            lblStatusDb.Name = "lblStatusDb";
            lblStatusDb.Size = new Size(105, 17);
            lblStatusDb.Text = "Veritabanı";
            //
            // MainForm
            //
            AcceptButton = btnSearch;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1200, 720);
            Controls.Add(dgvForms);
            Controls.Add(grpFilter);
            Controls.Add(toolStrip);
            Controls.Add(pnlHeader);
            Controls.Add(statusStrip);
            KeyPreview = true;
            MinimumSize = new Size(1000, 600);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "DIGITEST Girdi Muayene";
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picLogo).EndInit();
            toolStrip.ResumeLayout(false);
            toolStrip.PerformLayout();
            grpFilter.ResumeLayout(false);
            tlpFilter.ResumeLayout(false);
            tlpFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvForms).EndInit();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel pnlHeader;
        private PictureBox picLogo;
        private Label lblAppTitle;
        private Label lblAppSubtitle;
        private ToolStrip toolStrip;
        private ToolStripButton btnNew;
        private ToolStripButton btnOpen;
        private ToolStripButton btnPdf;
        private ToolStripSeparator sepActions;
        private ToolStripButton btnDelete;
        private ToolStripButton btnRefresh;
        private GroupBox grpFilter;
        private TableLayoutPanel tlpFilter;
        private Label lblDate;
        private DateTimePicker dtpFrom;
        private Label lblDateSeparator;
        private DateTimePicker dtpTo;
        private Label lblFormNo;
        private TextBox txtFormNo;
        private Label lblSupplier;
        private TextBox txtSupplier;
        private Button btnSearch;
        private Label lblResult;
        private ComboBox cmbResult;
        private Label lblProduct;
        private TextBox txtProduct;
        private Label lblLotNo;
        private TextBox txtLotNo;
        private Button btnClear;
        private DataGridView dgvForms;
        private DataGridViewTextBoxColumn colFormNo;
        private DataGridViewTextBoxColumn colInspectionDate;
        private DataGridViewTextBoxColumn colSupplier;
        private DataGridViewTextBoxColumn colProduct;
        private DataGridViewTextBoxColumn colLotNo;
        private DataGridViewTextBoxColumn colPurchaseOrderNo;
        private DataGridViewTextBoxColumn colQuantity;
        private DataGridViewTextBoxColumn colResult;
        private DataGridViewTextBoxColumn colInspector;
        private DataGridViewTextBoxColumn colLastChanged;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel lblStatusCount;
        private ToolStripStatusLabel lblStatusDb;
    }
}
