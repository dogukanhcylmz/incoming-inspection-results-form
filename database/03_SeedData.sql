/* =====================================================================
   DIGITEST - Girdi Muayene ve Sonuç Formu (FR-QC-009)
   03 - Başlangıç verileri

   Bu scripti DigitestQC veritabanı seçiliyken çalıştırın.
   Var olan kayıtların üzerine YAZMAZ; sadece eksikleri ekler.
   ===================================================================== */

IF DB_NAME() IN (N'master', N'model', N'msdb', N'tempdb')
BEGIN
    RAISERROR (N'Bu script sistem veritabanında çalıştırılamaz. Önce DigitestQC veritabanını seçin.', 16, 1);
    SET NOEXEC ON;
END;
GO

-- Doküman bilgisi: FR-QC-009, Yayın 01.06.2026, Revizyon 00
IF NOT EXISTS (SELECT 1 FROM dbo.FormDocumentInfo WHERE Id = 1)
BEGIN
    INSERT INTO dbo.FormDocumentInfo (Id, DocumentNo, PublishDate, Revision, RevisionDate)
    VALUES (1, N'FR-QC-009', '2026-06-01', N'00', NULL);
END;
GO

-- Sabit muayene satırları (7. satır serbest bırakılmıştır)
INSERT INTO dbo.InspectionTemplateItem (RowNo, InspectionTypeTr, InspectionTypeEn, AcceptCriteria)
SELECT v.RowNo, v.InspectionTypeTr, v.InspectionTypeEn, v.AcceptCriteria
FROM (VALUES
        (1, N'Görsel Muayene',      N'Visual Inspection',   N'Genel Kontrol'),
        (2, N'Miktar',              N'Quantity',            N'Nicel Kontrol'),
        (3, N'Doküman Kontrolü',    N'Document Inspection', N'(CoC, CoA)'),
        (4, N'Paketleme Kontrolü',  N'Packing Inspection',  N'Uygun'),
        (5, N'Onaylı Tedarikçi mi?',N'Approved Supplier?',  N'Evet'),
        (6, N'Ölçüm',               N'Measurement',         N'Örnekleme Büyüklüğü: ')
     ) AS v (RowNo, InspectionTypeTr, InspectionTypeEn, AcceptCriteria)
WHERE NOT EXISTS (SELECT 1 FROM dbo.InspectionTemplateItem t WHERE t.RowNo = v.RowNo);
GO

SET NOEXEC OFF;
GO
