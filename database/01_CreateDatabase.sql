/* Veritabanı oluşturma*/

IF DB_ID(N'DigitestQC') IS NULL
BEGIN
    CREATE DATABASE DigitestQC COLLATE Turkish_CI_AS;
END;
GO
