# TestCaseApi

.NET 8 tabanlı, PostgreSQL kullanan ve dış bir LLM/Ollama benzeri servisten test senaryosu üreten küçük bir REST API.

## Gereksinimler
- .NET 8 SDK
- PostgreSQL 14+ (çalışan bir sunucu ve erişilebilir bir veritabanı)
- İsteğe bağlı: AI servisi (varsayılan olarak `http://localhost:8000/api/generate` adresine çağrı yapılıyor)

## Kurulum
1) Bağımlılıkları indir:
```bash
dotnet restore
```

2) Veritabanı bağlantısını ayarla: `TestCaseApi/appsettings.Development.json` (veya `appsettings.json`) içindeki `DefaultConnection` değerini düzenleyin. Örnek:
```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=testcase_db;Username=postgres;Password=sifre"
}
```

3) EF Core araçlarını kurun (yüklü değilse):
```bash
dotnet tool install --global dotnet-ef
```

4) Migrasyonları veritabanına uygulayın:
```bash
cd TestCaseApi
dotnet ef database update
```

5) Uygulamayı başlatın:
```bash
dotnet run --project TestCaseApi
```
Varsayılan olarak Swagger UI geliştirme ortamında açılır: `https://localhost:7204/swagger` (veya konsoldaki URL).

## Yapılandırma (AI servisi)
`appsettings*.json` altındaki `Ollama` bölümü AI isteğini hazırlar:
- `BaseUrl`: AI servisi ana adresi (örn. `http://localhost:8000`)
- `GeneratePath`: POST isteği atılan yol (örn. `/api/generate`)
- `Requirement`, `Framework`, `Model`: Gönderilen JSON alan adları
- `DefaultModel`: `model` boş bırakılırsa kullanılacak varsayılan model

İçerik, `GenerateAsync` çağrısı ile JSON olarak AI servisine gönderilir; cevapta gelen test senaryosu ve script verisi veritabanına yazılır.

## API Uç Noktaları
- `POST /api/ai/generate`  
  İstek gövdesi:
  ```json
  {
    "requirement": "Örnek kullanıcı kaydı senaryosu",
    "framework": "playwright",
    "model": "qwen"   // isteğe bağlı
  }
  ```
  Yanıt: Üretilen test senaryosu (`test_case` başlığı, adımlar, beklenen sonuç), kullanılan model, ve script bilgileri. Kaydedilen kayıtlar PostgreSQL'e eklenir.

- `GET /api/ai/list`  
  Veritabanındaki tüm üretilmiş test senaryolarını döndürür.

## Geliştirici Notları
- CORS varsayılan politikada tüm origin/header/metodlara açık.
- Yeni sütunlar için ek migrasyon gerektiğinde `dotnet ef migrations add <MigrationName>` komutu kullanılabilir.
