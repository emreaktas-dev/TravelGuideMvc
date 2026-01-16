# TravelGuideMvc

**TravelGuideMvc**, Kırgızistan’a gelen turistler için geliştirilmiş  
**ASP.NET Core MVC tabanlı** bir seyahat rehberi uygulamasıdır.

Uygulama; gezilecek yerleri listelemeyi, bu yerlere yakın
**otel, restoran, hastane ve ATM** gibi servis noktalarını
**konum bazlı** olarak göstermeyi amaçlar.

---

## Mevcut Özellikler

- Gezilecek yerler listesi (şehir ve kategori filtresi)
- SEO uyumlu slug bazlı yer detay sayfası
- Yer detaylarında:
  - Açıklama ve fotoğraflar
  - Google Maps ve yol tarifi bağlantıları
- **Nearby (Yakındaki Servisler)**:
  - Otel, restoran, hastane, ATM
  - Haversine formülü ile gerçek mesafe hesabı
  - Kullanıcı tarafından seçilebilir radius (2–50 km)
  - Harita, yol tarifi ve telefon linkleri

---

## Teknik Yapı

- ASP.NET Core MVC
- Entity Framework Core (Code First)
- SQL Server (LocalDB)
- Katmanlı yapı: Models, Services, Controllers, ViewModels, Views

Konum hesaplamaları tamamen backend tarafında yapılır,
harici bir konum API’sine bağımlı değildir.

---

## Planlanan Geliştirmeler

- Kullanıcının gerçek konumuna göre “Near Me”
- Emergency (acil durum) sayfası
- Culture & Travel Tips (SIM kart, para, kültür)
- Admin panel
- Mobil uygulama (API entegrasyonu)

---

## Geliştirici

**Emre Aktaş**  
Bilgisayar Mühendisliği Öğrencisi
