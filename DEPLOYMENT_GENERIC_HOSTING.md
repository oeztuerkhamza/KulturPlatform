# KulturPlatform - Generic Hosting Deployment Guide
## (Hetzner, DigitalOcean, AWS, VPS, On-Premise)

Bu guide Azure d???ndaki hosting sa?lay?c?lar? için deployment talimatlar?n? içerir.

---

## ?? Hangi Hosting Sa?lay?c?lar? ?çin Uygundur?

- ? Hetzner
- ? DigitalOcean
- ? AWS EC2/RDS
- ? Contabo
- ? OVH
- ? Herhangi bir VPS provider
- ? On-Premise server
- ? Shared hosting (s?n?rl? - IIS gerektirir)

---

## ?? Öngereksinimler

### Sunucu Gereksinimleri (Minimum)
```
CPU: 2 core
RAM: 4 GB
Disk: 50 GB SSD
OS: Windows Server 2019+ veya Ubuntu 20.04+
Network: Sabit IP + Domain name
```

### Yaz?l?m Gereksinimleri
- .NET 10 Runtime (ASP.NET Core)
- SQL Server 2019+ veya PostgreSQL 14+
- Web Server: IIS (Windows) veya Nginx/Apache (Linux)
- SSL Certificate (Let's Encrypt ücretsiz)

---

## ?? 3 Farkl? Deployment Yöntemi

### Yöntem 1: Docker (Önerilen - En Kolay) ??
### Yöntem 2: Windows Server + IIS
### Yöntem 3: Linux + Nginx

---

# YÖNTEM 1: Docker Deployment (Platform Ba??ms?z) ??

## Avantajlar?
- ? Platform ba??ms?z (Windows/Linux/Mac)
- ? Kolay setup ve güncelleme
- ? Izolasyon ve güvenlik
- ? Otomatik restart
- ? Kolayca scale edilebilir

## Ad?m 1: Dockerfile Olu?tur

Proje root'unda `Dockerfile` olu?turun:

```dockerfile
# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy csproj files and restore
COPY ["KulturPlatform.API/KulturPlatform.API.csproj", "KulturPlatform.API/"]
COPY ["KulturPlatform.Application/KulturPlatform.Application.csproj", "KulturPlatform.Application/"]
COPY ["KulturPlatform.Domain/KulturPlatform.Domain.csproj", "KulturPlatform.Domain/"]
COPY ["KulturPlatform.Infrastructure/KulturPlatform.Infrastructure.csproj", "KulturPlatform.Infrastructure/"]

RUN dotnet restore "KulturPlatform.API/KulturPlatform.API.csproj"

# Copy everything else and build
COPY . .
WORKDIR "/src/KulturPlatform.API"
RUN dotnet build "KulturPlatform.API.csproj" -c Release -o /app/build

# Publish
FROM build AS publish
RUN dotnet publish "KulturPlatform.API.csproj" -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
EXPOSE 80
EXPOSE 443

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KulturPlatform.API.dll"]
```

## Ad?m 2: docker-compose.yml Olu?tur

```yaml
version: '3.8'

services:
  # SQL Server Database
  sqlserver:
    image: mcr.microsoft.com/mssql/server:2022-latest
    container_name: kulturplatform-db
    environment:
      - ACCEPT_EULA=Y
      - SA_PASSWORD=YourStrong!Passw0rd
      - MSSQL_PID=Express
    ports:
      - "1433:1433"
    volumes:
      - sqldata:/var/opt/mssql
    restart: unless-stopped

  # API Application
  api:
    build:
      context: .
      dockerfile: Dockerfile
    container_name: kulturplatform-api
    environment:
      - ASPNETCORE_ENVIRONMENT=Production
      - ASPNETCORE_URLS=http://+:80
      - ConnectionStrings__DefaultConnection=Server=sqlserver,1433;Database=KulturPlatformDb;User Id=sa;Password=YourStrong!Passw0rd;TrustServerCertificate=true
      - JwtSettings__SecretKey=${JWT_SECRET_KEY}
      - JwtSettings__Issuer=KulturPlatform
      - JwtSettings__Audience=KulturPlatformApp
      - JwtSettings__ExpirationHours=24
      - AzureCommunicationServices__ConnectionString=${EMAIL_CONNECTION_STRING}
      - AzureCommunicationServices__SenderEmail=noreply@kpf.de
    ports:
      - "8080:80"
    depends_on:
      - sqlserver
    restart: unless-stopped
    volumes:
      - ./uploads:/app/uploads

  # Nginx Reverse Proxy (SSL termination)
  nginx:
    image: nginx:alpine
    container_name: kulturplatform-nginx
    ports:
      - "80:80"
      - "443:443"
    volumes:
      - ./nginx.conf:/etc/nginx/nginx.conf:ro
      - ./ssl:/etc/nginx/ssl:ro
    depends_on:
      - api
    restart: unless-stopped

volumes:
  sqldata:
```

## Ad?m 3: .env Dosyas? Olu?tur

```env
# .env dosyas? (?ifreli saklanmal?!)
JWT_SECRET_KEY=YourSuperSecretKeyThatIsAtLeast32CharactersLong!
EMAIL_CONNECTION_STRING=endpoint=https://...;accesskey=...
```

## Ad?m 4: Nginx Configuration

`nginx.conf`:

```nginx
events {
    worker_connections 1024;
}

http {
    upstream api {
        server api:80;
    }

    # HTTP -> HTTPS redirect
    server {
        listen 80;
        server_name kpf.de www.kpf.de;
        return 301 https://$server_name$request_uri;
    }

    # HTTPS
    server {
        listen 443 ssl http2;
        server_name kpf.de www.kpf.de;

        ssl_certificate /etc/nginx/ssl/fullchain.pem;
        ssl_certificate_key /etc/nginx/ssl/privkey.pem;
        ssl_protocols TLSv1.2 TLSv1.3;
        ssl_ciphers HIGH:!aNULL:!MD5;

        client_max_body_size 20M;

        location / {
            proxy_pass http://api;
            proxy_http_version 1.1;
            proxy_set_header Upgrade $http_upgrade;
            proxy_set_header Connection keep-alive;
            proxy_set_header Host $host;
            proxy_set_header X-Real-IP $remote_addr;
            proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
            proxy_set_header X-Forwarded-Proto $scheme;
            proxy_cache_bypass $http_upgrade;
        }
    }
}
```

## Ad?m 5: Deploy

```bash
# 1. Sunucuya ba?lan (SSH)
ssh root@your-server-ip

# 2. Docker & Docker Compose kur (Ubuntu)
curl -fsSL https://get.docker.com -o get-docker.sh
sh get-docker.sh
apt-get install docker-compose-plugin

# 3. Projeyi clone et
git clone https://github.com/ademkarakas/KulturPlatform.git
cd KulturPlatform

# 4. .env dosyas?n? olu?tur
nano .env
# (JWT_SECRET_KEY ve EMAIL_CONNECTION_STRING gir)

# 5. SSL sertifikas? al (Let's Encrypt)
apt-get install certbot
certbot certonly --standalone -d kpf.de -d www.kpf.de
# Sertifikalar? /etc/letsencrypt/live/kpf.de/ alt?nda olacak
mkdir -p ssl
cp /etc/letsencrypt/live/kpf.de/fullchain.pem ssl/
cp /etc/letsencrypt/live/kpf.de/privkey.pem ssl/

# 6. Build ve ba?lat
docker compose up -d --build

# 7. Log'lar? kontrol et
docker compose logs -f api

# 8. Database migration
docker compose exec api dotnet ef database update
```

## Ad?m 6: ?lk Admin Kullan?c?

```bash
# SQL Server container'a ba?lan
docker exec -it kulturplatform-db /opt/mssql-tools/bin/sqlcmd \
  -S localhost -U sa -P 'YourStrong!Passw0rd'

# SQL komutlar?:
USE KulturPlatformDb;
GO

INSERT INTO Admins (Id, Email, PasswordHash, Name, Role, IsActive, CreatedAt)
VALUES (
    NEWID(),
    'admin@kpf.de',
    '$2a$11$[BCrypt hash]',
    'System Administrator',
    'SystemAdmin',
    1,
    GETUTCDATE()
);
GO
```

---

# YÖNTEM 2: Windows Server + IIS

## Ad?m 1: Gerekli Yaz?l?mlar

```powershell
# 1. .NET 10 Hosting Bundle
# https://dotnet.microsoft.com/download/dotnet/10.0
# ASP.NET Core Runtime + Hosting Bundle'? indirip kurun

# 2. IIS Kurulumu
Install-WindowsFeature -name Web-Server -IncludeManagementTools

# 3. URL Rewrite Module (HTTPS redirect için)
# https://www.iis.net/downloads/microsoft/url-rewrite

# 4. SQL Server Express (veya tam sürüm)
# https://www.microsoft.com/sql-server/sql-server-downloads
```

## Ad?m 2: Database Setup

```sql
-- SQL Server Management Studio'da:

-- 1. Yeni database olu?tur
CREATE DATABASE KulturPlatformDb;
GO

-- 2. Application için login/user olu?tur
CREATE LOGIN kulturapp WITH PASSWORD = 'StrongPassword123!';
GO

USE KulturPlatformDb;
GO

CREATE USER kulturapp FOR LOGIN kulturapp;
GO

ALTER ROLE db_owner ADD MEMBER kulturapp;
GO
```

## Ad?m 3: Application Deploy

```powershell
# 1. Projeyi publish et (development makinesinde)
dotnet publish KulturPlatform.API/KulturPlatform.API.csproj `
  -c Release `
  -o C:\Publish\KulturPlatform

# 2. Publish klasörünü sunucuya kopyala (RDP veya FTP)
# C:\inetpub\wwwroot\kulturplatform\ alt?na

# 3. appsettings.Production.json düzenle
# C:\inetpub\wwwroot\kulturplatform\appsettings.Production.json
```

`appsettings.Production.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=KulturPlatformDb;User Id=kulturapp;Password=StrongPassword123!;TrustServerCertificate=true"
  },
  "JwtSettings": {
    "SecretKey": "YourSuperSecretKeyThatIsAtLeast32CharactersLong!",
    "Issuer": "KulturPlatform",
    "Audience": "KulturPlatformApp",
    "ExpirationHours": "24"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

## Ad?m 4: IIS Configuration

```powershell
# IIS Manager aç?n (inetmgr)

# 1. Yeni Application Pool olu?tur
# - Name: KulturPlatformAppPool
# - .NET CLR Version: No Managed Code
# - Managed Pipeline Mode: Integrated
# - Identity: ApplicationPoolIdentity

# 2. Yeni Website olu?tur
# - Site name: KulturPlatform
# - Physical path: C:\inetpub\wwwroot\kulturplatform
# - Binding: http, port 80, hostname: kpf.de
# - Application Pool: KulturPlatformAppPool

# 3. SSL Certificate ekle (Let's Encrypt veya sat?n al?nan)
# - Win-ACME kullanabilirsiniz (ücretsiz): https://www.win-acme.com/
# - Veya: IIS Manager -> Server Certificates -> Create Certificate Request

# 4. HTTPS Binding ekle
# - Binding: https, port 443, hostname: kpf.de, SSL Certificate: [seçin]
```

## Ad?m 5: Permissions

```powershell
# Application Pool identity'sine database eri?imi ver
icacls "C:\inetpub\wwwroot\kulturplatform" /grant "IIS APPPOOL\KulturPlatformAppPool:(OI)(CI)F"

# Uploads klasörü için write permission
New-Item -Path "C:\inetpub\wwwroot\kulturplatform\uploads" -ItemType Directory
icacls "C:\inetpub\wwwroot\kulturplatform\uploads" /grant "IIS APPPOOL\KulturPlatformAppPool:(OI)(CI)M"
```

## Ad?m 6: Migration

```powershell
cd C:\inetpub\wwwroot\kulturplatform
dotnet KulturPlatform.API.dll ef database update
# Veya migration SQL script'i çal??t?r?n
```

---

# YÖNTEM 3: Linux + Nginx (Ubuntu 22.04)

## Ad?m 1: Sunucu Haz?rl???

```bash
# 1. System update
sudo apt update && sudo apt upgrade -y

# 2. .NET 10 Runtime kur
wget https://dot.net/v1/dotnet-install.sh
chmod +x dotnet-install.sh
./dotnet-install.sh --channel 10.0 --runtime aspnetcore
echo 'export DOTNET_ROOT=$HOME/.dotnet' >> ~/.bashrc
echo 'export PATH=$PATH:$HOME/.dotnet' >> ~/.bashrc
source ~/.bashrc

# 3. Nginx kur
sudo apt install nginx -y

# 4. SQL Server kur (Linux)
wget -qO- https://packages.microsoft.com/keys/microsoft.asc | sudo apt-key add -
sudo add-apt-repository "$(wget -qO- https://packages.microsoft.com/config/ubuntu/22.04/mssql-server-2022.list)"
sudo apt update
sudo apt install -y mssql-server
sudo /opt/mssql/bin/mssql-conf setup
# (Express edition seç, sa password belirle)
```

## Ad?m 2: Application Deploy

```bash
# 1. Kullan?c? olu?tur
sudo useradd -m -s /bin/bash kulturapp
sudo mkdir -p /var/www/kulturplatform
sudo chown kulturapp:kulturapp /var/www/kulturplatform

# 2. Projeyi publish et (development makinesinde)
dotnet publish -c Release -o ./publish

# 3. Sunucuya kopyala (rsync veya scp)
rsync -avz --delete ./publish/ kulturapp@your-server:/var/www/kulturplatform/

# 4. Environment variables
sudo nano /etc/systemd/system/kulturplatform.service
```

`/etc/systemd/system/kulturplatform.service`:
```ini
[Unit]
Description=KulturPlatform API
After=network.target

[Service]
WorkingDirectory=/var/www/kulturplatform
ExecStart=/home/kulturapp/.dotnet/dotnet /var/www/kulturplatform/KulturPlatform.API.dll
Restart=always
RestartSec=10
KillSignal=SIGINT
SyslogIdentifier=kulturplatform
User=kulturapp
Environment=ASPNETCORE_ENVIRONMENT=Production
Environment=ASPNETCORE_URLS=http://localhost:5000
Environment="ConnectionStrings__DefaultConnection=Server=localhost;Database=KulturPlatformDb;User Id=sa;Password=YourSaPassword123!;TrustServerCertificate=true"
Environment="JwtSettings__SecretKey=YourSuperSecretKeyThatIsAtLeast32CharactersLong!"

[Install]
WantedBy=multi-user.target
```

```bash
# Service'i ba?lat
sudo systemctl daemon-reload
sudo systemctl enable kulturplatform
sudo systemctl start kulturplatform
sudo systemctl status kulturplatform
```

## Ad?m 3: Nginx Reverse Proxy

```bash
sudo nano /etc/nginx/sites-available/kulturplatform
```

`/etc/nginx/sites-available/kulturplatform`:
```nginx
server {
    listen 80;
    server_name kpf.de www.kpf.de;
    
    location / {
        proxy_pass http://localhost:5000;
        proxy_http_version 1.1;
        proxy_set_header Upgrade $http_upgrade;
        proxy_set_header Connection keep-alive;
        proxy_set_header Host $host;
        proxy_cache_bypass $http_upgrade;
        proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header X-Forwarded-Proto $scheme;
    }
}
```

```bash
# Site'? aktif et
sudo ln -s /etc/nginx/sites-available/kulturplatform /etc/nginx/sites-enabled/
sudo nginx -t
sudo systemctl restart nginx
```

## Ad?m 4: SSL Certificate (Let's Encrypt)

```bash
# Certbot kur
sudo apt install certbot python3-certbot-nginx -y

# SSL sertifikas? al ve nginx'e otomatik ekle
sudo certbot --nginx -d kpf.de -d www.kpf.de

# Otomatik renewal test
sudo certbot renew --dry-run
```

---

## ?? Yöntemlerin Kar??la?t?rmas?

| Özellik | Docker | Windows+IIS | Linux+Nginx |
|---------|--------|-------------|-------------|
| **Kolay Kurulum** | ????? | ??? | ???? |
| **Kolay Güncelleme** | ????? | ??? | ??? |
| **Performans** | ???? | ????? | ????? |
| **Maliyet** | Dü?ük | Orta (Windows lisans) | Dü?ük |
| **Monitoring** | ???? | ????? | ???? |
| **Platform** | Her yerde | Sadece Windows | Linux |
| **Önerilen** | ? Ba?lang?ç/Orta | ? Enterprise | ? Production |

---

## ?? Email Configuration (Azure D???)

Docker/Generic hosting'de email için alternatifler:

### Seçenek 1: SMTP (Mailgun, SendGrid, AWS SES)

`appsettings.Production.json`:
```json
{
  "EmailSettings": {
    "SmtpServer": "smtp.mailgun.org",
    "SmtpPort": 587,
    "SmtpUsername": "postmaster@kpf.de",
    "SmtpPassword": "[password]",
    "SenderEmail": "noreply@kpf.de",
    "SenderName": "KPF Team",
    "UseSsl": true
  }
}
```

`EmailService.cs` güncelle:
```csharp
public class SmtpEmailService : IEmailService
{
    private readonly SmtpClient _smtpClient;
    
    public SmtpEmailService(IConfiguration config)
    {
        var settings = config.GetSection("EmailSettings");
        _smtpClient = new SmtpClient(settings["SmtpServer"])
        {
            Port = int.Parse(settings["SmtpPort"]),
            Credentials = new NetworkCredential(
                settings["SmtpUsername"], 
                settings["SmtpPassword"]
            ),
            EnableSsl = bool.Parse(settings["UseSsl"])
        };
    }
}
```

### Seçenek 2: SendGrid API

```bash
dotnet add package SendGrid
```

### Seçenek 3: Mailgun API

```bash
dotnet add package RestSharp
```

---

## ?? File Storage (Azure D???)

### Local Disk Storage (Varsay?lan)
Zaten mevcut: `LocalFileStorageService.cs`

### S3-Compatible Storage (MinIO, Backblaze, DigitalOcean Spaces)

```bash
dotnet add package AWSSDK.S3
```

`appsettings.Production.json`:
```json
{
  "S3Storage": {
    "Endpoint": "https://fra1.digitaloceanspaces.com",
    "BucketName": "kulturplatform",
    "AccessKey": "[access-key]",
    "SecretKey": "[secret-key]",
    "Region": "fra1"
  }
}
```

---

## ?? Monitoring & Logging

### Application Insights Alternatifi: Serilog + Seq

```bash
dotnet add package Serilog.AspNetCore
dotnet add package Serilog.Sinks.Seq
```

`Program.cs`:
```csharp
builder.Host.UseSerilog((context, config) =>
{
    config.WriteTo.Console()
          .WriteTo.Seq("http://localhost:5341");
});
```

---

## ?? Güvenlik Checklist (Generic Hosting)

- [ ] Firewall aktif (UFW/iptables/Windows Firewall)
- [ ] Sadece port 80, 443, 22 (SSH) aç?k
- [ ] SSH key authentication (password disabled)
- [ ] fail2ban kurulu (brute-force protection)
- [ ] Automatic security updates
- [ ] Database: localhost'a s?n?rl? (external access kapal?)
- [ ] Strong passwords (20+ karakter)
- [ ] Regular backups (database + files)
- [ ] SSL/TLS certificate otomatik renewal

---

## ?? Troubleshooting (Generic Hosting)

### Docker
```bash
# Container log'lar?
docker compose logs -f api

# Container içine gir
docker compose exec api bash

# Restart
docker compose restart api

# Database'e ba?lan
docker compose exec sqlserver /opt/mssql-tools/bin/sqlcmd -S localhost -U sa
```

### Linux Systemd
```bash
# Log'lar? izle
sudo journalctl -u kulturplatform -f

# Restart
sudo systemctl restart kulturplatform

# Status
sudo systemctl status kulturplatform
```

### Windows IIS
```powershell
# Event Viewer: Windows Logs -> Application
# IIS Log'lar: C:\inetpub\logs\LogFiles\

# Application Pool restart
Restart-WebAppPool -Name "KulturPlatformAppPool"
```

---

## ?? Yard?m

**Bu guide ile ilgili sorular?n?z için:**
- GitHub Issues: https://github.com/ademkarakas/KulturPlatform/issues
- Email: [sizin email]

---

**Haz?rlayan:** [?sminiz]  
**Tarih:** {tarih}  
**Versiyon:** 1.0
