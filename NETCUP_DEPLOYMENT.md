# KulturPlatform – Netcup Deployment Kılavuzu

Bu kılavuz, projeyi Netcup VPS üzerinde Docker ile yayınlamak için gereken tüm adımları kapsar.

---

## Mimari

```
┌─────────────────────────────────────────────────────┐
│  Netcup VPS (Ubuntu 22.04)                          │
│                                                     │
│  ┌─────────┐   ┌────────────┐   ┌───────────────┐  │
│  │  Nginx  │──▶│  .NET API  │──▶│  SQL Server   │  │
│  │  :80    │   │  :8080     │   │  :1433        │  │
│  │  :443   │   └────────────┘   └───────────────┘  │
│  └─────────┘                                        │
│       │                                             │
│  React (statik dosyalar – Nginx üzerinden servis)   │
└─────────────────────────────────────────────────────┘
```

| URL                                       | Açıklama       |
| ----------------------------------------- | -------------- |
| `https://kulturplattformfreiburg.org`     | React Frontend |
| `https://www.kulturplattformfreiburg.org` | Frontend (www) |
| `https://api.kulturplattformfreiburg.org` | .NET 10 API    |

---

## Gereksinimler

|      | Minimum          | Önerilen         |
| ---- | ---------------- | ---------------- |
| CPU  | 2 vCore          | 4 vCore          |
| RAM  | 4 GB             | 8 GB             |
| Disk | 40 GB SSD        | 80 GB SSD        |
| OS   | Ubuntu 22.04 LTS | Ubuntu 22.04 LTS |

> **Netcup VPS önerisi:** RS 2000 G10s (2 vCore, 4 GB RAM, 80 GB SSD)  
> Sipariş: https://www.netcup.de/vserver/vps.php

---

## Adım 1 – Netcup VPS Kurulumu

### 1.1 VPS Satın Al ve SSH Erişimi Sağla

Netcup müşteri panelinden (SCP) VPS siparişi ver.  
İşletim sistemi olarak **Ubuntu 22.04 LTS** seç.

```bash
# Yerel makineden sunucuya bağlan
ssh root@<SUNUCU_IP>
```

### 1.2 Temel Güvenlik Ayarları

```bash
# Sistem güncellemesi
apt update && apt upgrade -y

# Güvenlik duvarı
ufw allow OpenSSH
ufw allow 80/tcp
ufw allow 443/tcp
ufw enable

# SSH root girişini devre dışı bırak (isteğe bağlı ama önerilen)
adduser kpfadmin
usermod -aG sudo kpfadmin
# /etc/ssh/sshd_config: PermitRootLogin no
# systemctl restart sshd
```

---

## Adım 2 – DNS Ayarları

Netcup Domain panelinde veya başka bir DNS sağlayıcısında şu kayıtları ekle:

| Tür | Ad    | Değer         |
| --- | ----- | ------------- |
| A   | `@`   | `<SUNUCU_IP>` |
| A   | `www` | `<SUNUCU_IP>` |
| A   | `api` | `<SUNUCU_IP>` |

> DNS değişikliklerinin yayılması 1-48 saat sürebilir.

---

## Adım 3 – Proje Dosyalarını Sunucuya Yükle

### 3.1 Backend (KulturPlatform/)

```bash
# Sunucuda proje dizinini oluştur
mkdir -p /opt/kulturplatform
mkdir -p /opt/kulturplatform-frontend
mkdir -p /opt/kulturplatform/db-backup

# Yerel makineden sunucuya kopyala (Windows PowerShell'den)
scp -r "KulturPlatform/*" root@<SUNUCU_IP>:/opt/kulturplatform/

# Veya Git ile:
cd /opt/kulturplatform
git clone https://github.com/KULLANICI/KulturPlatform.git .
```

### 3.2 Frontend (KPFa-main/)

```bash
# Yerel makineden
scp -r "KPFa-main/*" root@<SUNUCU_IP>:/opt/kulturplatform-frontend/
```

### 3.3 Veritabanı Backup Dosyası

```bash
# KulturPlatformDb.bak dosyasını db-backup klasörüne kopyala
scp "KulturPlatformDb.bak" root@<SUNUCU_IP>:/opt/kulturplatform/db-backup/
```

---

## Adım 4 – Ortam Değişkenlerini Ayarla

```bash
cd /opt/kulturplatform

# .env dosyasını oluştur
cp .env.example .env
nano .env
```

`.env` dosyasını şu şekilde doldur:

```env
DOMAIN=kulturplattformfreiburg.org
MSSQL_SA_PASSWORD=GucluSifre@2026!        # En az 8 karakter, harf+rakam+özel
JWT_SECRET_KEY=buraya_en_az_32_karakter_rastgele_yaz  # openssl rand -base64 32
SMTP_HOST=smtp.ionos.de                   # ya da Netcup mail sunucunuz
SMTP_PORT=587
SMTP_FROM_EMAIL=noreply@kulturplattformfreiburg.org
SMTP_USERNAME=noreply@kulturplattformfreiburg.org
SMTP_PASSWORD=EmailSifreniz
```

> **JWT_SECRET_KEY üretmek için:**
>
> ```bash
> openssl rand -base64 32
> ```

---

## Adım 5 – Docker ile Deployment

### 5.1 Docker Kurulumu

```bash
curl -fsSL https://get.docker.com | sh
systemctl enable docker && systemctl start docker
apt-get install -y docker-compose-plugin
```

### 5.2 Frontend Build

```bash
# Node.js kur
curl -fsSL https://deb.nodesource.com/setup_22.x | bash -
apt-get install -y nodejs

cd /opt/kulturplatform-frontend

# Production build
VITE_API_URL="https://api.kulturplattformfreiburg.org/api" npm ci && npm run build
```

### 5.3 SSL Sertifikası Al (Let's Encrypt)

```bash
cd /opt/kulturplatform

# Geçici nginx config ile HTTP üzerinden challenge yap
docker compose up -d nginx certbot

# Sertifika al
docker compose run --rm certbot certonly \
  --webroot -w /var/www/certbot \
  --non-interactive --agree-tos \
  -m admin@kulturplattformfreiburg.org \
  -d kulturplattformfreiburg.org \
  -d www.kulturplattformfreiburg.org \
  -d api.kulturplattformfreiburg.org
```

### 5.4 SQL Server ve API'yi Başlat

```bash
cd /opt/kulturplatform
docker compose up -d
```

Container'ların durumunu kontrol et:

```bash
docker compose ps
docker compose logs -f api
```

### 5.5 Veritabanını Restore Et

```bash
cd /opt/kulturplatform
bash restore-db.sh
```

Restore tamamlandıktan sonra API'yi yeniden başlat:

```bash
docker compose restart api
```

### 5.6 Frontend Statik Dosyalarını Volume'a Kopyala

```bash
VOLUME_PATH=$(docker volume inspect kulturplatform_frontend_dist --format '{{.Mountpoint}}')
cp -r /opt/kulturplatform-frontend/dist/. "$VOLUME_PATH/"
docker compose restart nginx
```

---

## Adım 6 – Otomatik Deploy Script

Yukarıdaki adımların hepsini otomatik yapmak için:

```bash
cd /opt/kulturplatform
chmod +x deploy.sh restore-db.sh
sudo bash deploy.sh
```

---

## Adım 7 – Doğrulama

```bash
# Frontend
curl -I https://kulturplattformfreiburg.org

# API
curl https://api.kulturplattformfreiburg.org/api/home

# SSL sertifikası
openssl s_client -connect kulturplattformfreiburg.org:443 -brief
```

---

## Güncelleme (Sonraki Deploymentlar)

```bash
cd /opt/kulturplatform

# Kodu çek
git pull

# API'yi yeniden build et
docker compose up -d --build api

# Frontend'i yeniden build et
cd /opt/kulturplatform-frontend
git pull
VITE_API_URL="https://api.kulturplattformfreiburg.org/api" npm ci && npm run build
VOLUME_PATH=$(docker volume inspect kulturplatform_frontend_dist --format '{{.Mountpoint}}')
cp -r dist/. "$VOLUME_PATH/"
docker compose exec nginx nginx -s reload
```

---

## İzleme ve Bakım

```bash
# Log görüntüle
docker compose logs -f api
docker compose logs -f sqlserver
docker compose logs -f nginx

# Container'ları yeniden başlat
docker compose restart

# Disk kullanımı
df -h
docker system df

# Gereksiz Docker verilerini temizle
docker system prune -f
```

---

## SSL Otomatik Yenileme

Certbot container'ı otomatik olarak her 12 saatte bir sertifika yenilemeyi kontrol eder.  
Manuel yenileme için:

```bash
docker compose run --rm certbot renew
docker compose exec nginx nginx -s reload
```

---

## Olası Sorunlar

### API başlamıyor

```bash
docker compose logs api
# "migrations failed" hatası varsa:
docker compose restart api
```

### SQL Server bağlantı hatası

```bash
# Container çalışıyor mu?
docker compose ps sqlserver
# Şifreyi kontrol et
cat .env | grep MSSQL
```

### Nginx 502 Bad Gateway

```bash
# API container sağlıklı mı?
docker compose inspect kpf_api | grep Health
# API loglarını kontrol et
docker compose logs api --tail 50
```

### SSL sertifikası alınamıyor

- DNS kayıtlarının `<SUNUCU_IP>`'ye işaret ettiğini doğrula
- 80 portuna erişim engellenmiş olabilir: `ufw status`

---

## Önemli Dosyalar

| Dosya                                            | Açıklama                                                  |
| ------------------------------------------------ | --------------------------------------------------------- |
| `.env`                                           | Production ortam değişkenleri (gizli, Git'e commit etme!) |
| `docker-compose.yml`                             | Servis tanımları                                          |
| `Dockerfile`                                     | API container build                                       |
| `nginx/conf.d/kulturplatform.conf`               | Nginx site yapılandırması                                 |
| `restore-db.sh`                                  | Veritabanı restore scripti                                |
| `deploy.sh`                                      | Tam otomatik deployment                                   |
| `KulturPlatform.API/appsettings.Production.json` | Production API ayarları                                   |
