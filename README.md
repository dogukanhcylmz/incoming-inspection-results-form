# Girdi Muayene ve Sonuç Formu

Windows masaüstü uygulaması
Form ekrandan doldurulur, SQL Server'a kaydedilir, istenildiğinde dolu haliyle tekrar açılır ve PDF çıktısı alınır.

---

## Özellikler

- Kayıt listesi: tarih aralığı, kayıt no, tedarikçi, ürün, lot no ve genel sonuca göre arama
- Form giriş ekranı: Ürün Parça Bilgileri, Ticari Bilgiler, 7 satırlık muayene tablosu, NCR, Notlar, Adı Soyadı
- Her satırda Kabul / şartlı Kabul / Red kutucuklarından yalnızca biri seçilebilir
- Genel sonuç satır kararlarından otomatik hesaplanır (Red > Beklemede > Şartlı Kabul > Kabul)
- Doğrulamalar: zorunlu alanlar, Red kararında NCR zorunluluğu, geliş tarihi ≤ muayene tarihi, alan uzunlukları
- Otomatik kayıt numarası: GM-2026-0001, GM-2026-0002
- PDF çıktısı (QuestPDF): A4 yatay, logolu, kağıt formun düzeninde; imza alanı imza için boş
- Her form kendi doküman bilgisini saklar ve PDF'e onunla basılır
- Daha önce girilen tedarikçi, envanter grubu, birim ve muayene eden adları açılır kutularda öneri olarak gelir
- İlk açılışta veritabanı ve tablolar otomatik oluşturulur

---

## Gereksinimler

| Bileşen | Sürüm |
|---|---|
| Visual Studio | 2022 (17.8 +), ".NET masaüstü geliştirme" iş yükü kurulu |
| .NET | 8 SDK (Visual Studio 17.8+ ile birlikte gelir) |
| SQL Server | 2016 veya üzeri – Express veya LocalDB  |

---

## Kurulum

### 1. Projeyi açın

DigitestGirdiMuayene.sln dosyasını Visual Studio 22 ile açın. NuGet paketleri ilk derlemede otomatik indirilir.
Başlangıç projesi   Digitest.IncomingInspection.WinForms  olmalıdır (kalın yazıyla görüncek değilse sağ tık -> Set as Startup Project).

### 2. Bağlantı cümlesini ayarlayın

`src/Digitest.IncomingInspection.WinForms/appsettings.json`: içerisinde

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.\\SQLEXPRESS;Database=DigitestQC;Trusted_Connection=True;TrustServerCertificate=True;"
}
```


 LocalDB (VS ile gelir)->  `Server=(localdb)\\MSSQLLocalDB;Database=DigitestQC;Trusted_Connection=True;TrustServerCertificate=True;` 


### 3. Çalıştırın

F5'e basın. İlk açılışta uygulama:

1. `DigitestQC` veritabanını oluşturur,
2. Tabloları oluşturur,
3. Sabit muayene satırlarını ekler.

Scriptler tekrar çalıştırılabilir yazıldığı için her açılışta güvenle çalışır var olan veriye dokunmaz.

---

## Kullanımı
- Yeni form,  Kaydı açma, Kaydetmek, PDF almak, kKayıt silmek, Listeyi yenilemek

- PDF'ler varsayılan olarak   Belgeler\DIGITEST\Girdi Muayene   klasörüne kaydedilr ve otomatik açılır.

---

Bağımlılık yönü: WinForms -> Data, Reporting -> Core. Ekranlar veritabanına doğrudan değil,
InspectionFormService üzerinden yazar ve doğrulama ve genel sonuç hesabı tek yerde.

