/* =====================================================================
   DIGITEST - Girdi Muayene ve Sonuç Formu (FR-QC-009)
   02 - Tablolar

   Bu scripti DigitestQC veritabanı seçiliyken çalıştırın.
   Script tekrar çalıştırılabilir (var olan tabloları bozmaz).
   ===================================================================== */

IF DB_NAME() IN (N'master', N'model', N'msdb', N'tempdb')
BEGIN
    RAISERROR (N'Bu script sistem veritabanında çalıştırılamaz. Önce DigitestQC veritabanını seçin.', 16, 1);
    SET NOEXEC ON;
END;
GO

/* ---------------------------------------------------------------------
   Sabit muayene satırları şablonu (Görsel Muayene, Miktar, ...)
   Yeni form açıldığında satırlar buradan gelir.
   --------------------------------------------------------------------- */
IF OBJECT_ID(N'dbo.InspectionTemplateItem', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.InspectionTemplateItem
    (
        RowNo               INT             NOT NULL
            CONSTRAINT PK_InspectionTemplateItem PRIMARY KEY
            CONSTRAINT CK_InspectionTemplateItem_RowNo CHECK (RowNo BETWEEN 1 AND 7),
        InspectionTypeTr    NVARCHAR(100)   NOT NULL,
        InspectionTypeEn    NVARCHAR(100)   NULL,
        AcceptCriteria      NVARCHAR(200)   NULL,
        IsActive            BIT             NOT NULL
            CONSTRAINT DF_InspectionTemplateItem_IsActive DEFAULT (1)
    );
END;
GO

/* ---------------------------------------------------------------------
   Yıllık kayıt numarası sayacı (GM-2026-0001, GM-2026-0002, ...)
   --------------------------------------------------------------------- */
IF OBJECT_ID(N'dbo.FormNumberCounter', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.FormNumberCounter
    (
        [Year]      SMALLINT    NOT NULL CONSTRAINT PK_FormNumberCounter PRIMARY KEY,
        LastNo      INT         NOT NULL
    );
END;
GO

/* ---------------------------------------------------------------------
   Form başlığı: Ürün Parça Bilgileri + Ticari Bilgiler + sonuç alanları
   --------------------------------------------------------------------- */
IF OBJECT_ID(N'dbo.InspectionForm', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.InspectionForm
    (
        Id                      INT IDENTITY(1, 1) NOT NULL
            CONSTRAINT PK_InspectionForm PRIMARY KEY,
        FormNo                  NVARCHAR(20)    NOT NULL,

        -- Ürün Parça Bilgileri
        ProjectNo               NVARCHAR(50)    NULL,
        ProductName             NVARCHAR(200)   NOT NULL,
        LotNo                   NVARCHAR(100)   NOT NULL,
        InspectionDate          DATE            NOT NULL,
        InventoryCode           NVARCHAR(50)    NULL,
        IncomingQuantity        DECIMAL(18, 3)  NULL,
        QuantityUnit            NVARCHAR(20)    NULL,
        TechnicalDrawingNo      NVARCHAR(100)   NULL,
        TechnicalDrawingRev     NVARCHAR(20)    NULL,
        Other                   NVARCHAR(500)   NULL,

        -- Ticari Bilgiler
        WaybillNo               NVARCHAR(50)    NULL,
        PurchaseOrderNo         NVARCHAR(50)    NULL,
        ArrivalDate             DATE            NULL,
        InventoryGroup          NVARCHAR(100)   NULL,
        SupplierName            NVARCHAR(200)   NOT NULL,

        -- Sonuç
        NcrText                 NVARCHAR(1000)  NULL,
        Notes                   NVARCHAR(1000)  NULL,
        InspectorName           NVARCHAR(100)   NULL,
        OverallResult           TINYINT         NOT NULL
            CONSTRAINT DF_InspectionForm_OverallResult DEFAULT (0),

        -- Formun oluşturulduğu andaki doküman bilgisi (snapshot)
        DocumentNo              NVARCHAR(30)    NOT NULL,
        DocPublishDate          DATE            NOT NULL,
        DocRevision             NVARCHAR(10)    NOT NULL,
        DocRevisionDate         DATE            NULL,

        CreatedAt               DATETIME2(0)    NOT NULL
            CONSTRAINT DF_InspectionForm_CreatedAt DEFAULT (SYSDATETIME()),
        UpdatedAt               DATETIME2(0)    NULL,

        CONSTRAINT UQ_InspectionForm_FormNo UNIQUE (FormNo),
        CONSTRAINT CK_InspectionForm_OverallResult CHECK (OverallResult BETWEEN 0 AND 3),
        CONSTRAINT CK_InspectionForm_IncomingQuantity CHECK (IncomingQuantity IS NULL OR IncomingQuantity >= 0)
    );

    CREATE INDEX IX_InspectionForm_InspectionDate ON dbo.InspectionForm (InspectionDate DESC, Id DESC);
    CREATE INDEX IX_InspectionForm_SupplierName   ON dbo.InspectionForm (SupplierName);
    CREATE INDEX IX_InspectionForm_LotNo          ON dbo.InspectionForm (LotNo);
END;
GO

/* ---------------------------------------------------------------------
   Muayene satırları (formdaki 7 satırlık tablo)
   Decision: 0 = Seçilmedi, 1 = Kabul, 2 = Şartlı Kabul, 3 = Red
   --------------------------------------------------------------------- */
IF OBJECT_ID(N'dbo.InspectionItem', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.InspectionItem
    (
        Id                  INT IDENTITY(1, 1) NOT NULL
            CONSTRAINT PK_InspectionItem PRIMARY KEY,
        FormId              INT             NOT NULL
            CONSTRAINT FK_InspectionItem_InspectionForm
            REFERENCES dbo.InspectionForm (Id) ON DELETE CASCADE,
        RowNo               INT             NOT NULL,
        IsFixed             BIT             NOT NULL,
        InspectionTypeTr    NVARCHAR(100)   NOT NULL,
        InspectionTypeEn    NVARCHAR(100)   NULL,
        AcceptCriteria      NVARCHAR(200)   NULL,
        Applied             NVARCHAR(200)   NULL,
        Decision            TINYINT         NOT NULL
            CONSTRAINT DF_InspectionItem_Decision DEFAULT (0),
        Statement           NVARCHAR(500)   NULL,

        CONSTRAINT UQ_InspectionItem_FormId_RowNo UNIQUE (FormId, RowNo),
        CONSTRAINT CK_InspectionItem_RowNo CHECK (RowNo BETWEEN 1 AND 7),
        CONSTRAINT CK_InspectionItem_Decision CHECK (Decision BETWEEN 0 AND 3)
    );
END;
GO

SET NOEXEC OFF;
GO
