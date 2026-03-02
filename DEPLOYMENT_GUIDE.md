# KulturPlatform Deployment Guide

## ?? Deployment Checklist

### Ön Haz?rl?k (Bu Bilgisayarda Yap?lacak)

#### 1. Veritaban? Backup'? Al?n
```sql
-- SQL Server Management Studio'da:
-- Database'e sa? t?k -> Tasks -> Back Up
-- veya komut ile:
BACKUP DATABASE [KulturPlatformDb] 
TO DISK = 'C:\Backup\KulturPlatformDb.bak'
WITH FORMAT, INIT, NAME = 'Full Backup of KulturPlatformDb';
```

**? TODO:** Backup dosyas?n? (.bak) deployment yapacak ki?iye gönderin.

#### 2. Mevcut Connection String'i Kaydedin
`appsettings.json` dosyas?ndaki ba?lant? dizesini kaydedin:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=KulturPlatformDb;Trusted_Connection=true;TrustServerCertificate=true"
}
```

#### 3. Email/JWT Ayarlar?n? Toplay?n
A?a??daki bilgileri haz?rlay?n:
- ? Azure Communication Services connection string (email için)
- ? JWT Secret Key
- ? JWT Issuer & Audience
- ? File storage path/container bilgileri

**?? Güvenlik:** Bu bilgileri **?ifreli** bir ?ekilde payla??n (örn: LastPass, 1Password).

#### 4. Admin Kullan?c? Bilgileri
?lk sistem admin bilgilerini haz?rlay?n:
```json
{
  "email": "admin@kpf.de",
  "password": "Güvenli?ifre123!",
  "role": "SystemAdmin"
}
```

---

## ?? Deployment Yapacak Ki?i ?çin Talimatlar

### ADIM 1: Azure Kaynaklar? Olu?turma

#### 1.1 Azure Portal'da Gerekli Servisler

**A. SQL Database Olu?turma**
```
1. Azure Portal -> Create a resource -> SQL Database
2. Ayarlar:
   - Database name: kulturplatform-db
   - Server: Yeni server olu?tur
   - Authentication: SQL Authentication
   - Admin login: sqladmin
   - Admin password: [güçlü ?ifre]
   - Pricing tier: Basic/Standard (ihtiyaca göre)
   - Geo-replication: ?ste?e ba?l?
```

**B. App Service Olu?turma**
```
1. Azure Portal -> Create a resource -> Web App
2. Ayarlar:
   - Name: kulturplatform-api
   - Runtime: .NET 10
   - OS: Windows veya Linux
   - Region: West Europe (yak?n lokasyon)
   - Pricing tier: B1 veya üzeri (production için)
```

**C. Azure Communication Services (Email)**
```
1. Azure Portal -> Create a resource -> Communication Services
2. Email Service ekleyin
3. Custom domain (kpf.de) veya Azure managed domain
4. Connection string'i kopyalay?n
```

**D. Azure Storage Account (Dosya/Resim Depolama - Opsiyonel)**
```
1. Azure Portal -> Create a resource -> Storage Account
2. Container olu?turun: images, documents
3. Access key/connection string kopyalay?n
```

---

### ADIM 2: Veritaban? Kurulumu

#### 2.1 Database Restore
```sql
-- Azure SQL Database'e SSMS ile ba?lan?n:
-- Server: [server-name].database.windows.net
-- Authentication: SQL Authentication
-- Username: sqladmin
-- Password: [yukar?da belirledi?iniz ?ifre]

-- Database restore (Azure SQL'de farkl? yöntem):
-- 1. Backup dosyas?n? Azure Blob Storage'a yükleyin
-- 2. RESTORE komutunu çal??t?r?n veya Azure Portal'dan restore edin
```

**Alternatif: Migration Script**
```bash
# E?er .bak restore çal??mazsa, migration'lar? çal??t?r?n:

# 1. Kod repository'sini clone edin
git clone https://github.com/ademkarakas/KulturPlatform.git
cd KulturPlatform

# 2. Connection string'i güncelleyin (appsettings.Production.json)
# 3. Migration'lar? uygulay?n:
dotnet ef database update --project KulturPlatform.Infrastructure --startup-project KulturPlatform.API --context AppDbContext
```

#### 2.2 Firewall Ayarlar?
Azure SQL Server'da:
- Azure Portal -> SQL Server -> Firewalls and virtual networks
- "Allow Azure services" aç?k olmal?
- Kendi IP'nizi ekleyin (yönetim için)

---

### ADIM 3: Configuration

#### 3.1 App Service Configuration (Environment Variables)

Azure Portal -> App Service -> Configuration -> Application settings

A?a??daki ayarlar? ekleyin:

**Connection Strings:**
```
Name: DefaultConnection
Value: Server=tcp:[server-name].database.windows.net,1433;Initial Catalog=kulturplatform-db;Persist Security Info=False;User ID=sqladmin;Password=[password];MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;
Type: SQLAzure
```

**Application Settings:**
```json
{
  "JwtSettings__SecretKey": "[32+ karakter güçlü anahtar]",
  "JwtSettings__Issuer": "KulturPlatform",
  "JwtSettings__Audience": "KulturPlatformApp",
  "JwtSettings__ExpirationHours": "24",
  
  "AzureCommunicationServices__ConnectionString": "[Azure Communication Services connection string]",
  "AzureCommunicationServices__SenderEmail": "noreply@kpf.de",
  
  "FileStorage__BasePath": "/home/site/wwwroot/uploads",
  "FileStorage__MaxFileSize": "10485760",
  
  "ASPNETCORE_ENVIRONMENT": "Production"
}
```

**?? Güvenlik:** Production'da `appsettings.json` yerine **Environment Variables** kullan?n!

---

### ADIM 4: Deployment

#### 4.1 Otomatik Deployment (Önerilen)

**GitHub Actions ile CI/CD:**

Repository'de `.github/workflows/azure-deploy.yml` dosyas? olu?turun:

```yaml
name: Deploy to Azure

on:
  push:
    branches: [ main, production ]

jobs:
  build-and-deploy:
    runs-on: ubuntu-latest
    
    steps:
    - uses: actions/checkout@v3
    
    - name: Setup .NET
      uses: actions/setup-dotnet@v3
      with:
        dotnet-version: '10.0.x'
    
    - name: Restore dependencies
      run: dotnet restore
    
    - name: Build
      run: dotnet build --configuration Release --no-restore
    
    - name: Test
      run: dotnet test --no-restore --verbosity normal
    
    - name: Publish
      run: dotnet publish KulturPlatform.API/KulturPlatform.API.csproj -c Release -o ${{github.workspace}}/publish
    
    - name: Deploy to Azure Web App
      uses: azure/webapps-deploy@v2
      with:
        app-name: 'kulturplatform-api'
        publish-profile: ${{ secrets.AZURE_WEBAPP_PUBLISH_PROFILE }}
        package: ${{github.workspace}}/publish
```

**Publish Profile Alma:**
1. Azure Portal -> App Service -> Get publish profile
2. XML dosyas?n? indirin
3. GitHub -> Settings -> Secrets -> New repository secret
4. Name: `AZURE_WEBAPP_PUBLISH_PROFILE`
5. Value: XML içeri?ini yap??t?r?n

#### 4.2 Manuel Deployment

**Visual Studio'dan:**
```
1. Solution'da API projesine sa? t?k
2. Publish -> Azure -> Azure App Service (Windows/Linux)
3. Azure hesab?yla giri? yap?n
4. App Service seçin: kulturplatform-api
5. Publish'e t?klay?n
```

**CLI ile:**
```bash
# Azure CLI ile login
az login

# Build & publish
dotnet publish KulturPlatform.API/KulturPlatform.API.csproj -c Release -o ./publish

# Zip olu?tur
cd publish
zip -r ../deploy.zip .
cd ..

# Deploy
az webapp deployment source config-zip \
  --resource-group kulturplatform-rg \
  --name kulturplatform-api \
  --src deploy.zip
```

---

### ADIM 5: Post-Deployment

#### 5.1 Database Seed (?lk Admin Kullan?c?)

**Seçenek A: Migration ile**
`KulturPlatform.Infrastructure/Migrations/SeedData.cs` olu?turun ve ilk admin'i ekleyin.

**Seçenek B: SQL ile**
```sql
-- Azure SQL Database'e ba?lan?n ve çal??t?r?n:

INSERT INTO Admins (Id, Email, PasswordHash, Name, Role, IsActive, CreatedAt)
VALUES (
    NEWID(),
    'admin@kpf.de',
    '$2a$11$[BCrypt hash - Password.Create("?ifre") ile olu?turun]',
    'System Administrator',
    'SystemAdmin',
    1,
    GETUTCDATE()
);
```

**Seçenek C: API Endpoint ile**
E?er bir registration endpoint varsa, ilk kullan?c?y? olu?turun.

#### 5.2 Health Check
```bash
# API'nin çal??t???n? do?rulay?n:
curl https://kulturplatform-api.azurewebsites.net/health

# Swagger'a eri?im (development/staging için):
https://kulturplatform-api.azurewebsites.net/swagger
```

#### 5.3 Test Senaryolar?

**1. Login Test**
```bash
curl -X POST https://kulturplatform-api.azurewebsites.net/api/Auth/login \
  -H "Content-Type: application/json" \
  -d '{
    "email": "admin@kpf.de",
    "password": "Güvenli?ifre123!"
  }'
```

**Beklenen:** 200 OK + JWT token

**2. Authorized Endpoint Test**
```bash
curl -X GET https://kulturplatform-api.azurewebsites.net/api/Dashboard/overview \
  -H "Authorization: Bearer [token]"
```

**Beklenen:** 200 OK + dashboard data

**3. Database Connectivity**
```bash
curl -X GET https://kulturplatform-api.azurewebsites.net/api/Admins \
  -H "Authorization: Bearer [token]"
```

**Beklenen:** Admin listesi

---

### ADIM 6: Frontend Deployment (Ayr? Proje ise)

E?er React/Next.js frontend'iniz varsa:

#### 6.1 Azure Static Web Apps
```
1. Azure Portal -> Create a resource -> Static Web Apps
2. GitHub repository'sini ba?lay?n
3. Build settings:
   - App location: /frontend
   - API location: (bo? - ayr? API var)
   - Output location: build veya .next
4. Environment variables:
   - NEXT_PUBLIC_API_URL: https://kulturplatform-api.azurewebsites.net
```

#### 6.2 CORS Ayarlar?
Backend API'de CORS'u production domain için aç?n:

`appsettings.Production.json`:
```json
{
  "AllowedOrigins": [
    "https://www.kpf.de",
    "https://kpf.de"
  ]
}
```

---

## ?? Güvenlik Kontrol Listesi

- [ ] Production'da `ASPNETCORE_ENVIRONMENT=Production`
- [ ] Sensitive data (password, keys) environment variables'da
- [ ] HTTPS zorlan?yor
- [ ] CORS sadece belirli origin'ler için aç?k
- [ ] SQL injection korumas? var (EF Core kullan?l?yor ?)
- [ ] Rate limiting ayarlanm??
- [ ] Logging/monitoring aktif (Application Insights önerilen)
- [ ] Database backup stratejisi belirlenmi?
- [ ] SSL/TLS certificate geçerli

---

## ?? Monitoring & Maintenance

### Application Insights Kurulumu
```bash
# NuGet package ekle (API projesine):
dotnet add package Microsoft.ApplicationInsights.AspNetCore

# appsettings.Production.json:
{
  "ApplicationInsights": {
    "ConnectionString": "[Azure portal'dan al?n]"
  }
}

# Program.cs'de:
builder.Services.AddApplicationInsightsTelemetry();
```

### Log Monitoring
Azure Portal -> App Service -> Log stream'den real-time log'lar? izleyin.

### Database Performance
Azure Portal -> SQL Database -> Query Performance Insight

---

## ?? Troubleshooting

### Problem: 500 Internal Server Error
```bash
# Log'lar? kontrol edin:
az webapp log tail --name kulturplatform-api --resource-group kulturplatform-rg

# Yayg?n sebepler:
# - Connection string yanl??
# - Database eri?im hatas? (firewall)
# - Missing environment variables
```

### Problem: 401 Unauthorized
```bash
# JWT token'? decode edin: https://jwt.io
# Kontrol edin:
# - Token expire olmam?? m??
# - Issuer/Audience do?ru mu?
# - Secret key production'da ayn? m??
```

### Problem: Database connection timeout
```sql
-- Azure SQL firewall'u kontrol edin
-- App Service'in outbound IP'sini SQL firewall'a ekleyin
```

---

## ?? ?leti?im

Deployment s?ras?nda sorun olursa:
- **Email:** [sizin email'iniz]
- **Phone:** [sizin telefon numaran?z]
- **GitHub Issues:** https://github.com/ademkarakas/KulturPlatform/issues

---

## ?? Ek Kaynaklar

- [Azure App Service Docs](https://docs.microsoft.com/azure/app-service/)
- [Azure SQL Database Docs](https://docs.microsoft.com/azure/sql-database/)
- [ASP.NET Core Deployment](https://docs.microsoft.com/aspnet/core/host-and-deploy/azure-apps/)

---

**Son Güncelleme:** {tarih}
**Haz?rlayan:** {isim}
