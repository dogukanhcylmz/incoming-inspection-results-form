#nullable disable

namespace Digitest.IncomingInspection.WinForms.Forms
{
    partial class InspectionEditForm
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
            DataGridViewCellStyle itemsHeaderStyle = new DataGridViewCellStyle();
            DataGridViewCellStyle itemsDefaultStyle = new DataGridViewCellStyle();
            DataGridViewCellStyle rowNoCellStyle = new DataGridViewCellStyle();
            DataGridViewCellStyle typeCellStyle = new DataGridViewCellStyle();
            tlpMain = new TableLayoutPanel();
            tlpHeader = new TableLayoutPanel();
            picLogo = new PictureBox();
            lblTitle = new Label();
            lblTitleEn = new Label();
            tlpDocInfo = new TableLayoutPanel();
            lblDocumentNo = new Label();
            txtDocumentNo = new TextBox();
            lblDocPublishDate = new Label();
            dtpDocPublishDate = new DateTimePicker();
            lblDocRevision = new Label();
            txtDocRevision = new TextBox();
            lblDocRevisionDate = new Label();
            dtpDocRevisionDate = new DateTimePicker();
            tlpInfo = new TableLayoutPanel();
            grpProduct = new GroupBox();
            tlpProduct = new TableLayoutPanel();
            lblProjectNo = new Label();
            txtProjectNo = new TextBox();
            lblInventoryCode = new Label();
            txtInventoryCode = new TextBox();
            lblProductName = new Label();
            txtProductName = new TextBox();
            lblIncomingQty = new Label();
            tlpQuantity = new TableLayoutPanel();
            txtIncomingQty = new TextBox();
            cmbUnit = new ComboBox();
            lblLotNo = new Label();
            txtLotNo = new TextBox();
            lblDrawingNo = new Label();
            txtDrawingNo = new TextBox();
            lblInspectionDate = new Label();
            dtpInspectionDate = new DateTimePicker();
            lblDrawingRev = new Label();
            txtDrawingRev = new TextBox();
            lblOther = new Label();
            txtOther = new TextBox();
            grpCommercial = new GroupBox();
            tlpCommercial = new TableLayoutPanel();
            lblWaybillNo = new Label();
            txtWaybillNo = new TextBox();
            lblPoNo = new Label();
            txtPoNo = new TextBox();
            lblArrivalDate = new Label();
            dtpArrivalDate = new DateTimePicker();
            lblInventoryGroup = new Label();
            cmbInventoryGroup = new ComboBox();
            lblSupplier = new Label();
            cmbSupplier = new ComboBox();
            grpItems = new GroupBox();
            dgvItems = new DataGridView();
            colRowNo = new DataGridViewTextBoxColumn();
            colInspectionType = new DataGridViewTextBoxColumn();
            colAcceptCriteria = new DataGridViewTextBoxColumn();
            colApplied = new DataGridViewTextBoxColumn();
            colAccepted = new DataGridViewCheckBoxColumn();
            colConditional = new DataGridViewCheckBoxColumn();
            colRejected = new DataGridViewCheckBoxColumn();
            colStatement = new DataGridViewTextBoxColumn();
            tlpBottom = new TableLayoutPanel();
            grpNcr = new GroupBox();
            txtNcr = new TextBox();
            grpNotes = new GroupBox();
            txtNotes = new TextBox();
            grpInspector = new GroupBox();
            tlpInspector = new TableLayoutPanel();
            lblInspector = new Label();
            cmbInspector = new ComboBox();
            lblSignatureHint = new Label();
            tlpButtons = new TableLayoutPanel();
            lblStatus = new Label();
            flpButtons = new FlowLayoutPanel();
            btnSave = new Button();
            btnPrint = new Button();
            btnClose = new Button();
            errorProvider = new ErrorProvider(components);
            toolTip = new ToolTip(components);
            tlpMain.SuspendLayout();
            tlpHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picLogo).BeginInit();
            tlpDocInfo.SuspendLayout();
            tlpInfo.SuspendLayout();
            grpProduct.SuspendLayout();
            tlpProduct.SuspendLayout();
            tlpQuantity.SuspendLayout();
            grpCommercial.SuspendLayout();
            tlpCommercial.SuspendLayout();
            grpItems.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvItems).BeginInit();
            tlpBottom.SuspendLayout();
            grpNcr.SuspendLayout();
            grpNotes.SuspendLayout();
            grpInspector.SuspendLayout();
            tlpInspector.SuspendLayout();
            tlpButtons.SuspendLayout();
            flpButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            SuspendLayout();
            //
            // tlpMain
            //
            tlpMain.ColumnCount = 1;
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpMain.Controls.Add(tlpHeader, 0, 0);
            tlpMain.Controls.Add(tlpInfo, 0, 1);
            tlpMain.Controls.Add(grpItems, 0, 2);
            tlpMain.Controls.Add(tlpBottom, 0, 3);
            tlpMain.Controls.Add(tlpButtons, 0, 4);
            tlpMain.Dock = DockStyle.Fill;
            tlpMain.Location = new Point(0, 0);
            tlpMain.Name = "tlpMain";
            tlpMain.Padding = new Padding(10);
            tlpMain.RowCount = 5;
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 112F));
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 214F));
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 140F));
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            tlpMain.Size = new Size(1280, 840);
            tlpMain.TabIndex = 0;
            //
            // tlpHeader
            //
            tlpHeader.BackColor = Color.White;
            tlpHeader.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            tlpHeader.ColumnCount = 3;
            tlpHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 230F));
            tlpHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 320F));
            tlpHeader.Controls.Add(picLogo, 0, 0);
            tlpHeader.Controls.Add(lblTitle, 1, 0);
            tlpHeader.Controls.Add(lblTitleEn, 1, 1);
            tlpHeader.Controls.Add(tlpDocInfo, 2, 0);
            tlpHeader.Dock = DockStyle.Fill;
            tlpHeader.Location = new Point(10, 10);
            tlpHeader.Margin = new Padding(0);
            tlpHeader.Name = "tlpHeader";
            tlpHeader.RowCount = 2;
            tlpHeader.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpHeader.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpHeader.Size = new Size(1260, 112);
            tlpHeader.TabIndex = 0;
            //
            // picLogo
            //
            picLogo.Dock = DockStyle.Fill;
            picLogo.Location = new Point(9, 9);
            picLogo.Margin = new Padding(8);
            picLogo.Name = "picLogo";
            tlpHeader.SetRowSpan(picLogo, 2);
            picLogo.Size = new Size(214, 94);
            picLogo.SizeMode = PictureBoxSizeMode.Zoom;
            picLogo.TabIndex = 0;
            picLogo.TabStop = false;
            //
            // lblTitle
            //
            lblTitle.Dock = DockStyle.Fill;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(13, 27, 62);
            lblTitle.Location = new Point(235, 1);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(702, 54);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "Girdi Muayene ve Sonuç Formu";
            lblTitle.TextAlign = ContentAlignment.BottomCenter;
            //
            // lblTitleEn
            //
            lblTitleEn.Dock = DockStyle.Fill;
            lblTitleEn.Font = new Font("Segoe UI", 11F, FontStyle.Italic);
            lblTitleEn.ForeColor = Color.DimGray;
            lblTitleEn.Location = new Point(235, 56);
            lblTitleEn.Name = "lblTitleEn";
            lblTitleEn.Size = new Size(702, 55);
            lblTitleEn.TabIndex = 2;
            lblTitleEn.Text = "Incoming Inspection and Results Form";
            lblTitleEn.TextAlign = ContentAlignment.TopCenter;
            //
            // tlpDocInfo
            //
            tlpDocInfo.ColumnCount = 2;
            tlpDocInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 112F));
            tlpDocInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpDocInfo.Controls.Add(lblDocumentNo, 0, 0);
            tlpDocInfo.Controls.Add(txtDocumentNo, 1, 0);
            tlpDocInfo.Controls.Add(lblDocPublishDate, 0, 1);
            tlpDocInfo.Controls.Add(dtpDocPublishDate, 1, 1);
            tlpDocInfo.Controls.Add(lblDocRevision, 0, 2);
            tlpDocInfo.Controls.Add(txtDocRevision, 1, 2);
            tlpDocInfo.Controls.Add(lblDocRevisionDate, 0, 3);
            tlpDocInfo.Controls.Add(dtpDocRevisionDate, 1, 3);
            tlpDocInfo.Dock = DockStyle.Fill;
            tlpDocInfo.Location = new Point(941, 1);
            tlpDocInfo.Margin = new Padding(0);
            tlpDocInfo.Name = "tlpDocInfo";
            tlpDocInfo.Padding = new Padding(6, 1, 6, 1);
            tlpDocInfo.RowCount = 4;
            tlpDocInfo.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tlpDocInfo.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tlpDocInfo.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tlpDocInfo.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tlpHeader.SetRowSpan(tlpDocInfo, 2);
            tlpDocInfo.Size = new Size(318, 110);
            tlpDocInfo.TabIndex = 3;
            //
            // lblDocumentNo
            //
            lblDocumentNo.Anchor = AnchorStyles.Left;
            lblDocumentNo.AutoSize = true;
            lblDocumentNo.Location = new Point(9, 7);
            lblDocumentNo.Name = "lblDocumentNo";
            lblDocumentNo.Size = new Size(83, 15);
            lblDocumentNo.TabIndex = 0;
            lblDocumentNo.Text = "Doküman No *";
            //
            // txtDocumentNo
            //
            txtDocumentNo.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtDocumentNo.Location = new Point(121, 3);
            txtDocumentNo.Name = "txtDocumentNo";
            txtDocumentNo.Size = new Size(188, 23);
            txtDocumentNo.TabIndex = 1;
            //
            // lblDocPublishDate
            //
            lblDocPublishDate.Anchor = AnchorStyles.Left;
            lblDocPublishDate.AutoSize = true;
            lblDocPublishDate.Location = new Point(9, 34);
            lblDocPublishDate.Name = "lblDocPublishDate";
            lblDocPublishDate.Size = new Size(73, 15);
            lblDocPublishDate.TabIndex = 2;
            lblDocPublishDate.Text = "Yayın Tarihi *";
            //
            // dtpDocPublishDate
            //
            dtpDocPublishDate.Anchor = AnchorStyles.Left;
            dtpDocPublishDate.Checked = false;
            dtpDocPublishDate.ShowCheckBox = true;
            dtpDocPublishDate.CustomFormat = "dd.MM.yyyy";
            dtpDocPublishDate.Format = DateTimePickerFormat.Custom;
            dtpDocPublishDate.Location = new Point(121, 30);
            dtpDocPublishDate.Name = "dtpDocPublishDate";
            dtpDocPublishDate.Size = new Size(140, 23);
            dtpDocPublishDate.TabIndex = 3;
            //
            // lblDocRevision
            //
            lblDocRevision.Anchor = AnchorStyles.Left;
            lblDocRevision.AutoSize = true;
            lblDocRevision.Location = new Point(9, 61);
            lblDocRevision.Name = "lblDocRevision";
            lblDocRevision.Size = new Size(64, 15);
            lblDocRevision.TabIndex = 4;
            lblDocRevision.Text = "Revizyon *";
            //
            // txtDocRevision
            //
            txtDocRevision.Anchor = AnchorStyles.Left;
            txtDocRevision.Location = new Point(121, 57);
            txtDocRevision.Name = "txtDocRevision";
            txtDocRevision.Size = new Size(80, 23);
            txtDocRevision.TabIndex = 5;
            //
            // lblDocRevisionDate
            //
            lblDocRevisionDate.Anchor = AnchorStyles.Left;
            lblDocRevisionDate.AutoSize = true;
            lblDocRevisionDate.Location = new Point(9, 88);
            lblDocRevisionDate.Name = "lblDocRevisionDate";
            lblDocRevisionDate.Size = new Size(93, 15);
            lblDocRevisionDate.TabIndex = 6;
            lblDocRevisionDate.Text = "Revizyon Tarihi";
            //
            // dtpDocRevisionDate
            //
            dtpDocRevisionDate.Anchor = AnchorStyles.Left;
            dtpDocRevisionDate.Checked = false;
            dtpDocRevisionDate.CustomFormat = "dd.MM.yyyy";
            dtpDocRevisionDate.Format = DateTimePickerFormat.Custom;
            dtpDocRevisionDate.Location = new Point(121, 84);
            dtpDocRevisionDate.Name = "dtpDocRevisionDate";
            dtpDocRevisionDate.ShowCheckBox = true;
            dtpDocRevisionDate.Size = new Size(140, 23);
            dtpDocRevisionDate.TabIndex = 7;
            //
            // tlpInfo
            //
            tlpInfo.ColumnCount = 2;
            tlpInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 64F));
            tlpInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 36F));
            tlpInfo.Controls.Add(grpProduct, 0, 0);
            tlpInfo.Controls.Add(grpCommercial, 1, 0);
            tlpInfo.Dock = DockStyle.Fill;
            tlpInfo.Location = new Point(10, 82);
            tlpInfo.Margin = new Padding(0, 4, 0, 0);
            tlpInfo.Name = "tlpInfo";
            tlpInfo.RowCount = 1;
            tlpInfo.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpInfo.Size = new Size(1260, 210);
            tlpInfo.TabIndex = 1;
            //
            // grpProduct
            //
            grpProduct.Controls.Add(tlpProduct);
            grpProduct.Dock = DockStyle.Fill;
            grpProduct.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            grpProduct.Location = new Point(0, 0);
            grpProduct.Margin = new Padding(0, 0, 6, 0);
            grpProduct.Name = "grpProduct";
            grpProduct.Padding = new Padding(8, 4, 8, 6);
            grpProduct.Size = new Size(800, 210);
            grpProduct.TabIndex = 0;
            grpProduct.TabStop = false;
            grpProduct.Text = "Ürün Parça Bilgileri";
            //
            // tlpProduct
            //
            tlpProduct.ColumnCount = 4;
            tlpProduct.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 128F));
            tlpProduct.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpProduct.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 128F));
            tlpProduct.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpProduct.Controls.Add(lblProjectNo, 0, 0);
            tlpProduct.Controls.Add(txtProjectNo, 1, 0);
            tlpProduct.Controls.Add(lblInventoryCode, 2, 0);
            tlpProduct.Controls.Add(txtInventoryCode, 3, 0);
            tlpProduct.Controls.Add(lblProductName, 0, 1);
            tlpProduct.Controls.Add(txtProductName, 1, 1);
            tlpProduct.Controls.Add(lblIncomingQty, 2, 1);
            tlpProduct.Controls.Add(tlpQuantity, 3, 1);
            tlpProduct.Controls.Add(lblLotNo, 0, 2);
            tlpProduct.Controls.Add(txtLotNo, 1, 2);
            tlpProduct.Controls.Add(lblDrawingNo, 2, 2);
            tlpProduct.Controls.Add(txtDrawingNo, 3, 2);
            tlpProduct.Controls.Add(lblInspectionDate, 0, 3);
            tlpProduct.Controls.Add(dtpInspectionDate, 1, 3);
            tlpProduct.Controls.Add(lblDrawingRev, 2, 3);
            tlpProduct.Controls.Add(txtDrawingRev, 3, 3);
            tlpProduct.Controls.Add(lblOther, 0, 4);
            tlpProduct.Controls.Add(txtOther, 1, 4);
            tlpProduct.Dock = DockStyle.Fill;
            tlpProduct.Font = new Font("Segoe UI", 9F);
            tlpProduct.Location = new Point(8, 20);
            tlpProduct.Name = "tlpProduct";
            tlpProduct.RowCount = 5;
            tlpProduct.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tlpProduct.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tlpProduct.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tlpProduct.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tlpProduct.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tlpProduct.Size = new Size(784, 184);
            tlpProduct.TabIndex = 0;
            //
            // lblProjectNo
            //
            lblProjectNo.Anchor = AnchorStyles.Left;
            lblProjectNo.AutoSize = true;
            lblProjectNo.Location = new Point(3, 10);
            lblProjectNo.Name = "lblProjectNo";
            lblProjectNo.Size = new Size(86, 15);
            lblProjectNo.TabIndex = 0;
            lblProjectNo.Text = "Proje Numarası";
            //
            // txtProjectNo
            //
            txtProjectNo.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtProjectNo.Location = new Point(131, 6);
            txtProjectNo.Name = "txtProjectNo";
            txtProjectNo.Size = new Size(258, 23);
            txtProjectNo.TabIndex = 1;
            //
            // lblInventoryCode
            //
            lblInventoryCode.Anchor = AnchorStyles.Left;
            lblInventoryCode.AutoSize = true;
            lblInventoryCode.Location = new Point(395, 10);
            lblInventoryCode.Name = "lblInventoryCode";
            lblInventoryCode.Size = new Size(80, 15);
            lblInventoryCode.TabIndex = 2;
            lblInventoryCode.Text = "Envanter Kodu";
            //
            // txtInventoryCode
            //
            txtInventoryCode.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtInventoryCode.Location = new Point(523, 6);
            txtInventoryCode.Name = "txtInventoryCode";
            txtInventoryCode.Size = new Size(258, 23);
            txtInventoryCode.TabIndex = 3;
            //
            // lblProductName
            //
            lblProductName.Anchor = AnchorStyles.Left;
            lblProductName.AutoSize = true;
            lblProductName.Location = new Point(3, 46);
            lblProductName.Name = "lblProductName";
            lblProductName.Size = new Size(96, 15);
            lblProductName.TabIndex = 4;
            lblProductName.Text = "Ürün/Parça Adı *";
            //
            // txtProductName
            //
            txtProductName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtProductName.Location = new Point(131, 42);
            txtProductName.Name = "txtProductName";
            txtProductName.Size = new Size(258, 23);
            txtProductName.TabIndex = 5;
            //
            // lblIncomingQty
            //
            lblIncomingQty.Anchor = AnchorStyles.Left;
            lblIncomingQty.AutoSize = true;
            lblIncomingQty.Location = new Point(395, 46);
            lblIncomingQty.Name = "lblIncomingQty";
            lblIncomingQty.Size = new Size(76, 15);
            lblIncomingQty.TabIndex = 6;
            lblIncomingQty.Text = "Gelen Miktar";
            //
            // tlpQuantity
            //
            tlpQuantity.ColumnCount = 2;
            tlpQuantity.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            tlpQuantity.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tlpQuantity.Controls.Add(txtIncomingQty, 0, 0);
            tlpQuantity.Controls.Add(cmbUnit, 1, 0);
            tlpQuantity.Dock = DockStyle.Fill;
            tlpQuantity.Location = new Point(520, 36);
            tlpQuantity.Margin = new Padding(0);
            tlpQuantity.Name = "tlpQuantity";
            tlpQuantity.RowCount = 1;
            tlpQuantity.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpQuantity.Size = new Size(264, 36);
            tlpQuantity.TabIndex = 7;
            //
            // txtIncomingQty
            //
            txtIncomingQty.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtIncomingQty.Location = new Point(3, 6);
            txtIncomingQty.Name = "txtIncomingQty";
            txtIncomingQty.PlaceholderText = "Miktar";
            txtIncomingQty.Size = new Size(152, 23);
            txtIncomingQty.TabIndex = 0;
            txtIncomingQty.TextAlign = HorizontalAlignment.Right;
            //
            // cmbUnit
            //
            cmbUnit.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cmbUnit.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbUnit.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbUnit.FormattingEnabled = true;
            cmbUnit.Items.AddRange(new object[] { "Adet", "Kg", "g", "m", "mm", "Lt", "Paket", "Kutu", "Set", "Takım", "Rulo" });
            cmbUnit.Location = new Point(161, 6);
            cmbUnit.Name = "cmbUnit";
            cmbUnit.Size = new Size(100, 23);
            cmbUnit.TabIndex = 1;
            //
            // lblLotNo
            //
            lblLotNo.Anchor = AnchorStyles.Left;
            lblLotNo.AutoSize = true;
            lblLotNo.Location = new Point(3, 82);
            lblLotNo.Name = "lblLotNo";
            lblLotNo.Size = new Size(110, 15);
            lblLotNo.TabIndex = 8;
            lblLotNo.Text = "Lot/Parti/Seri No *";
            //
            // txtLotNo
            //
            txtLotNo.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtLotNo.Location = new Point(131, 78);
            txtLotNo.Name = "txtLotNo";
            txtLotNo.Size = new Size(258, 23);
            txtLotNo.TabIndex = 9;
            //
            // lblDrawingNo
            //
            lblDrawingNo.Anchor = AnchorStyles.Left;
            lblDrawingNo.AutoSize = true;
            lblDrawingNo.Location = new Point(395, 82);
            lblDrawingNo.Name = "lblDrawingNo";
            lblDrawingNo.Size = new Size(93, 15);
            lblDrawingNo.TabIndex = 10;
            lblDrawingNo.Text = "Teknik Resim No";
            //
            // txtDrawingNo
            //
            txtDrawingNo.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtDrawingNo.Location = new Point(523, 78);
            txtDrawingNo.Name = "txtDrawingNo";
            txtDrawingNo.Size = new Size(258, 23);
            txtDrawingNo.TabIndex = 11;
            //
            // lblInspectionDate
            //
            lblInspectionDate.Anchor = AnchorStyles.Left;
            lblInspectionDate.AutoSize = true;
            lblInspectionDate.Location = new Point(3, 118);
            lblInspectionDate.Name = "lblInspectionDate";
            lblInspectionDate.Size = new Size(97, 15);
            lblInspectionDate.TabIndex = 12;
            lblInspectionDate.Text = "Muayene Tarihi *";
            //
            // dtpInspectionDate
            //
            dtpInspectionDate.Anchor = AnchorStyles.Left;
            dtpInspectionDate.CustomFormat = "dd.MM.yyyy";
            dtpInspectionDate.Format = DateTimePickerFormat.Custom;
            dtpInspectionDate.Location = new Point(131, 114);
            dtpInspectionDate.Name = "dtpInspectionDate";
            dtpInspectionDate.Size = new Size(140, 23);
            dtpInspectionDate.TabIndex = 13;
            //
            // lblDrawingRev
            //
            lblDrawingRev.Anchor = AnchorStyles.Left;
            lblDrawingRev.AutoSize = true;
            lblDrawingRev.Location = new Point(395, 118);
            lblDrawingRev.Name = "lblDrawingRev";
            lblDrawingRev.Size = new Size(96, 15);
            lblDrawingRev.TabIndex = 14;
            lblDrawingRev.Text = "Teknik Resim Rev";
            //
            // txtDrawingRev
            //
            txtDrawingRev.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtDrawingRev.Location = new Point(523, 114);
            txtDrawingRev.Name = "txtDrawingRev";
            txtDrawingRev.Size = new Size(258, 23);
            txtDrawingRev.TabIndex = 15;
            //
            // lblOther
            //
            lblOther.Anchor = AnchorStyles.Left;
            lblOther.AutoSize = true;
            lblOther.Location = new Point(3, 156);
            lblOther.Name = "lblOther";
            lblOther.Size = new Size(34, 15);
            lblOther.TabIndex = 16;
            lblOther.Text = "Diğer";
            //
            // txtOther
            //
            txtOther.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            tlpProduct.SetColumnSpan(txtOther, 3);
            txtOther.Location = new Point(131, 152);
            txtOther.Name = "txtOther";
            txtOther.Size = new Size(650, 23);
            txtOther.TabIndex = 17;
            //
            // grpCommercial
            //
            grpCommercial.Controls.Add(tlpCommercial);
            grpCommercial.Dock = DockStyle.Fill;
            grpCommercial.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            grpCommercial.Location = new Point(806, 0);
            grpCommercial.Margin = new Padding(0);
            grpCommercial.Name = "grpCommercial";
            grpCommercial.Padding = new Padding(8, 4, 8, 6);
            grpCommercial.Size = new Size(454, 210);
            grpCommercial.TabIndex = 1;
            grpCommercial.TabStop = false;
            grpCommercial.Text = "Ticari Bilgiler";
            //
            // tlpCommercial
            //
            tlpCommercial.ColumnCount = 2;
            tlpCommercial.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 128F));
            tlpCommercial.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpCommercial.Controls.Add(lblWaybillNo, 0, 0);
            tlpCommercial.Controls.Add(txtWaybillNo, 1, 0);
            tlpCommercial.Controls.Add(lblPoNo, 0, 1);
            tlpCommercial.Controls.Add(txtPoNo, 1, 1);
            tlpCommercial.Controls.Add(lblArrivalDate, 0, 2);
            tlpCommercial.Controls.Add(dtpArrivalDate, 1, 2);
            tlpCommercial.Controls.Add(lblInventoryGroup, 0, 3);
            tlpCommercial.Controls.Add(cmbInventoryGroup, 1, 3);
            tlpCommercial.Controls.Add(lblSupplier, 0, 4);
            tlpCommercial.Controls.Add(cmbSupplier, 1, 4);
            tlpCommercial.Dock = DockStyle.Fill;
            tlpCommercial.Font = new Font("Segoe UI", 9F);
            tlpCommercial.Location = new Point(8, 20);
            tlpCommercial.Name = "tlpCommercial";
            tlpCommercial.RowCount = 5;
            tlpCommercial.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tlpCommercial.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tlpCommercial.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tlpCommercial.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tlpCommercial.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tlpCommercial.Size = new Size(438, 184);
            tlpCommercial.TabIndex = 0;
            //
            // lblWaybillNo
            //
            lblWaybillNo.Anchor = AnchorStyles.Left;
            lblWaybillNo.AutoSize = true;
            lblWaybillNo.Location = new Point(3, 10);
            lblWaybillNo.Name = "lblWaybillNo";
            lblWaybillNo.Size = new Size(101, 15);
            lblWaybillNo.TabIndex = 0;
            lblWaybillNo.Text = "İrsaliye Numarası";
            //
            // txtWaybillNo
            //
            txtWaybillNo.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtWaybillNo.Location = new Point(131, 6);
            txtWaybillNo.Name = "txtWaybillNo";
            txtWaybillNo.Size = new Size(304, 23);
            txtWaybillNo.TabIndex = 1;
            //
            // lblPoNo
            //
            lblPoNo.Anchor = AnchorStyles.Left;
            lblPoNo.AutoSize = true;
            lblPoNo.Location = new Point(3, 46);
            lblPoNo.Name = "lblPoNo";
            lblPoNo.Size = new Size(122, 15);
            lblPoNo.TabIndex = 2;
            lblPoNo.Text = "Sipariş Emri Numarası";
            //
            // txtPoNo
            //
            txtPoNo.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtPoNo.Location = new Point(131, 42);
            txtPoNo.Name = "txtPoNo";
            txtPoNo.Size = new Size(304, 23);
            txtPoNo.TabIndex = 3;
            //
            // lblArrivalDate
            //
            lblArrivalDate.Anchor = AnchorStyles.Left;
            lblArrivalDate.AutoSize = true;
            lblArrivalDate.Location = new Point(3, 82);
            lblArrivalDate.Name = "lblArrivalDate";
            lblArrivalDate.Size = new Size(68, 15);
            lblArrivalDate.TabIndex = 4;
            lblArrivalDate.Text = "Geliş Tarihi";
            //
            // dtpArrivalDate
            //
            dtpArrivalDate.Anchor = AnchorStyles.Left;
            dtpArrivalDate.Checked = false;
            dtpArrivalDate.CustomFormat = "dd.MM.yyyy";
            dtpArrivalDate.Format = DateTimePickerFormat.Custom;
            dtpArrivalDate.Location = new Point(131, 78);
            dtpArrivalDate.Name = "dtpArrivalDate";
            dtpArrivalDate.ShowCheckBox = true;
            dtpArrivalDate.Size = new Size(140, 23);
            dtpArrivalDate.TabIndex = 5;
            //
            // lblInventoryGroup
            //
            lblInventoryGroup.Anchor = AnchorStyles.Left;
            lblInventoryGroup.AutoSize = true;
            lblInventoryGroup.Location = new Point(3, 118);
            lblInventoryGroup.Name = "lblInventoryGroup";
            lblInventoryGroup.Size = new Size(87, 15);
            lblInventoryGroup.TabIndex = 6;
            lblInventoryGroup.Text = "Envanter Grubu";
            //
            // cmbInventoryGroup
            //
            cmbInventoryGroup.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cmbInventoryGroup.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbInventoryGroup.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbInventoryGroup.FormattingEnabled = true;
            cmbInventoryGroup.Location = new Point(131, 114);
            cmbInventoryGroup.Name = "cmbInventoryGroup";
            cmbInventoryGroup.Size = new Size(304, 23);
            cmbInventoryGroup.TabIndex = 7;
            //
            // lblSupplier
            //
            lblSupplier.Anchor = AnchorStyles.Left;
            lblSupplier.AutoSize = true;
            lblSupplier.Location = new Point(3, 156);
            lblSupplier.Name = "lblSupplier";
            lblSupplier.Size = new Size(88, 15);
            lblSupplier.TabIndex = 8;
            lblSupplier.Text = "Tedarikçi Adı *";
            //
            // cmbSupplier
            //
            cmbSupplier.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cmbSupplier.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbSupplier.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbSupplier.FormattingEnabled = true;
            cmbSupplier.Location = new Point(131, 152);
            cmbSupplier.Name = "cmbSupplier";
            cmbSupplier.Size = new Size(304, 23);
            cmbSupplier.TabIndex = 9;
            //
            // grpItems
            //
            grpItems.Controls.Add(dgvItems);
            grpItems.Dock = DockStyle.Fill;
            grpItems.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            grpItems.Location = new Point(10, 296);
            grpItems.Margin = new Padding(0, 4, 0, 0);
            grpItems.Name = "grpItems";
            grpItems.Padding = new Padding(8, 4, 8, 8);
            grpItems.Size = new Size(1260, 346);
            grpItems.TabIndex = 2;
            grpItems.TabStop = false;
            grpItems.Text = "Muayene Sonuçları  (her satırda Kabul / Şartlı Kabul / Red kutucuklarından birini işaretleyin)";
            //
            // dgvItems
            //
            dgvItems.AllowUserToAddRows = false;
            dgvItems.AllowUserToDeleteRows = false;
            dgvItems.AllowUserToOrderColumns = false;
            dgvItems.AllowUserToResizeRows = false;
            dgvItems.BackgroundColor = Color.White;
            itemsHeaderStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            itemsHeaderStyle.BackColor = Color.FromArgb(217, 217, 217);
            itemsHeaderStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            itemsHeaderStyle.ForeColor = Color.Black;
            itemsHeaderStyle.SelectionBackColor = Color.FromArgb(217, 217, 217);
            itemsHeaderStyle.SelectionForeColor = Color.Black;
            itemsHeaderStyle.WrapMode = DataGridViewTriState.True;
            dgvItems.ColumnHeadersDefaultCellStyle = itemsHeaderStyle;
            dgvItems.ColumnHeadersHeight = 36;
            dgvItems.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvItems.Columns.AddRange(new DataGridViewColumn[] { colRowNo, colInspectionType, colAcceptCriteria, colApplied, colAccepted, colConditional, colRejected, colStatement });
            itemsDefaultStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            itemsDefaultStyle.BackColor = Color.White;
            itemsDefaultStyle.Font = new Font("Segoe UI", 9F);
            itemsDefaultStyle.ForeColor = Color.Black;
            itemsDefaultStyle.SelectionBackColor = Color.FromArgb(204, 228, 247);
            itemsDefaultStyle.SelectionForeColor = Color.Black;
            itemsDefaultStyle.WrapMode = DataGridViewTriState.False;
            dgvItems.DefaultCellStyle = itemsDefaultStyle;
            dgvItems.Dock = DockStyle.Fill;
            dgvItems.EditMode = DataGridViewEditMode.EditOnEnter;
            dgvItems.EnableHeadersVisualStyles = false;
            dgvItems.Location = new Point(8, 20);
            dgvItems.MultiSelect = false;
            dgvItems.Name = "dgvItems";
            dgvItems.RowHeadersVisible = false;
            dgvItems.RowTemplate.Height = 30;
            dgvItems.ScrollBars = ScrollBars.Vertical;
            dgvItems.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgvItems.Size = new Size(1244, 318);
            dgvItems.TabIndex = 0;
            dgvItems.CellValueChanged += dgvItems_CellValueChanged;
            dgvItems.CurrentCellDirtyStateChanged += dgvItems_CurrentCellDirtyStateChanged;
            dgvItems.DataError += dgvItems_DataError;
            //
            // colRowNo
            //
            rowNoCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            rowNoCellStyle.BackColor = Color.FromArgb(242, 242, 242);
            colRowNo.DefaultCellStyle = rowNoCellStyle;
            colRowNo.HeaderText = "No";
            colRowNo.Name = "colRowNo";
            colRowNo.ReadOnly = true;
            colRowNo.Resizable = DataGridViewTriState.False;
            colRowNo.SortMode = DataGridViewColumnSortMode.NotSortable;
            colRowNo.Width = 44;
            //
            // colInspectionType
            //
            typeCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            colInspectionType.DefaultCellStyle = typeCellStyle;
            colInspectionType.HeaderText = "Muayene Tipi";
            colInspectionType.Name = "colInspectionType";
            colInspectionType.SortMode = DataGridViewColumnSortMode.NotSortable;
            colInspectionType.Width = 250;
            //
            // colAcceptCriteria
            //
            colAcceptCriteria.HeaderText = "Kabul Kriteri";
            colAcceptCriteria.Name = "colAcceptCriteria";
            colAcceptCriteria.SortMode = DataGridViewColumnSortMode.NotSortable;
            colAcceptCriteria.Width = 200;
            //
            // colApplied
            //
            colApplied.HeaderText = "Uygulanan";
            colApplied.Name = "colApplied";
            colApplied.SortMode = DataGridViewColumnSortMode.NotSortable;
            colApplied.Width = 200;
            //
            // colAccepted
            //
            colAccepted.FalseValue = false;
            colAccepted.HeaderText = "Kabul";
            colAccepted.Name = "colAccepted";
            colAccepted.Resizable = DataGridViewTriState.False;
            colAccepted.TrueValue = true;
            colAccepted.Width = 65;
            //
            // colConditional
            //
            colConditional.FalseValue = false;
            colConditional.HeaderText = "Şartlı Kabul";
            colConditional.Name = "colConditional";
            colConditional.Resizable = DataGridViewTriState.False;
            colConditional.TrueValue = true;
            colConditional.Width = 85;
            //
            // colRejected
            //
            colRejected.FalseValue = false;
            colRejected.HeaderText = "Red";
            colRejected.Name = "colRejected";
            colRejected.Resizable = DataGridViewTriState.False;
            colRejected.TrueValue = true;
            colRejected.Width = 60;
            //
            // colStatement
            //
            colStatement.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colStatement.HeaderText = "Açıklama";
            colStatement.MinimumWidth = 150;
            colStatement.Name = "colStatement";
            colStatement.SortMode = DataGridViewColumnSortMode.NotSortable;
            //
            // tlpBottom
            //
            tlpBottom.ColumnCount = 3;
            tlpBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 38F));
            tlpBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 38F));
            tlpBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 24F));
            tlpBottom.Controls.Add(grpNcr, 0, 0);
            tlpBottom.Controls.Add(grpNotes, 1, 0);
            tlpBottom.Controls.Add(grpInspector, 2, 0);
            tlpBottom.Dock = DockStyle.Fill;
            tlpBottom.Location = new Point(10, 646);
            tlpBottom.Margin = new Padding(0, 4, 0, 0);
            tlpBottom.Name = "tlpBottom";
            tlpBottom.RowCount = 1;
            tlpBottom.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpBottom.Size = new Size(1260, 136);
            tlpBottom.TabIndex = 3;
            //
            // grpNcr
            //
            grpNcr.Controls.Add(txtNcr);
            grpNcr.Dock = DockStyle.Fill;
            grpNcr.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            grpNcr.Location = new Point(0, 0);
            grpNcr.Margin = new Padding(0, 0, 6, 0);
            grpNcr.Name = "grpNcr";
            grpNcr.Padding = new Padding(8, 4, 8, 8);
            grpNcr.Size = new Size(472, 136);
            grpNcr.TabIndex = 0;
            grpNcr.TabStop = false;
            grpNcr.Text = "NCR  (Red kararında zorunlu)";
            //
            // txtNcr
            //
            txtNcr.AcceptsReturn = true;
            txtNcr.Dock = DockStyle.Fill;
            txtNcr.Font = new Font("Segoe UI", 9F);
            txtNcr.Location = new Point(8, 20);
            txtNcr.Multiline = true;
            txtNcr.Name = "txtNcr";
            txtNcr.ScrollBars = ScrollBars.Vertical;
            txtNcr.Size = new Size(456, 108);
            txtNcr.TabIndex = 0;
            //
            // grpNotes
            //
            grpNotes.Controls.Add(txtNotes);
            grpNotes.Dock = DockStyle.Fill;
            grpNotes.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            grpNotes.Location = new Point(478, 0);
            grpNotes.Margin = new Padding(0, 0, 6, 0);
            grpNotes.Name = "grpNotes";
            grpNotes.Padding = new Padding(8, 4, 8, 8);
            grpNotes.Size = new Size(472, 136);
            grpNotes.TabIndex = 1;
            grpNotes.TabStop = false;
            grpNotes.Text = "Notlar";
            //
            // txtNotes
            //
            txtNotes.AcceptsReturn = true;
            txtNotes.Dock = DockStyle.Fill;
            txtNotes.Font = new Font("Segoe UI", 9F);
            txtNotes.Location = new Point(8, 20);
            txtNotes.Multiline = true;
            txtNotes.Name = "txtNotes";
            txtNotes.ScrollBars = ScrollBars.Vertical;
            txtNotes.Size = new Size(456, 108);
            txtNotes.TabIndex = 0;
            //
            // grpInspector
            //
            grpInspector.Controls.Add(tlpInspector);
            grpInspector.Dock = DockStyle.Fill;
            grpInspector.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            grpInspector.Location = new Point(956, 0);
            grpInspector.Margin = new Padding(0);
            grpInspector.Name = "grpInspector";
            grpInspector.Padding = new Padding(8, 4, 8, 8);
            grpInspector.Size = new Size(304, 136);
            grpInspector.TabIndex = 2;
            grpInspector.TabStop = false;
            grpInspector.Text = "Muayene Eden";
            //
            // tlpInspector
            //
            tlpInspector.ColumnCount = 1;
            tlpInspector.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpInspector.Controls.Add(lblInspector, 0, 0);
            tlpInspector.Controls.Add(cmbInspector, 0, 1);
            tlpInspector.Controls.Add(lblSignatureHint, 0, 2);
            tlpInspector.Dock = DockStyle.Fill;
            tlpInspector.Font = new Font("Segoe UI", 9F);
            tlpInspector.Location = new Point(8, 20);
            tlpInspector.Name = "tlpInspector";
            tlpInspector.RowCount = 3;
            tlpInspector.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpInspector.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpInspector.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpInspector.Size = new Size(288, 108);
            tlpInspector.TabIndex = 0;
            //
            // lblInspector
            //
            lblInspector.AutoSize = true;
            lblInspector.Location = new Point(3, 3);
            lblInspector.Margin = new Padding(3, 3, 3, 2);
            lblInspector.Name = "lblInspector";
            lblInspector.Size = new Size(64, 15);
            lblInspector.TabIndex = 0;
            lblInspector.Text = "Adı Soyadı";
            //
            // cmbInspector
            //
            cmbInspector.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cmbInspector.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbInspector.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbInspector.FormattingEnabled = true;
            cmbInspector.Location = new Point(3, 23);
            cmbInspector.Name = "cmbInspector";
            cmbInspector.Size = new Size(282, 23);
            cmbInspector.TabIndex = 1;
            //
            // lblSignatureHint
            //
            lblSignatureHint.Dock = DockStyle.Fill;
            lblSignatureHint.ForeColor = Color.DimGray;
            lblSignatureHint.Location = new Point(3, 52);
            lblSignatureHint.Name = "lblSignatureHint";
            lblSignatureHint.Size = new Size(282, 56);
            lblSignatureHint.TabIndex = 2;
            lblSignatureHint.Text = "İmza alanı PDF çıktısında ıslak imza için boş bırakılır.";
            lblSignatureHint.TextAlign = ContentAlignment.MiddleLeft;
            //
            // tlpButtons
            //
            tlpButtons.ColumnCount = 2;
            tlpButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpButtons.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tlpButtons.Controls.Add(lblStatus, 0, 0);
            tlpButtons.Controls.Add(flpButtons, 1, 0);
            tlpButtons.Dock = DockStyle.Fill;
            tlpButtons.Location = new Point(10, 782);
            tlpButtons.Margin = new Padding(0, 4, 0, 0);
            tlpButtons.Name = "tlpButtons";
            tlpButtons.RowCount = 1;
            tlpButtons.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpButtons.Size = new Size(1260, 44);
            tlpButtons.TabIndex = 4;
            //
            // lblStatus
            //
            lblStatus.Anchor = AnchorStyles.Left;
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblStatus.Location = new Point(3, 13);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(160, 17);
            lblStatus.TabIndex = 0;
            lblStatus.Text = "Yeni kayıt";
            //
            // flpButtons
            //
            flpButtons.Anchor = AnchorStyles.Right;
            flpButtons.AutoSize = true;
            flpButtons.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flpButtons.Controls.Add(btnSave);
            flpButtons.Controls.Add(btnPrint);
            flpButtons.Controls.Add(btnClose);
            flpButtons.Location = new Point(780, 2);
            flpButtons.Margin = new Padding(0);
            flpButtons.Name = "flpButtons";
            flpButtons.Size = new Size(480, 40);
            flpButtons.TabIndex = 1;
            flpButtons.WrapContents = false;
            //
            // btnSave
            //
            btnSave.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSave.Location = new Point(3, 3);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(140, 34);
            btnSave.TabIndex = 0;
            btnSave.Text = "Kaydet  (Ctrl+S)";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            //
            // btnPrint
            //
            btnPrint.BackColor = Color.FromArgb(13, 27, 62);
            btnPrint.FlatAppearance.BorderColor = Color.FromArgb(13, 27, 62);
            btnPrint.FlatStyle = FlatStyle.Flat;
            btnPrint.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnPrint.ForeColor = Color.White;
            btnPrint.Location = new Point(149, 3);
            btnPrint.Name = "btnPrint";
            btnPrint.Size = new Size(210, 34);
            btnPrint.TabIndex = 1;
            btnPrint.Text = "Kaydet ve PDF Al  (Ctrl+P)";
            btnPrint.UseVisualStyleBackColor = false;
            btnPrint.Click += btnPrint_Click;
            //
            // btnClose
            //
            btnClose.Location = new Point(365, 3);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(112, 34);
            btnClose.TabIndex = 2;
            btnClose.Text = "Kapat";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            //
            // errorProvider
            //
            errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            errorProvider.ContainerControl = this;
            //
            // InspectionEditForm
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnClose;
            ClientSize = new Size(1280, 840);
            Controls.Add(tlpMain);
            KeyPreview = true;
            MinimizeBox = false;
            MinimumSize = new Size(1150, 780);
            Name = "InspectionEditForm";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Girdi Muayene Formu";
            tlpMain.ResumeLayout(false);
            tlpHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picLogo).EndInit();
            tlpDocInfo.ResumeLayout(false);
            tlpDocInfo.PerformLayout();
            tlpInfo.ResumeLayout(false);
            grpProduct.ResumeLayout(false);
            tlpProduct.ResumeLayout(false);
            tlpProduct.PerformLayout();
            tlpQuantity.ResumeLayout(false);
            tlpQuantity.PerformLayout();
            grpCommercial.ResumeLayout(false);
            tlpCommercial.ResumeLayout(false);
            tlpCommercial.PerformLayout();
            grpItems.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvItems).EndInit();
            tlpBottom.ResumeLayout(false);
            grpNcr.ResumeLayout(false);
            grpNcr.PerformLayout();
            grpNotes.ResumeLayout(false);
            grpNotes.PerformLayout();
            grpInspector.ResumeLayout(false);
            tlpInspector.ResumeLayout(false);
            tlpInspector.PerformLayout();
            tlpButtons.ResumeLayout(false);
            tlpButtons.PerformLayout();
            flpButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tlpMain;
        private TableLayoutPanel tlpHeader;
        private PictureBox picLogo;
        private Label lblTitle;
        private Label lblTitleEn;
        private TableLayoutPanel tlpDocInfo;
        private Label lblDocumentNo;
        private TextBox txtDocumentNo;
        private Label lblDocPublishDate;
        private DateTimePicker dtpDocPublishDate;
        private Label lblDocRevision;
        private TextBox txtDocRevision;
        private Label lblDocRevisionDate;
        private DateTimePicker dtpDocRevisionDate;
        private TableLayoutPanel tlpInfo;
        private GroupBox grpProduct;
        private TableLayoutPanel tlpProduct;
        private Label lblProjectNo;
        private TextBox txtProjectNo;
        private Label lblInventoryCode;
        private TextBox txtInventoryCode;
        private Label lblProductName;
        private TextBox txtProductName;
        private Label lblIncomingQty;
        private TableLayoutPanel tlpQuantity;
        private TextBox txtIncomingQty;
        private ComboBox cmbUnit;
        private Label lblLotNo;
        private TextBox txtLotNo;
        private Label lblDrawingNo;
        private TextBox txtDrawingNo;
        private Label lblInspectionDate;
        private DateTimePicker dtpInspectionDate;
        private Label lblDrawingRev;
        private TextBox txtDrawingRev;
        private Label lblOther;
        private TextBox txtOther;
        private GroupBox grpCommercial;
        private TableLayoutPanel tlpCommercial;
        private Label lblWaybillNo;
        private TextBox txtWaybillNo;
        private Label lblPoNo;
        private TextBox txtPoNo;
        private Label lblArrivalDate;
        private DateTimePicker dtpArrivalDate;
        private Label lblInventoryGroup;
        private ComboBox cmbInventoryGroup;
        private Label lblSupplier;
        private ComboBox cmbSupplier;
        private GroupBox grpItems;
        private DataGridView dgvItems;
        private DataGridViewTextBoxColumn colRowNo;
        private DataGridViewTextBoxColumn colInspectionType;
        private DataGridViewTextBoxColumn colAcceptCriteria;
        private DataGridViewTextBoxColumn colApplied;
        private DataGridViewCheckBoxColumn colAccepted;
        private DataGridViewCheckBoxColumn colConditional;
        private DataGridViewCheckBoxColumn colRejected;
        private DataGridViewTextBoxColumn colStatement;
        private TableLayoutPanel tlpBottom;
        private GroupBox grpNcr;
        private TextBox txtNcr;
        private GroupBox grpNotes;
        private TextBox txtNotes;
        private GroupBox grpInspector;
        private TableLayoutPanel tlpInspector;
        private Label lblInspector;
        private ComboBox cmbInspector;
        private Label lblSignatureHint;
        private TableLayoutPanel tlpButtons;
        private Label lblStatus;
        private FlowLayoutPanel flpButtons;
        private Button btnSave;
        private Button btnPrint;
        private Button btnClose;
        private ErrorProvider errorProvider;
        private ToolTip toolTip;
    }
}
