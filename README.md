# DIGITEST – Girdi Muayene ve Sonuç Formu (FR-QC-009)

Kağıt/Excel olarak kullanılan **Girdi Muayene ve Sonuç Formu**nun Windows masaüstü uygulaması.
Form ekrandan doldurulur, SQL Server'a kaydedilir, istenildiğinde dolu haliyle tekrar açılır ve
**kağıt formun birebir aynısı** olarak PDF çıktısı alınır.

---

## Özellikler

- **Kayıt listesi:** tarih aralığı, kayıt no, tedarikçi, ürün, lot no ve genel sonuca göre arama
- **Form giriş ekranı:** kağıt formdaki tüm alanlar (Ürün Parça Bilgileri, Ticari Bilgiler, 7 satırlık muayene tablosu, NCR, Notlar, Adı Soyadı)
- Sabit muayene satırları (Görsel Muayene, Miktar, Doküman Kontrolü, Paketleme Kontrolü, Onaylı Tedarikçi mi?, Ölçüm) yeni formda hazır gelir; 7. satır serbesttir
- Her satırda **Kabul / Şartlı Kabul / Red** kutucuklarından yalnızca biri seçilebilir
- **Genel sonuç** satır kararlarından otomatik hesaplanır (Red > Beklemede > Şartlı Kabul > Kabul)
- Doğrulamalar: zorunlu alanlar, Red kararında NCR zorunluluğu, geliş tarihi ≤ muayene tarihi, alan uzunlukları
- Otomatik kayıt numarası: `GM-2026-0001`, `GM-2026-0002`, … (her yıl sıfırlanır)
- **PDF çıktısı (QuestPDF):** A4 yatay, DIGITEST logolu, kağıt formun düzeninde; imza alanı ıslak imza için boş
- Doküman bilgileri (Doküman No, Yayın Tarihi, Revizyon, Revizyon Tarihi) form ekranının sağ üst köşesinden **her forma özel** yazılabilir. Yeni formda **Doküman Bilgileri** ekranındaki varsayılan değerler hazır gelir. Her form kendi doküman bilgisini saklar ve PDF'e onunla basılır
- Daha önce girilen tedarikçi, envanter grubu, birim ve muayene eden adları açılır kutularda öneri olarak gelir
- İlk açılışta veritabanı ve tablolar **otomatik oluşturulur**

---

## Gereksinimler

| Bileşen | Sürüm |
|---|---|
| İşletim sistemi | Windows 10 / 11 (64 bit) |
| Visual Studio | 2022 (17.8 veya üzeri), **".NET masaüstü geliştirme"** iş yükü kurulu |
| .NET | 8 SDK (Visual Studio 17.8+ ile birlikte gelir) |
| SQL Server | 2016 veya üzeri – **Express** veya **LocalDB** yeterli |

---

## Kurulum

### 1. Projeyi açın

```bash
git clone https://github.com/<kullanici-adi>/DigitestGirdiMuayene.git
```

`DigitestGirdiMuayene.sln` dosyasını Visual Studio 2022 ile açın. NuGet paketleri ilk derlemede otomatik indirilir.
Başlangıç projesi `Digitest.IncomingInspection.WinForms` olmalıdır (kalın yazıyla görünür; değilse sağ tık → *Set as Startup Project*).

### 2. Bağlantı cümlesini ayarlayın

`src/Digitest.IncomingInspection.WinForms/appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.\\SQLEXPRESS;Database=DigitestQC;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

Kendi kurulumunuza göre örnekler:

| SQL Server | Bağlantı cümlesi |
|---|---|
| SQL Express (varsayılan) | `Server=.\\SQLEXPRESS;Database=DigitestQC;Trusted_Connection=True;TrustServerCertificate=True;` |
| LocalDB (Visual Studio ile gelir) | `Server=(localdb)\\MSSQLLocalDB;Database=DigitestQC;Trusted_Connection=True;TrustServerCertificate=True;` |
| Varsayılan instance | `Server=localhost;Database=DigitestQC;Trusted_Connection=True;TrustServerCertificate=True;` |
| SQL kullanıcısı ile | `Server=.\\SQLEXPRESS;Database=DigitestQC;User Id=kullanici;Password=sifre;TrustServerCertificate=True;` |

> **İpucu:** Kendi bilgisayarınıza özel ayarları git'e göndermek istemiyorsanız aynı klasöre
> `appsettings.Local.json` oluşturun (aynı yapıda). Bu dosya `appsettings.json`'ı ezer ve `.gitignore`'da olduğu için repoya gitmez.

### 3. Çalıştırın

**F5**'e basın. İlk açılışta uygulama:

1. `DigitestQC` veritabanını `Turkish_CI_AS` harmanlamasıyla oluşturur,
2. Tabloları oluşturur (`database/02_CreateTables.sql`),
3. Doküman bilgisini (FR-QC-009, 01.06.2026, Rev 00) ve sabit muayene satırlarını ekler (`database/03_SeedData.sql`).

Scriptler tekrar çalıştırılabilir yazıldığı için her açılışta güvenle çalışır; var olan veriye dokunmaz.

#### Veritabanını elle kurmak isterseniz

`appsettings.json` içinde `"Database": { "AutoCreate": false }` yapın ve SSMS'te sırasıyla çalıştırın:

1. `database/01_CreateDatabase.sql` (master üzerinde)
2. `database/02_CreateTables.sql` (DigitestQC seçiliyken)
3. `database/03_SeedData.sql` (DigitestQC seçiliyken)

---

## Kullanım

| İşlem | Nasıl |
|---|---|
| Yeni form | **+ Yeni Form** veya `Ctrl+N` |
| Kaydı açmak | Listede çift tık, `Enter` veya **Aç / Düzenle** |
| Kaydetmek | **Kaydet** veya `Ctrl+S` |
| PDF almak | Form ekranında **Kaydet ve PDF Al** / `Ctrl+P`; listede **PDF Al** |
| Silmek | Listede **Sil** veya `Delete` (onay ister) |
| Listeyi yenilemek | `F5` |

- `*` işaretli alanlar zorunludur: **Ürün/Parça Adı, Lot/Parti/Seri No, Muayene Tarihi, Tedarikçi Adı**.
- **Gelen Miktar**'da ondalık için virgül veya nokta kullanılabilir (`12,5`); binlik ayırıcı kullanmayın (`1500`).
- **Geliş Tarihi** isteğe bağlıdır; tarih kutusundaki onay işaretiyle açılıp kapatılır.
- 7. (serbest) satıra muayene tipini `Türkçe / English` biçiminde yazarsanız PDF'te iki satır olarak basılır.
- PDF'ler varsayılan olarak `Belgeler\DIGITEST\Girdi Muayene` klasörüne
  `FR-QC-009_GM-2026-0001_LOTNO.pdf` adıyla kaydedilir ve otomatik açılır. Klasör `appsettings.json` → `Pdf:OutputFolder` ile değiştirilebilir.

---

## Proje yapısı

```
DigitestGirdiMuayene.sln
├─ database/                                  SQL scriptleri (tek kaynak; uygulamaya gömülür)
│  ├─ 01_CreateDatabase.sql
│  ├─ 02_CreateTables.sql
│  └─ 03_SeedData.sql
└─ src/
   ├─ Digitest.IncomingInspection.Core        Modeller, enum'lar, doğrulama, iş servisi (bağımlılık yok)
   ├─ Digitest.IncomingInspection.Data        Dapper + sade SQL ile repository'ler, veritabanı kurulumu
   ├─ Digitest.IncomingInspection.Reporting   QuestPDF ile PDF şablonu (kağıt formun düzeni)
   └─ Digitest.IncomingInspection.WinForms    Ekranlar, ayarlar, uygulama girişi
      ├─ Forms/MainForm                       Kayıt listesi ve arama
      ├─ Forms/InspectionEditForm             Form giriş ekranı
      ├─ Forms/DocumentInfoForm               Doküman bilgileri
      └─ appsettings.json                     Bağlantı ve PDF ayarları
```

Bağımlılık yönü: `WinForms → Data, Reporting → Core`. Ekranlar veritabanına doğrudan değil,
`InspectionFormService` üzerinden yazar; doğrulama ve genel sonuç hesabı tek yerdedir.

Formlar Visual Studio tasarımcısıyla (Designer) açılıp düzenlenebilir.

### Veritabanı

| Tablo | İçerik |
|---|---|
| `InspectionForm` | Form başlığı: ürün/ticari bilgiler, NCR, notlar, muayene eden, genel sonuç, doküman bilgisi kopyası |
| `InspectionItem` | Muayene satırları (`FormId` → `InspectionForm`, silmede CASCADE). `Decision`: 0 Seçilmedi, 1 Kabul, 2 Şartlı Kabul, 3 Red |
| `InspectionTemplateItem` | Yeni formda hazır gelen sabit satırlar |
| `FormDocumentInfo` | Doküman No / Yayın Tarihi / Revizyon / Revizyon Tarihi (tek satır) |
| `FormNumberCounter` | Yıllık kayıt numarası sayacı |

Alan uzunlukları `Core/Validation/FieldLimits.cs` ile veritabanında aynıdır; birini değiştirirseniz diğerini de değiştirin.

---

## Özelleştirme

- **Sabit satırları değiştirmek:** `InspectionTemplateItem` tablosundaki kayıtları güncelleyin (ör. kabul kriteri metni). Değişiklik yeni formlara yansır.
- **Revizyon değiştiğinde:** Uygulamada **Doküman Bilgileri** ekranından güncelleyin.
- **PDF düzeni:** `Reporting/InspectionFormPdfGenerator.cs`. Sütun genişlikleri kağıt formdan ölçülen oranlarla verilmiştir; renkler, yazı tipi ve satır yükseklikleri dosyanın başındaki sabitlerdedir.
- **Logo:** `src/Digitest.IncomingInspection.Reporting/Assets/logo_digitest.png` dosyasını değiştirmeniz yeterli (PDF ve ekran aynı dosyayı kullanır).

### Kağıt formdaki küçük düzeltmeler

PDF'te kağıt formdaki bazı İngilizce etiket hataları düzeltildi:

| Kağıt form | PDF |
|---|---|
| Envanter Kodu – *Inventory Name* | *Inventory Code* |
| Gelen Miktar – *Income Quantity* | *Incoming Quantity* |
| Envanter Grubu – *Supplier Name* | *Inventory Group* |
| Lot/Parti/**Soir** No | Lot/Parti/**Seri** No – *Lot/Batch/Serial No* |

Orijinal halini istiyorsanız `InspectionFormPdfGenerator.cs` → `ComposeProductAndCommercialInfo` içindeki metinleri değiştirin.

---

## Dağıtım (başka bilgisayara kurulum)

Visual Studio'da WinForms projesine sağ tık → **Publish** → *Folder* → `win-x64` veya komut satırından:

```bash
dotnet publish src/Digitest.IncomingInspection.WinForms -c Release -r win-x64 --self-contained true -o publish
```

`publish` klasörünü hedef bilgisayara kopyalayın, `appsettings.json` içindeki bağlantı cümlesini düzenleyip `DigitestGirdiMuayene.exe`'yi çalıştırın.
`--self-contained true` ile hedef bilgisayarda .NET kurulu olması gerekmez.

---

## GitHub'a gönderme

```bash
cd DigitestGirdiMuayene
git init
git add .
git commit -m "Girdi Muayene ve Sonuç Formu - ilk sürüm"
git branch -M main
git remote add origin https://github.com/<kullanici-adi>/DigitestGirdiMuayene.git
git push -u origin main
```

---

## Sık karşılaşılan sorunlar

| Belirti | Çözüm |
|---|---|
| "SQL Server'a ulaşılamadı" | *SQL Server (SQLEXPRESS)* servisinin çalıştığını kontrol edin (services.msc). Sunucu adını SSMS'te bağlandığınız adla aynı yazın. |
| "Login failed" (18456) | Windows kullanıcınızın SQL Server'da yetkisi olmalı, ya da `User Id`/`Password` ile bağlanın. |
| Sertifika / SSL hatası | Bağlantı cümlesinde `TrustServerCertificate=True;` olduğundan emin olun. |
| PDF'in üzerine yazılamıyor | Aynı PDF bir görüntüleyicide açıkken uygulama zaman damgalı yeni bir dosya oluşturur. |

---

## Lisans notu – QuestPDF

PDF üretiminde [QuestPDF](https://www.questpdf.com) kullanılır. **Community** lisansı, yıllık brüt geliri
**1 milyon USD altındaki** şirketler için ücretsizdir. Üzerindeyse Professional/Enterprise lisans alınmalı
ve `Program.cs` içindeki `QuestPDF.Settings.License` satırı buna göre değiştirilmelidir.
Ayrıntı: https://www.questpdf.com/license/
