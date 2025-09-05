# Müşteri Bakiye Seyri - Kurulum Kılavuzu

## 🚀 Hızlı Kurulum (Önerilen)

### Docker ile Tek Komut Kurulum

```bash
# 1. Zip dosyasını çıkartın
unzip MusteriBakiyeSeyri.zip
cd MusteriBakiyeSeyri

# 2. Docker Compose ile başlatın
docker-compose up --build

# 3. Tarayıcıda açın
http://localhost:8080
```

**Bu kadar!** Sistem otomatik olarak:
- ✅ PostgreSQL veritabanını kuracak
- ✅ Tabloları oluşturacak
- ✅ Örnek verileri yükleyecek
- ✅ Web uygulamasını başlatacak

## 🔧 Manuel Kurulum

### Gereksinimler
- .NET 9.0 SDK
- PostgreSQL 16+

### Adımlar

```bash
# 1. Veritabanını oluşturun
createdb MusteriBakiyeSeyri

# 2. Connection string'i güncelleyin
# Web/appsettings.Development.json dosyasında:
"DefaultConnection": "Host=localhost;Database=MusteriBakiyeSeyri;Username=postgres;Password=yourpassword"

# 3. Veritabanı script'ini çalıştırın
psql -d MusteriBakiyeSeyri -f Scripts/init-db.sql

# 4. Projeyi çalıştırın
dotnet run --project Web
```

## 📊 Test Verileri

Sistem 4 örnek müşteri ile gelir:

1. **Nano Bilgi Sistemleri (127098)** - 7 fatura, karmaşık senaryo
2. **Asya Halıcılık AŞ (127747)** - 3 fatura, basit senaryo
3. **Veri Bilişim LTD (127269)** - 4 fatura, ödenmemiş faturalar
4. **Expert Gümrükleme (129914)** - 2 fatura, minimal senaryo

## 🧪 Test Çalıştırma

```bash
# Tüm testleri çalıştır
dotnet test

# Sadece unit testler
dotnet test Tests/Application.UnitTests
```

## 🐛 Sorun Giderme

### Docker Sorunları
```bash
# Docker container'ları temizle
docker-compose down -v
docker system prune -f

# Tekrar başlat
docker-compose up --build
```

### Veritabanı Sorunları
```bash
# PostgreSQL container'ını yeniden başlat
docker-compose restart postgres

# Logları kontrol et
docker-compose logs postgres
docker-compose logs web
```

### Port Sorunları
Eğer 8080 portu kullanımda ise, docker-compose.yml dosyasında:
```yaml
ports:
  - "8081:8080"  # 8081 portunu kullan
```

## 📞 İletişim

Herhangi bir sorun yaşarsanız, lütfen iletişime geçin.
Zafer Ali Günbey - 05462804150 - zaferaligunbey@gmail.com

---
