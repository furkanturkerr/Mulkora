<div align="center">

# 🏡 Mülkora

### Gayrimenkul İlan, Danışman ve Randevu Platformu

Satılık ve kiralık gayrimenkulleri keşfet, detaylı ilanları incele,  
doğrulanmış danışmanlara ulaş ve uygun tarih için gösterim randevusu oluştur.

<br/>

<img src="https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" />
<img src="https://img.shields.io/badge/ASP.NET_Core-MVC_%26_Web_API-7B2CBF?style=for-the-badge&logo=dotnet&logoColor=white" />
<img src="https://img.shields.io/badge/Entity_Framework_Core-5C940D?style=for-the-badge&logo=nuget&logoColor=white" />
<img src="https://img.shields.io/badge/SQL_Server-CC2927?style=for-the-badge&logo=microsoftsqlserver&logoColor=white" />

<br/>

<img src="https://img.shields.io/badge/Identity-Authentication-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" />
<img src="https://img.shields.io/badge/JWT-Authorization-111827?style=for-the-badge&logo=jsonwebtokens&logoColor=white" />
<img src="https://img.shields.io/badge/OpenAI-AI-412991?style=for-the-badge&logo=openai&logoColor=white" />
<img src="https://img.shields.io/badge/Leaflet-Maps-199900?style=for-the-badge&logo=leaflet&logoColor=white" />

<br/>

<img src="https://img.shields.io/badge/AutoMapper-Mapping-E63946?style=for-the-badge" />
<img src="https://img.shields.io/badge/FluentValidation-Validation-2F855A?style=for-the-badge" />
<img src="https://img.shields.io/badge/MailKit-Email-2563EB?style=for-the-badge" />
<img src="https://img.shields.io/badge/Razor_Views-Frontend-0F172A?style=for-the-badge&logo=html5&logoColor=white" />

</div>

<br/>

---

## ✨ Proje Hakkında

**Mülkora**, kullanıcıların satılık ve kiralık gayrimenkulleri keşfedebildiği, ilan detaylarını ve konumlarını inceleyebildiği ve ilgili gayrimenkul danışmanından gösterim randevusu oluşturabildiği ASP.NET Core tabanlı bir emlak platformudur.

Proje yalnızca ilan listeleyen bir yapı olarak geliştirilmedi. İlanın danışman tarafından oluşturulmasından admin onayına, görsel ve özellik yönetiminden randevu çakışmalarının önlenmesine, e-posta doğrulamadan AI destekli içerik kontrolüne kadar birbirine bağlı gerçek iş süreçleri üzerinde çalışıldı.

Uygulama üç farklı kullanım alanına sahiptir:

- 👤 **Kullanıcı** — İlan keşfi, danışman inceleme ve randevu işlemleri
- 🧑‍💼 **Agent** — Kendi ilanlarını, görsellerini ve randevularını yönetme
- ⚙️ **Admin** — İlan onay süreci ve sistem yönetimi

Frontend uygulaması veritabanına doğrudan erişmez. **ASP.NET Core MVC WebUI**, tüm backend işlemlerini **ASP.NET Core Web API** üzerinden gerçekleştirir.

---

## 🚀 Öne Çıkan Özellikler

- 🏠 Satılık ve kiralık gayrimenkul listeleme
- 🔎 Şehir, ilçe, kategori, oda, fiyat ve ilan türüne göre filtreleme
- 📄 Sayfalama destekli ilan ve yönetim listeleri
- 🖼️ Bir ilana birden fazla görsel yükleme ve galeri sistemi
- 🧩 İlanlara dinamik özellik atama
- 🗺️ Adres bilgisinden otomatik koordinat üretme
- 📍 Leaflet ile ilan konumunu harita üzerinde gösterme
- 🧑‍💼 Aktif ve doğrulanmış gayrimenkul danışmanlarını listeleme
- 🔄 Agent → Admin ilan onay süreci
- 🤖 OpenAI destekli ilan içerik ön kontrolü
- 📅 Gayrimenkul gösterim randevusu oluşturma
- 🔒 Eş zamanlı randevu çakışmalarını önleyen transaction yapısı
- 🔐 ASP.NET Core Identity, JWT ve Cookie Authentication
- 👥 Rol tabanlı Admin ve Agent yetkilendirmesi
- 📧 E-posta doğrulama ve doğrulama mailini yeniden gönderme
- 🔑 Şifremi unuttum ve şifre sıfırlama sistemi
- ✉️ MailKit ile tasarımlı HTML e-postalar
- ✅ FluentValidation ile business doğrulamaları
- ⚠️ Merkezi API exception middleware
- 📩 İletişim formu ve admin mesaj yönetimi
- 👤 Kullanıcının kendi randevularını görüntüleyebildiği profil alanı
- 🚫 401, 403 ve 404 özel hata sayfaları
- ⚙️ Ayrı Admin ve Agent yönetim panelleri

---

## 🔄 İlan Yönetimi ve Yayın Süreci

Mülkora'da danışman tarafından oluşturulan bir ilan doğrudan yayına alınmaz.

Yeni ilan ilk olarak **Draft** durumunda oluşturulur. Danışman ilan bilgilerini, özelliklerini ve görsellerini hazırladıktan sonra ilanı admin onayına gönderebilir.

```text
Draft
  │
  │ Onaya Gönder
  ↓
PendingApproval
  │
  ├───────────────┐
  ↓               ↓
Published      Rejected
  │
  ├───────────────┐
  ↓               ↓
Sold            Rented
```

Admin yalnızca **onay bekleyen** ilanlar için yayın kararı verebilir.

Onaylanan ilan `Published` durumuna geçer ve public ilan sayfalarında görüntülenmeye başlanır. Reddedilen ilan ise Agent tarafından düzenlenerek tekrar onay sürecine gönderilebilir.

Danışman ilanı güncellediğinde sistem ilanı yeniden **Draft** durumuna alır. Böylece daha önce yayınlanmış bir ilanın içeriği değiştirildiğinde kontrol edilmeden yayında kalmasının önüne geçilir.

Ayrıca:

- Agent yalnızca kendisine ait ilan üzerinde işlem yapabilir.
- Yalnızca yayındaki satılık ilan `Sold` olarak işaretlenebilir.
- Yalnızca yayındaki kiralık ilan `Rented` olarak işaretlenebilir.
- Satılık bir ilan kiralandı, kiralık bir ilan satıldı olarak işaretlenemez.

Bu kontroller controller yerine Business katmanında uygulanmaktadır.

---

## 🤖 OpenAI Destekli İlan Ön Kontrolü

Adminin yayın kararını desteklemek için ilan inceleme sürecine **OpenAI API** entegrasyonu eklendi.

İlan incelenirken sistem;

- başlık,
- açıklama,
- fiyat,
- şehir ve ilçe,
- adres,
- kategori,
- ilan türü,
- oda ve banyo sayısı,
- net / brüt metrekare,
- bina ve kat bilgileri

gibi alanları AI servisine gönderir.

AI servisi içeriği;

- anlamsız veya rastgele veriler,
- spam ve reklam içeriği,
- test amaçlı ifadeler,
- uygunsuz içerik,
- mantıksız değerler,
- ilan bilgilerindeki açık çelişkiler

açısından ön kontrolden geçirir.

Servisten yalnızca yapılandırılmış bir sonuç beklenir:

```json
{
  "isApproved": true
}
```

veya:

```json
{
  "isApproved": false
}
```

AI sonucu **nihai yayın kararı değildir**.

Admin panelinde içerik için bir ön kontrol sonucu gösterilir ancak ilanı yayınlama veya reddetme yetkisi yine admindedir.

```text
Agent
  ↓
İlanı Onaya Gönderir
  ↓
Admin İlanı İnceler
  ↓
OpenAI Ön Kontrolü
  ↓
AI Sonucu + İlan Bilgileri
  ↓
Admin Kararı
  ↓
Onayla / Reddet
```

Bu yapıda AI, sistem üzerinde doğrudan değişiklik yapan bir mekanizma yerine admin kararını destekleyen ek bir kontrol katmanı olarak kullanılmıştır.

---

## 📅 Randevu Sistemi

Kullanıcılar yayında olan bir gayrimenkul için ilan detay sayfasından gösterim randevusu oluşturabilir.

Randevu oluşturulurken kullanıcı yalnızca ilanı ve istediği tarihi seçer. Danışman bilgisi istemciden alınmaz; sistem ilanın bağlı olduğu `AgentId` üzerinden doğru danışmanı otomatik olarak belirler.

Randevu oluşturma sırasında:

- İlanın mevcut olup olmadığı kontrol edilir.
- Yalnızca `Published` durumundaki ilanlara randevu oluşturulabilir.
- Geçmiş bir tarih için randevu oluşturulamaz.
- Aynı danışmana aynı tarih ve saatte ikinci bir randevu verilemez.

### Eş Zamanlı Randevu Kontrolü

Randevu çakışması yalnızca basit bir `Any()` sorgusuyla bırakılmadı.

İki kullanıcının aynı anda aynı boş randevu saatini seçmesi durumunda oluşabilecek yarış koşulunu azaltmak için randevu oluşturma işlemi **veritabanı transaction'ı** içerisinde çalışmaktadır.

```text
Kullanıcı A ──┐
              ├── Aynı Agent + Aynı Saat
Kullanıcı B ──┘
                     ↓
          Serializable Transaction
                     ↓
             Çakışma Kontrolü
                     ↓
          Yalnızca Bir Kayıt Oluşur
```

Transaction için `IsolationLevel.Serializable` kullanılır.

Danışmanlar kendilerine gelen randevuları Agent panelinden görüntüleyebilir ve yalnızca kendilerine ait randevuların durumunu değiştirebilir.

Kullanıcılar ise kendi randevu geçmişlerini profil alanından takip edebilir.

---

## 🗺️ Adres, Geocoding ve Harita

İlan oluştururken veya güncellerken koordinat bilgisinin kullanıcı tarafından elle girilmesi gerekmez.

Girilen;

```text
Adres + İlçe + Şehir
```

bilgisi **TrueWay Geocoding API** servisine gönderilir.

```text
Adres Bilgisi
    ↓
TrueWay Geocoding
    ↓
Latitude / Longitude
    ↓
Property
    ↓
Leaflet Haritası
```

Başarılı geocoding sonucunda latitude ve longitude değerleri ilanla birlikte saklanır.

Public ilan detay ekranında bu koordinatlar **Leaflet** kullanılarak harita üzerinde gösterilir ve kullanıcı ilan konumunu doğrudan görüntüleyebilir.

TrueWay Geocoding servisine erişim RapidAPI üzerinden gerçekleştirilmektedir.

---

## 🖼️ Çoklu İlan Görseli Yönetimi

İlan görselleri `Property` entity'si içerisinde tek bir alan olarak tutulmaz.

Her görsel ayrı bir `PropertyImage` kaydıdır:

```text
Property
   │
   ├── PropertyImage
   ├── PropertyImage
   └── PropertyImage
```

Agent bir ilana aynı anda birden fazla görsel yükleyebilir.

Görsel yükleme sırasında:

- `.jpg`
- `.jpeg`
- `.png`
- `.webp`

formatlarına izin verilir.

Her görsel için **5 MB** boyut kontrolü uygulanır ve dosyalar çakışmayı önlemek amacıyla GUID tabanlı benzersiz isimlerle API'nin `wwwroot/property-images` dizinine kaydedilir.

Veritabanında fiziksel dosyanın kendisi yerine dosya URL'si ve `DisplayOrder` bilgisi tutulur.

Agent yalnızca kendisine ait ilanın görsellerini ekleyebilir veya silebilir. Bir görsel silindiğinde hem veritabanı kaydı hem de fiziksel dosya kaldırılır.

Public ilan detayında görseller galeri olarak kullanıcıya sunulur.

---

## 🧩 İlan Özellikleri

İlanlara;

- özel bahçe,
- güvenlik,
- kapalı otopark,
- şömine,
- geniş teras,
- ebeveyn banyosu

gibi birden fazla özellik atanabilir.

`Property` ve `Feature` arasında **many-to-many** ilişki bulunmaktadır.

```text
Property
   ↓
FeatureProperty
   ↓
Feature
```

İlan oluşturulurken seçilen özellikler ilişki tablosuna bağlanır.

Güncelleme sırasında eski seçimler ile yeni seçimler karşılaştırılmak yerine mevcut ilişkiler temizlenerek seçili özellikler tekrar oluşturulur. Tekrarlanan Feature ID'leri `Distinct()` ile elenir.

Entity ilişkisi AutoMapper tarafından doğrudan değiştirilmez; normal ilan alanları map edilirken `Features` ilişkisi DataAccess katmanında ayrıca yönetilir.

---

## 🔐 Authentication & Authorization

Mülkora'da Web API ve MVC tarafı birbirine bağlı iki authentication yapısı kullanmaktadır.

```text
Kullanıcı
   ↓
WebUI Login
   ↓
Web API
   ↓
ASP.NET Core Identity
   ↓
JWT Access Token
   ↓
WebUI Cookie Authentication
   ↓
Korumalı API İstekleri
```

Kullanıcı giriş yaptığında API tarafında ASP.NET Core Identity ile e-posta ve parola kontrol edilir.

E-posta adresi doğrulanmamış kullanıcıların giriş yapmasına izin verilmez.

Başarılı giriş sonrasında JWT içerisine temel kullanıcı bilgileri ve roller eklenir:

```text
NameIdentifier
Email
Name
Surname
Role
AgentId → kullanıcı Agent ise
```

Agent hesabı için oluşturulan `AgentId` claim'i, ilan ve randevu işlemlerinde sahiplik kontrolü için kullanılmaktadır.

WebUI gelen JWT'yi okuyarak claim'leri Cookie Authentication oturumuna aktarır ve JWT'nin kendisini `access_token` claim'i olarak saklar.

WebUI tarafından korunan bir API endpointine istek yapılırken bu token:

```text
Authorization: Bearer <token>
```

şeklinde gönderilir.

Böylece:

- WebUI tarafında Cookie Authentication,
- Web API tarafında JWT Bearer Authentication

birlikte kullanılmaktadır.

Admin ve Agent alanları rol tabanlı `[Authorize]` kontrolleri ile korunur.

---

## 📧 Kayıt, E-Posta Doğrulama ve Şifre Sıfırlama

Account sistemi yalnızca Login ve Register ekranlarından oluşmaz.

Yeni kullanıcı kaydı oluşturulduğunda sistem ASP.NET Core Identity üzerinden bir **e-posta doğrulama tokenı** üretir.

Token URL-safe biçime dönüştürülür ve kullanıcıya tasarımlı HTML doğrulama e-postası gönderilir.

```text
Register
   ↓
Identity User
   ↓
Email Confirmation Token
   ↓
MailKit / SMTP
   ↓
Doğrulama Linki
   ↓
Email Confirmed
```

E-posta gönderimi başarısız olursa yarım kullanıcı kaydı bırakmamak için oluşturulan kullanıcı silinir.

Kullanıcı doğrulama e-postasını alamazsa yeni doğrulama bağlantısı talep edebilir.

Şifre sıfırlama sürecinde ise:

```text
E-posta Adresi
   ↓
Password Reset Token
   ↓
Tasarımlı HTML E-posta
   ↓
Reset Password Sayfası
   ↓
Yeni Şifre
```

akışı kullanılmaktadır.

E-posta gönderimi **MailKit + MimeKit + Gmail SMTP / STARTTLS** üzerinden gerçekleştirilir.

---

## 👥 Kullanıcı Rolleri ve Yönetim Alanları

### 👤 Kullanıcı

Normal kullanıcılar:

- Public ilanları görüntüleyebilir
- İlanları filtreleyebilir
- İlan detaylarını ve galeriyi inceleyebilir
- İlanın konumunu haritada görüntüleyebilir
- Aktif danışmanları inceleyebilir
- İlan üzerinden randevu oluşturabilir
- Profil alanından kendi randevularını görüntüleyebilir
- İletişim formu üzerinden mesaj gönderebilir

---

### 🧑‍💼 Agent

Agent paneli ayrı bir ASP.NET Core Area olarak geliştirilmiştir.

Danışman:

- Yalnızca kendi ilanlarını görüntüler
- İlanlarını metin ve durum bilgisine göre filtreleyebilir
- Yeni ilan oluşturabilir
- Kendi ilanını güncelleyebilir
- İlan için kategori ve özellik seçebilir
- Birden fazla ilan görseli yükleyebilir
- Mevcut görselleri silebilir
- Taslak veya reddedilmiş ilanı tekrar admin onayına gönderebilir
- Yayındaki satılık ilanını satıldı olarak işaretleyebilir
- Yayındaki kiralık ilanını kiralandı olarak işaretleyebilir
- Kendisine gelen randevuları görüntüleyebilir
- Kendisine ait randevuların durumunu yönetebilir

Agent işlemlerinde kullanıcıdan gelen bir `AgentId` değerine güvenilmez. Agent kimliği JWT içerisindeki claim üzerinden alınır ve gerekli işlemlerde ilan sahipliği ayrıca kontrol edilir.

---

### ⚙️ Admin

Admin alanı da ayrı bir Area olarak yapılandırılmış ve `Admin` rolü ile korunmuştur.

Admin panelinde:

- İlanları listeleme ve filtreleme
- İlan detaylarını inceleme
- Bekleyen ilanları onaylama veya reddetme
- AI içerik kontrol sonucunu görüntüleme
- Kategori yönetimi
- İlan özellikleri yönetimi
- Agent oluşturma ve yönetimi
- Kullanıcı yönetimi
- Rol yönetimi
- İletişim mesajlarını görüntüleme

işlemleri gerçekleştirilebilir.

Admin tarafından yeni bir Agent oluşturulduğunda yalnızca Agent kaydı değil, buna bağlı **ASP.NET Core Identity kullanıcı hesabı ve Agent rolü** de oluşturulur.

---

## 🏗️ Proje Mimarisi

Mülkora, sorumlulukları farklı projelere ayıran **Layered Architecture** yaklaşımıyla geliştirilmiştir.

Solution içerisinde backend ve frontend birbirinden ayrılmıştır.

```text
Mulkora
│
├── Backend
│   │
│   ├── Mulkora.Entity
│   │
│   ├── Mulkora.Contracts
│   │
│   ├── Mulkora.DataAccess
│   │
│   ├── Mulkora.Business
│   │
│   └── Mulkora.WebApi
│
└── Frontend
    │
    └── Mulkora.WebUI
```

### Mulkora.Entity

Domain entity'lerini ve enum yapılarını içerir.

Başlıca entity'ler:

- `AppUser`
- `AppRole`
- `Agent`
- `Property`
- `PropertyImage`
- `Category`
- `Feature`
- `Appointment`
- `Contact`

İlanın `ListingType` ve `PropertyStatus` değerleri enum yapıları üzerinden yönetilmektedir.

---

### Mulkora.Contracts

Katmanlar ve uygulamalar arasında taşınan DTO modellerini içerir.

Create, Update, Result ve detay senaryoları için ihtiyaca göre farklı DTO'lar kullanılır.

Bu sayede Entity sınıfları doğrudan API sözleşmesi olarak kullanılmaz.

---

### Mulkora.DataAccess

Veritabanı erişim katmanıdır.

İçerisinde:

- Entity Framework Core
- SQL Server
- `DbContext`
- Migrations
- Generic Repository
- Entity bazlı DAL interface'leri
- Entity Framework DAL implementasyonları
- Include sorguları
- Filtreleme ve sayfalama sorguları
- Transaction gerektiren veri erişim işlemleri

bulunmaktadır.

Standart CRUD işlemleri Generic Repository üzerinden yürütülürken; Property, Appointment ve diğer domain'e özel sorgular kendi DAL sınıflarında tutulur.

---

### Mulkora.Business

Uygulamanın iş kurallarını içerir.

Bu katmanda:

- Manager sınıfları
- Business kontrolleri
- Ownership kontrolleri
- İlan durum geçişleri
- Randevu kuralları
- FluentValidation validator'ları
- AutoMapper mapping profilleri
- Identity tabanlı kullanıcı işlemleri
- Mail işlemleri

yer almaktadır.

Controller'ların doğrudan veri erişim kodu içermemesi ve business kurallarının merkezi bir yerde tutulması amaçlanmıştır.

---

### Mulkora.WebApi

Uygulamanın backend servislerini dış dünyaya açan REST API katmanıdır.

Bu katmanda:

- API Controller'ları
- JWT Bearer Authentication
- Role-Based Authorization
- Swagger / OpenAPI
- Global Exception Middleware
- Image File Service
- OpenAI Service
- TrueWay Geocoding Service

bulunmaktadır.

Ayrıca API, fiziksel ilan görsellerini `wwwroot` üzerinden servis etmektedir.

---

### Mulkora.WebUI

Kullanıcı arayüzü ASP.NET Core MVC ve Razor Views ile geliştirilmiştir.

WebUI içerisinde:

- Public Controller'lar
- Razor Views
- ViewComponents
- ViewModel'ler
- API Service sınıfları
- Admin Area
- Agent Area
- Cookie Authentication
- Custom hata sayfaları

bulunmaktadır.

WebUI doğrudan `DbContext` veya DataAccess katmanına erişmez.

Backend işlemleri named `HttpClient` üzerinden Web API'ye gönderilir.

```text
Razor View
    ↓
MVC Controller
    ↓
WebUI Service
    ↓
HttpClient
    ↓
Web API
    ↓
Business
    ↓
DataAccess
    ↓
Entity Framework Core
    ↓
SQL Server
```

---

## 🗃️ Temel Entity İlişkileri

Mülkora'nın ana veri modeli `Property` etrafında şekillenmektedir.

```text
AppUser
   │
   ├────────────── Appointment
   │                    │
   │                    │
   └── Agent ───────────┤
         │               │
         │               │
         └── Property ───┘
                │
                ├── Category
                │
                ├── PropertyImage
                │
                └── Feature
                     ↑
               Many-to-Many
```

Temel ilişkiler:

- `AppUser ↔ Agent` → One-to-One
- `Agent → Property` → One-to-Many
- `Category → Property` → One-to-Many
- `Property → PropertyImage` → One-to-Many
- `Property ↔ Feature` → Many-to-Many
- `AppUser → Appointment` → One-to-Many
- `Agent → Appointment` → One-to-Many
- `Property → Appointment` → One-to-Many

Bu yapı sayesinde bir randevu aynı anda kullanıcı, ilan ve ilgili danışman ile ilişkilendirilmektedir.

---

## ✅ Validation ve Hata Yönetimi

Business katmanında **FluentValidation** kullanılarak Create ve Update işlemleri doğrulanmaktadır.

Örneğin ilan oluştururken:

- Fiyat sıfırdan büyük olmalıdır.
- Net metrekare brüt metrekareden büyük olamaz.
- Oda, salon, banyo ve bina yaşı negatif olamaz.
- Bulunduğu kat toplam kat sayısından büyük olamaz.
- Şehir, ilçe ve adres boş bırakılamaz.
- Kategori ve ilan türü seçilmelidir.

API tarafında merkezi `ExceptionMiddleware` kullanılır.

Middleware;

- validation hatalarını,
- yetkisiz işlemleri,
- business hatalarını,
- beklenmeyen sistem hatalarını

tek noktadan HTTP response'a dönüştürür.

WebUI tarafında ise status code ve exception senaryoları için özel hata sayfaları kullanılmaktadır.

---

## 🧩 Kullanılan Yaklaşımlar

- Layered Architecture
- Generic Repository Pattern
- Dependency Injection
- Interface Abstraction
- DTO Pattern
- ViewModel kullanımı
- AutoMapper
- FluentValidation
- RESTful API
- Async / Await
- LINQ
- Entity Framework Core
- One-to-One ilişkiler
- One-to-Many ilişkiler
- Many-to-Many ilişkiler
- Database Transactions
- JWT Authentication
- Cookie Authentication
- Role-Based Authorization
- Claim-Based Ownership Control
- Global Exception Middleware
- `IHttpClientFactory`
- Razor ViewComponents
- ASP.NET Core Areas

---

## 🛠️ Kullanılan Teknolojiler

### Backend

- .NET 8
- C#
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- ASP.NET Core Identity
- AutoMapper
- FluentValidation
- JWT Bearer Authentication
- MailKit
- MimeKit

### Frontend

- ASP.NET Core MVC
- Razor Views
- ViewComponents
- Areas
- HTML5
- CSS3
- JavaScript
- Responsive Design
- Leaflet

### AI & External Services

- OpenAI API
- TrueWay Geocoding API
- RapidAPI
- SMTP

### API & Security

- RESTful API
- JWT
- Cookie Authentication
- Role-Based Authorization
- Anti-Forgery Token
- Swagger / OpenAPI

### Development

- JetBrains Rider
- Git
- GitHub
- Swagger
- SQL Server

---

## 📌 Genel Akış

```text
                      MÜLKORA
                         │
        ┌────────────────┼────────────────┐
        │                │                │
        ↓                ↓                ↓
      User             Agent            Admin
        │                │                │
  İlan Keşfi       İlan Yönetimi    İlan İnceleme
  Randevu          Görsel Yönetimi  Onay / Red
  Profil           Randevular       Sistem Yönetimi
        │                │                │
        └────────────────┼────────────────┘
                         ↓
                 ASP.NET Core MVC
                         ↓
                      Web API
                         ↓
                 Business Layer
                         ↓
                Data Access Layer
                         ↓
                    SQL Server

        ┌────────────────┴────────────────┐
        ↓                                 ↓
   OpenAI API                    TrueWay Geocoding
 İlan Ön Kontrolü                 Konum Verisi
```

---

# 📸 Proje Görselleri

## 🏠 Anasayfa

<p align="center">
  <img src="https://github.com/furkanturkerr/Mulkora/blob/main/Frontend/Mulkora.WebUI/wwwroot/m%C3%BClkora-images/d-1.png" />
</p>

<p align="center">
  <img src="https://github.com/furkanturkerr/Mulkora/blob/main/Frontend/Mulkora.WebUI/wwwroot/m%C3%BClkora-images/d-2.png" />
</p>

<p align="center">
  <img src="https://github.com/furkanturkerr/Mulkora/blob/main/Frontend/Mulkora.WebUI/wwwroot/m%C3%BClkora-images/d-3.png" />
</p>

<p align="center">
  <img src="https://github.com/furkanturkerr/Mulkora/blob/main/Frontend/Mulkora.WebUI/wwwroot/m%C3%BClkora-images/d-4.png" />
</p>

<p align="center">
  <img src="https://github.com/furkanturkerr/Mulkora/blob/main/Frontend/Mulkora.WebUI/wwwroot/m%C3%BClkora-images/d-5.png" />
</p>

<p align="center">
  <img src="https://github.com/furkanturkerr/Mulkora/blob/main/Frontend/Mulkora.WebUI/wwwroot/m%C3%BClkora-images/d-6.png" />
</p>

<p align="center">
  <img src="https://github.com/furkanturkerr/Mulkora/blob/main/Frontend/Mulkora.WebUI/wwwroot/m%C3%BClkora-images/d-7.png" />
</p>

<p align="center">
  <img src="https://github.com/furkanturkerr/Mulkora/blob/main/Frontend/Mulkora.WebUI/wwwroot/m%C3%BClkora-images/d-8.png" />
</p>

---

## 🏡 İlan Keşfi

### İlan Listesi

<p align="center">
  <img src="https://github.com/furkanturkerr/Mulkora/blob/main/Frontend/Mulkora.WebUI/wwwroot/m%C3%BClkora-images/property.png" />
</p>

### İlan Filtreleme

<p align="center">
  <img src="https://github.com/furkanturkerr/Mulkora/blob/main/Frontend/Mulkora.WebUI/wwwroot/m%C3%BClkora-images/property-filter.png" />
</p>

### İlan Detayı

<p align="center">
  <img src="https://github.com/furkanturkerr/Mulkora/blob/main/Frontend/Mulkora.WebUI/wwwroot/m%C3%BClkora-images/property-detail.png" />
</p>

### Harita ve Konum Bilgisi

<p align="center">
  <img src="https://github.com/furkanturkerr/Mulkora/blob/main/Frontend/Mulkora.WebUI/wwwroot/m%C3%BClkora-images/property-detail-map.png" />
</p>

---

## 🧑‍💼 Gayrimenkul Danışmanları

### Danışman Listesi

<p align="center">
  <img src="https://github.com/furkanturkerr/Mulkora/blob/main/Frontend/Mulkora.WebUI/wwwroot/m%C3%BClkora-images/agent.png" />
</p>

### Danışman Detayı

<p align="center">
  <img src="https://github.com/furkanturkerr/Mulkora/blob/main/Frontend/Mulkora.WebUI/wwwroot/m%C3%BClkora-images/agent-detail.png" />
</p>

---

## 📅 Randevu Oluşturma

### Randevu Formu

<p align="center">
  <img src="https://github.com/furkanturkerr/Mulkora/blob/main/Frontend/Mulkora.WebUI/wwwroot/m%C3%BClkora-images/appointment-create.png" />
</p>

### Randevu Başarı Ekranı

<p align="center">
  <img src="https://github.com/furkanturkerr/Mulkora/blob/main/Frontend/Mulkora.WebUI/wwwroot/m%C3%BClkora-images/appointment-success.png" />
</p>

---

## 👤 Profil ve Randevularım

<p align="center">
  <img src="https://github.com/furkanturkerr/Mulkora/blob/main/Frontend/Mulkora.WebUI/wwwroot/m%C3%BClkora-images/profile-appointment-1.png" />
</p>

<p align="center">
  <img src="https://github.com/furkanturkerr/Mulkora/blob/main/Frontend/Mulkora.WebUI/wwwroot/m%C3%BClkora-images/profile-appointment-2.png" />
</p>

---

## ℹ️ Hakkımızda

<p align="center">
  <img src="https://github.com/furkanturkerr/Mulkora/blob/main/Frontend/Mulkora.WebUI/wwwroot/m%C3%BClkora-images/about.png" />
</p>

---

## 📩 İletişim

<p align="center">
  <img src="https://github.com/furkanturkerr/Mulkora/blob/main/Frontend/Mulkora.WebUI/wwwroot/m%C3%BClkora-images/contact.png" />
</p>

---

# 🔐 Account & Authentication

## Giriş

<p align="center">
  <img src="https://github.com/furkanturkerr/Mulkora/blob/main/Frontend/Mulkora.WebUI/wwwroot/m%C3%BClkora-images/auth-login.png" />
</p>

---

## Kayıt

<p align="center">
  <img src="https://github.com/furkanturkerr/Mulkora/blob/main/Frontend/Mulkora.WebUI/wwwroot/m%C3%BClkora-images/auth-register.png" />
</p>

### Kayıt Sonrası E-Posta Doğrulama Bilgilendirmesi

<p align="center">
  <img src="https://github.com/furkanturkerr/Mulkora/blob/main/Frontend/Mulkora.WebUI/wwwroot/m%C3%BClkora-images/auth-registerconfirm.png" />
</p>

### E-Posta Doğrulama Maili

<p align="center">
  <img width="1608" height="1253" alt="Mülkora e-posta doğrulama maili" src="https://github.com/user-attachments/assets/aad31a25-0247-48ea-b1c1-e74180932c86" />
</p>

---

## Doğrulama E-Postasını Yeniden Gönderme

<p align="center">
  <img src="https://github.com/furkanturkerr/Mulkora/blob/main/Frontend/Mulkora.WebUI/wwwroot/m%C3%BClkora-images/auth-resendconfirmation.png" />
</p>

---

## Şifremi Unuttum

<p align="center">
  <img src="https://github.com/furkanturkerr/Mulkora/blob/main/Frontend/Mulkora.WebUI/wwwroot/m%C3%BClkora-images/auth-forgotpassword.png" />
</p>

### Şifre Sıfırlama Talebi

<p align="center">
  <img src="https://github.com/furkanturkerr/Mulkora/blob/main/Frontend/Mulkora.WebUI/wwwroot/m%C3%BClkora-images/auth-forgotconfirm.png" />
</p>

### Şifre Sıfırlama E-Postası

<p align="center">
  <img src="https://github.com/furkanturkerr/Mulkora/blob/main/Frontend/Mulkora.WebUI/wwwroot/m%C3%BClkora-images/auth-mail-passsword.png" />
</p>

---

# 🧑‍💼 Agent Panel

## 🏠 İlan Yönetimi

### İlan Listesi

<p align="center">
  <img src="https://github.com/furkanturkerr/Mulkora/blob/main/Frontend/Mulkora.WebUI/wwwroot/m%C3%BClkora-images/Agent/property-list.png" />
</p>

### Yeni İlan Oluşturma

<p align="center">
  <img src="https://github.com/furkanturkerr/Mulkora/blob/main/Frontend/Mulkora.WebUI/wwwroot/m%C3%BClkora-images/Agent/property-create.png" />
</p>

### İlan Güncelleme

<p align="center">
  <img src="https://github.com/furkanturkerr/Mulkora/blob/main/Frontend/Mulkora.WebUI/wwwroot/m%C3%BClkora-images/Agent/property-update.png" />
</p>

---

## 📅 Randevu Yönetimi

<p align="center">
  <img src="https://github.com/furkanturkerr/Mulkora/blob/main/Frontend/Mulkora.WebUI/wwwroot/m%C3%BClkora-images/Agent/appointmentlist.png" />
</p>

---

# ⚙️ Admin Panel

## 🏠 İlan Yönetimi

### İlan Listesi

<p align="center">
  <img src="https://github.com/furkanturkerr/Mulkora/blob/main/Frontend/Mulkora.WebUI/wwwroot/m%C3%BClkora-images/admin/property-propertylist.png" />
</p>

### İlan İnceleme ve Onay Süreci

<p align="center">
  <img src="https://github.com/furkanturkerr/Mulkora/blob/main/Frontend/Mulkora.WebUI/wwwroot/m%C3%BClkora-images/admin/property-detail.png" />
</p>

### İlan Güncelleme

<p align="center">
  <img src="https://github.com/furkanturkerr/Mulkora/blob/main/Frontend/Mulkora.WebUI/wwwroot/m%C3%BClkora-images/admin/property-update.png" />
</p>

---

## 🗂️ Kategori Yönetimi

<p align="center">
  <img src="https://github.com/furkanturkerr/Mulkora/blob/main/Frontend/Mulkora.WebUI/wwwroot/m%C3%BClkora-images/admin/category.png" />
</p>

---

## 🧩 İlan Özellikleri Yönetimi

<p align="center">
  <img src="https://github.com/furkanturkerr/Mulkora/blob/main/Frontend/Mulkora.WebUI/wwwroot/m%C3%BClkora-images/admin/feature-featurelist.png" />
</p>

---

## 🧑‍💼 Danışman Yönetimi

### Danışman Listesi

<p align="center">
  <img src="https://github.com/furkanturkerr/Mulkora/blob/main/Frontend/Mulkora.WebUI/wwwroot/m%C3%BClkora-images/admin/agent-agentlist.png" />
</p>

### Danışman Güncelleme

<p align="center">
  <img src="https://github.com/furkanturkerr/Mulkora/blob/main/Frontend/Mulkora.WebUI/wwwroot/m%C3%BClkora-images/admin/agent-update.png" />
</p>

---

## 👥 Kullanıcı Yönetimi

<p align="center">
  <img src="https://github.com/furkanturkerr/Mulkora/blob/main/Frontend/Mulkora.WebUI/wwwroot/m%C3%BClkora-images/admin/User-UserList.png" />
</p>

---

## 🔐 Rol Yönetimi

<p align="center">
  <img src="https://github.com/furkanturkerr/Mulkora/blob/main/Frontend/Mulkora.WebUI/wwwroot/m%C3%BClkora-images/admin/role-rolelist.png" />
</p>

---

## 📩 İletişim Mesajları

<p align="center">
  <img src="https://github.com/furkanturkerr/Mulkora/blob/main/Frontend/Mulkora.WebUI/wwwroot/m%C3%BClkora-images/admin/messagelist.png" />
</p>

---

# ⚠️ Hata Sayfaları

## 401 — Unauthorized

<p align="center">
  <img src="https://github.com/furkanturkerr/Mulkora/blob/main/Frontend/Mulkora.WebUI/wwwroot/m%C3%BClkora-images/error-401.png" />
</p>

---

## 403 — Forbidden

<p align="center">
  <img src="https://github.com/furkanturkerr/Mulkora/blob/main/Frontend/Mulkora.WebUI/wwwroot/m%C3%BClkora-images/error-403.png" />
</p>

---

## 404 — Not Found

<p align="center">
  <img src="https://github.com/furkanturkerr/Mulkora/blob/main/Frontend/Mulkora.WebUI/wwwroot/m%C3%BClkora-images/error-4040.png" />
</p>





