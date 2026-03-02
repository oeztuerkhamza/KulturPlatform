# ? KulturPlatform Deployment H?zl? Kontrol Listesi

## ?? Gönderece?iniz Dosyalar/Bilgiler

### 1. Veritaban?
- [ ] **KulturPlatformDb.bak** - Veritaban? backup dosyas?
  - Konum: SQL Server Management Studio -> Database -> Right Click -> Tasks -> Back Up
  - Dosya boyutu: ~_____ MB

### 2. Konfigürasyon Bilgileri (?ifreli Gönder!)
```json
{
  "Database": {
    "CurrentServer": ".",
    "DatabaseName": "KulturPlatformDb",
    "Note": "Trusted_Connection kullan?l?yor (Windows Auth)"
  },
  
  "JWT": {
    "SecretKey": "[appsettings.json'dan kopyalay?n]",
    "Issuer": "KulturPlatform",
    "Audience": "KulturPlatformApp",
    "ExpirationHours": "24"
  },
  
  "Email": {
    "AzureCommunicationServicesConnectionString": "[appsettings.json'dan]",
    "SenderEmail": "noreply@kpf.de"
  },
  
  "FileStorage": {
    "CurrentPath": "[local path]",
    "MaxFileSize": "10485760"
  },
  
  "AdminUser": {
    "Email": "admin@kpf.de",
    "Password": "[güvenli ?ifre]",
    "Role": "SystemAdmin",
    "Note": "Bu bilgileri deployment sonras? ilk admin kullan?c? olu?turmak için kullan?n"
  }
}
```

### 3. Repository Bilgileri
- **GitHub:** https://github.com/ademkarakas/KulturPlatform
- **Branch:** main / Kult
- **Son Commit:** [commit hash]

---

## ?? Deployment Yapacak Ki?i ?çin H?zl? Ad?mlar

### ADIM 1: Azure Kaynaklar? (30 dk)
```
Azure Portal'da olu?tur:
??? Resource Group: kulturplatform-rg
??? SQL Server + Database
?   ??? Server: kulturplatform-sqlserver.database.windows.net
?   ??? Database: kulturplatform-db
?   ??? Pricing: Basic/Standard
??? App Service (Web App)
?   ??? Name: kulturplatform-api
?   ??? Runtime: .NET 10
?   ??? Plan: B1 veya üzeri
??? Communication Services (Email)
    ??? Connection string'i kaydet
```

### ADIM 2: Database Setup (15 dk)
```bash
1. Azure SQL Server Firewall ayarlar?:
   - "Allow Azure services" -> ON
   - Kendi IP'nizi ekleyin

2. Database Restore:
   - SSMS ile Azure SQL'e ba?lan?n
   - Backup'? restore edin veya migration'lar? çal??t?r?n:
     dotnet ef database update --startup-project KulturPlatform.API

3. Connection String'i test edin
```

### ADIM 3: App Service Configuration (10 dk)
```
Azure Portal -> App Service -> Configuration:

Connection Strings:
  DefaultConnection = Server=tcp:kulturplatform-sqlserver.database.windows.net,1433;...

Application Settings:
  JwtSettings__SecretKey = [güçlü anahtar]
  JwtSettings__Issuer = KulturPlatform
  JwtSettings__Audience = KulturPlatformApp
  AzureCommunicationServices__ConnectionString = [connection string]
  ASPNETCORE_ENVIRONMENT = Production
```

### ADIM 4: Deploy (20 dk)
```bash
# Yöntem 1: Visual Studio
Right-click API project -> Publish -> Azure -> Select App Service

# Yöntem 2: GitHub Actions (önerilen)
1. .github/workflows/azure-deploy.yml olu?tur
2. Publish Profile'? GitHub Secrets'a ekle
3. Push yap, otomatik deploy olsun

# Yöntem 3: CLI
dotnet publish -c Release
az webapp deployment source config-zip --src publish.zip
```

### ADIM 5: ?lk Admin Kullan?c? (5 dk)
```sql
-- Azure SQL'de çal??t?r:
INSERT INTO Admins (Id, Email, PasswordHash, Name, Role, IsActive, CreatedAt)
VALUES (
    NEWID(),
    'admin@kpf.de',
    '$2a$11$[BCrypt hash]',  -- Password.Create() ile olu?turulmal?
    'System Administrator',
    'SystemAdmin',
    1,
    GETUTCDATE()
);
```

### ADIM 6: Test (10 dk)
```bash
# Health check
curl https://kulturplatform-api.azurewebsites.net/health

# Login test
curl -X POST https://kulturplatform-api.azurewebsites.net/api/Auth/login \
  -H "Content-Type: application/json" \
  -d '{"email":"admin@kpf.de","password":"[?ifre]"}'

# Dashboard test (token ile)
curl https://kulturplatform-api.azurewebsites.net/api/Dashboard/overview \
  -H "Authorization: Bearer [token]"
```

---

## ?? Önemli Notlar

### Güvenlik
- ? `appsettings.json` dosyalar?n? GitHub'a push ETMEY?N
- ? Production secrets'lar? Azure App Service Configuration'da saklay?n
- ? SQL Server'da sadece gerekli IP'lere eri?im verin
- ? HTTPS zorunlu olmal?

### Performans
- Database: Basic tier yeterli (ba?lang?ç için)
- App Service: B1 plan minimum (production için S1 önerilir)
- Auto-scaling: ?htiyaç durumunda aktive edin

### Backup
- SQL Database: Otomatik daily backup aktif (Azure)
- Retention: 7-35 gün aras? ayarlay?n
- Manuel backup: Önemli de?i?iklikler öncesi

---

## ?? Deployment S?ras?nda Sorun mu Var?

### S?k Kar??la??lan Hatalar

**1. "Cannot open server" hatas?**
```
Çözüm: SQL Server firewall'a App Service outbound IP'sini ekle
Azure Portal -> SQL Server -> Firewalls -> Add client IP
```

**2. "401 Unauthorized" after login**
```
Çözüm: JWT settings kontrol et
- Secret key production'da farkl? olabilir
- Issuer/Audience e?le?meli
```

**3. "500 Internal Server Error"**
```
Çözüm: Log'lar? kontrol et
Azure Portal -> App Service -> Log stream
Ya da: az webapp log tail --name kulturplatform-api
```

**4. CORS hatas? (frontend'den)**
```
Çözüm: Program.cs'de CORS ayarlar?n? güncelle
AllowedOrigins'e production domain'i ekle
```

---

## ?? Post-Deployment Checklist

- [ ] Login çal???yor
- [ ] Dashboard aç?l?yor
- [ ] Profil güncelleme çal???yor
- [ ] ?ifre de?i?tirme çal???yor
- [ ] Email gönderimi çal???yor (test et)
- [ ] Dosya upload çal???yor
- [ ] Database backup otomatik
- [ ] Monitoring/Logging aktif
- [ ] SSL certificate geçerli
- [ ] Domain custom (e?er varsa)

---

## ?? Frontend Deploy (Ayr? yap?lacaksa)

```bash
# React/Next.js build
npm run build

# Azure Static Web Apps
az staticwebapp create \
  --name kulturplatform-frontend \
  --resource-group kulturplatform-rg

# Environment variable
NEXT_PUBLIC_API_URL=https://kulturplatform-api.azurewebsites.net
```

---

## ?? Toplam Süre Tahmini
- Azure kaynaklar? olu?turma: ~30 dk
- Database setup: ~15 dk
- Configuration: ~10 dk
- Deployment: ~20 dk
- Test: ~10 dk
**Toplam: ~1.5 saat**

---

## ? Deployment Tamamland?ktan Sonra

### Bana Gönderilecek Bilgiler:
1. ? API URL: https://kulturplatform-api.azurewebsites.net
2. ? Database server name: [server-name].database.windows.net
3. ? ?lk admin email/?ifre: [test için]
4. ? Test sonuçlar? (login, dashboard vb.)
5. ? Herhangi bir hata/uyar? var m??

---

**Haz?rlayan:** [?sminiz]
**Tarih:** [Tarih]
**?leti?im:** [Email/Phone]
