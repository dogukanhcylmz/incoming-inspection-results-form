/* =====================================================================
   DIGITEST - Girdi Muayene ve Sonuç Formu (FR-QC-009)
   01 - Veritabanı oluşturma

   NOT: Uygulama ilk çalıştığında (appsettings.json -> Database:AutoCreate
   true ise) veritabanını ve tabloları kendisi oluşturur. Bu scriptler
   elle kurulum yapmak isteyenler içindir.

   Sıra: 01 (master üzerinde) -> 02 ve 03 (DigitestQC seçiliyken)
   ===================================================================== */

IF DB_ID(N'DigitestQC') IS NULL
BEGIN
    -- Turkish_CI_AS: Türkçe sıralama ve i/İ - ı/I harflerinin doğru aranması için
    CREATE DATABASE DigitestQC COLLATE Turkish_CI_AS;
END;
GO
