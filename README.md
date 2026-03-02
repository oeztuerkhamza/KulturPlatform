# KulturPlatform - Backend API

KulturPlatform für Förderverein e.V. için geli?tirilmi? ASP.NET Core web API projesi.

---

## ?? Deployment Dökümanlar?

Projenizi deploy etmek için haz?rlanm?? kapsaml? guide'lar:

### ?? [DEPLOYMENT_README.md](./DEPLOYMENT_README.md) - BURADAN BA?LAYIN! ?
Hangi deployment yöntemini kullanaca??n?za karar verin (5 dakika).

### ?? [DEPLOYMENT_COMPARISON.md](./DEPLOYMENT_COMPARISON.md) 
Azure vs Generic Hosting kar??la?t?rmas?, maliyet analizi (€10-170/ay).

### ?? [DEPLOYMENT_GUIDE.md](./DEPLOYMENT_GUIDE.md)
Azure'da deploy için detayl? guide (~4000 kelime).

### ?? [DEPLOYMENT_GENERIC_HOSTING.md](./DEPLOYMENT_GENERIC_HOSTING.md)
Hetzner/DigitalOcean/VPS'de Docker/IIS/Nginx ile deploy (~5000 kelime).

### ? [DEPLOYMENT_CHECKLIST_TR.md](./DEPLOYMENT_CHECKLIST_TR.md)
Türkçe h?zl? checklist - Deployment yapacak ki?i için.

---

## ?? H?zl? Ba?lang?ç (Development)

### Gereksinimler
- .NET 10 SDK
- SQL Server 2019+
- Visual Studio 2022+ / VS Code

### Kurulum
```bash
git clone https://github.com/ademkarakas/KulturPlatform.git
cd KulturPlatform
dotnet restore
dotnet ef database update --project KulturPlatform.Infrastructure --startup-project KulturPlatform.API
cd KulturPlatform.API
dotnet run
```

Swagger UI: `https://localhost:7189/swagger`

---

## ?? Teknolojiler

- .NET 10 (ASP.NET Core)
- Entity Framework Core
- MediatR (CQRS)
- FluentValidation
- JWT Authentication
- BCrypt (Password hashing)
- SQL Server / PostgreSQL

---

## ??? Mimari

**Clean Architecture + DDD**
```
KulturPlatform/
??? API/                # Controllers, Middleware
??? Application/        # Business Logic (CQRS)
??? Domain/             # Entities, Value Objects
??? Infrastructure/     # Data Access (EF Core)
??? Tests/              # Unit & Integration Tests
```

---

## ?? ?leti?im

**GitHub:** https://github.com/ademkarakas/KulturPlatform  
**Issues:** https://github.com/ademkarakas/KulturPlatform/issues

---

**Detayl? dokümantasyon için deployment guide'lar?n? inceleyin.**
