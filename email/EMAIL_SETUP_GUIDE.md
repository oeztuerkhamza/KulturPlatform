# E-Posta Sunucusu Kurulum Rehberi

## kulturplattformfreiburg.org için Tam E-Posta Sistemi

Bu rehber, kendi domain'inizde e-posta gönderip almanızı, webmail ile Gmail gibi kullanmanızı ve istediğiniz kadar e-posta adresi eklemenizi sağlar.

---

## 📋 Genel Bakış

| Bileşen | Teknoloji | Açıklama |
|---------|-----------|----------|
| Mail Server | Docker-Mailserver (Postfix + Dovecot) | E-posta gönderme/alma |
| Webmail | Roundcube | Tarayıcıdan Gmail gibi kullanım |
| Anti-Spam | SpamAssassin | Spam filtreleme |
| Güvenlik | Fail2ban + DKIM + SPF + DMARC | E-posta güvenliği |
| SSL | Let's Encrypt | Şifreli bağlantı |

---

## 🔧 ADIM 1: DNS Kayıtları (Netcup DNS Paneli)

**Netcup CCP → Domain → kulturplattformfreiburg.org → DNS ayarları**

Aşağıdaki DNS kayıtlarını ekleyin:

### Zorunlu Kayıtlar

| Tür | Host | Değer | TTL |
|-----|------|-------|-----|
| **A** | `mail` | `152.53.163.22` | 3600 |
| **MX** | `@` | `10 mail.kulturplattformfreiburg.org.` | 3600 |
| **TXT** | `@` | `v=spf1 mx a ip4:152.53.163.22 ~all` | 3600 |
| **TXT** | `_dmarc` | `v=DMARC1; p=quarantine; rua=mailto:admin@kulturplattformfreiburg.org; pct=100` | 3600 |

### DKIM Kaydı (Kurulumdan sonra eklenecek)

DKIM kaydı kurulum esnasında otomatik oluşturulur. `setup-email.sh` çalıştırıldıktan sonra:

```bash
./mail-manage.sh dkim
```

Çıkan değeri şu şekilde DNS'e ekleyin:

| Tür | Host | Değer |
|-----|------|-------|
| **TXT** | `mail._domainkey` | `v=DKIM1; h=sha256; k=rsa; p=MIIBIjAN...` (script çıktısı) |

### Opsiyonel: Reverse DNS (PTR)

Netcup Server Control Panel → Server → Network → PTR kaydını `mail.kulturplattformfreiburg.org` olarak ayarlayın. Bu, e-postalarınızın spam olarak işaretlenmesini önler.

---

## 🚀 ADIM 2: Sunucuda Kurulum

### SSH ile sunucuya bağlanın:

```bash
ssh root@152.53.163.22
```

### Proje dosyalarını kopyalayın:

```bash
# Eğer git ile deploy ediyorsanız, email/ dizini zaten mevcut olacak
cd /opt/kulturplatform

# Yoksa dosyaları manuel kopyalayın
mkdir -p /opt/kulturplatform/email/config
# email/ dizinindeki dosyaları buraya kopyalayın
```

### Kurulum scriptini çalıştırın:

```bash
cd /opt/kulturplatform/email
chmod +x setup-email.sh mail-manage.sh
./setup-email.sh
```

Script otomatik olarak:
1. ✅ Gerekli dizinleri oluşturur
2. ✅ SSL sertifikası alır (mail.kulturplattformfreiburg.org)
3. ✅ Mail server container'larını başlatır
4. ✅ Roundcube webmail'i kurar
5. ✅ info@kulturplattformfreiburg.org hesabını oluşturur
6. ✅ DKIM anahtarını oluşturur
7. ✅ Nginx'i webmail için yapılandırır

---

## 📧 ADIM 3: E-Posta Hesaplarını Yönetme

### Yeni hesap ekle:

```bash
./mail-manage.sh add info@kulturplattformfreiburg.org "GuvenliSifre123!"
./mail-manage.sh add destek@kulturplattformfreiburg.org "BaskaSifre456!"
./mail-manage.sh add ahmet@kulturplattformfreiburg.org "SifreABC789!"
```

### Hesap sil:

```bash
./mail-manage.sh remove test@kulturplattformfreiburg.org
```

### Tüm hesapları listele:

```bash
./mail-manage.sh list
```

### Alias (yönlendirme) ekle:

```bash
# iletisim@... adresine gelen mailler info@... adresine yönlendirilir
./mail-manage.sh alias iletisim@kulturplattformfreiburg.org info@kulturplattformfreiburg.org
```

### Şifre değiştir:

```bash
./mail-manage.sh password info@kulturplattformfreiburg.org "YeniSifre123!"
```

---

## 🌐 ADIM 4: Webmail Kullanımı (Gmail Gibi)

### Tarayıcıdan:

1. **https://mail.kulturplattformfreiburg.org** adresine gidin
2. E-posta adresinizi ve şifrenizi girin
3. Gmail benzeri arayüzle e-postalarınızı yönetin!

### Roundcube Özellikleri:
- 📨 E-posta gönderme/alma
- 📎 Dosya ekleme (25MB'a kadar)
- 📁 Klasör yönetimi
- 🔍 E-posta arama
- ⚙️ Filtre oluşturma

---

## 📱 ADIM 5: Gmail'e Bağlama (İsteğe Bağlı)

Gmail üzerinden kulturplattformfreiburg.org e-postalarınızı okuyup gönderebilirsiniz:

### Gmail'de E-Posta Okuma (IMAP):

1. Gmail → Ayarlar (⚙️) → Tüm ayarları göster
2. **Hesaplar ve İçe Aktarma** sekmesi
3. **"Diğer hesaplardan e-posta al"** → Hesap ekle
4. Bilgileri girin:
   - E-posta: `info@kulturplattformfreiburg.org`
   - Kullanıcı adı: `info@kulturplattformfreiburg.org`
   - Şifre: (hesap şifresi)
   - POP/IMAP Sunucu: `mail.kulturplattformfreiburg.org`
   - Port: `993`
   - ✅ SSL kullan

### Gmail'den E-Posta Gönderme (SMTP):

1. Gmail → Ayarlar → Hesaplar ve İçe Aktarma
2. **"Şu adresten e-posta gönder"** → Başka adres ekle
3. Bilgileri girin:
   - Ad: Kulturplattform Freiburg
   - E-posta: `info@kulturplattformfreiburg.org`
   - SMTP Sunucu: `mail.kulturplattformfreiburg.org`
   - Port: `587`
   - Kullanıcı: `info@kulturplattformfreiburg.org`
   - Şifre: (hesap şifresi)
   - ✅ TLS ile güvenli bağlantı

---

## 📱 ADIM 6: Telefon / Outlook Bağlantısı

### iPhone Mail:
1. Ayarlar → Mail → Hesaplar → Hesap Ekle → Diğer
2. **Gelen Sunucu (IMAP):**
   - Sunucu: `mail.kulturplattformfreiburg.org`
   - Port: 993, SSL: Açık
3. **Giden Sunucu (SMTP):**
   - Sunucu: `mail.kulturplattformfreiburg.org`
   - Port: 587, TLS: Açık

### Android / Outlook:
- Aynı sunucu bilgilerini kullanın
- IMAP: port 993 (SSL)
- SMTP: port 587 (STARTTLS)

---

## 🔒 Güvenlik Kontrolleri

Kurulumdan sonra e-postalarınızın güvenliğini test edin:

### SPF Kontrolü:
```bash
dig TXT kulturplattformfreiburg.org
# "v=spf1 mx a ip4:152.53.163.22 ~all" görünmeli
```

### MX Kontrolü:
```bash
dig MX kulturplattformfreiburg.org
# mail.kulturplattformfreiburg.org. görünmeli
```

### E-posta Testi:
```bash
# Sunucudan test maili gönder
docker exec kpf_mailserver swaks --to test@gmail.com --from info@kulturplattformfreiburg.org --server localhost
```

### Online Test:
- https://www.mail-tester.com → Test e-postası gönderin, puanınızı görün
- https://mxtoolbox.com → DNS kayıtlarınızı kontrol edin

---

## 🔄 .NET API SMTP Ayarlarını Güncelleme

Artık Ionos SMTP yerine kendi mail server'ınızı kullanabilirsiniz. `.env` dosyasını güncelleyin:

```env
# Eski (Ionos):
# SMTP_HOST=smtp.ionos.de
# SMTP_FROM_EMAIL=noreply@kulturplattformfreiburg.org

# Yeni (Kendi sunucu):
SMTP_HOST=mail.kulturplattformfreiburg.org
SMTP_PORT=587
SMTP_FROM_EMAIL=noreply@kulturplattformfreiburg.org
SMTP_USERNAME=noreply@kulturplattformfreiburg.org
SMTP_PASSWORD=noreply_sifresi
```

Sonra API container'ını yeniden başlatın:
```bash
cd /opt/kulturplatform
docker compose up -d api
```

> **Not:** Bunun için `noreply@kulturplattformfreiburg.org` hesabını oluşturmanız gerekir:
> ```bash
> ./mail-manage.sh add noreply@kulturplattformfreiburg.org "NoReplySifre123!"
> ```

---

## ❓ Sorun Giderme

### Mail server başlamıyor:
```bash
docker logs kpf_mailserver
```

### E-posta gönderilemiyor:
```bash
# Port 25 açık mı?
telnet mail.kulturplattformfreiburg.org 25

# Firewall kontrol
ufw status
ufw allow 25/tcp
ufw allow 465/tcp
ufw allow 587/tcp
ufw allow 993/tcp
```

### Spam klasörüne düşüyor:
1. DNS kayıtlarını kontrol edin (SPF, DKIM, DMARC)
2. Reverse DNS (PTR) kaydını ayarlayın
3. https://www.mail-tester.com ile test edin

### Roundcube'a bağlanamıyorum:
```bash
docker logs kpf_roundcube
docker exec kpf_nginx nginx -t
```

---

## 🗂️ Dosya Yapısı

```
email/
├── docker-compose.email.yml   # Mail server + Roundcube container'ları
├── setup-email.sh             # Otomatik kurulum scripti
├── mail-manage.sh             # E-posta hesaplarını yönetme aracı
├── config/                    # Docker-mailserver yapılandırma dizini
│   ├── postfix-accounts.cf    # (otomatik oluşturulur) Hesaplar
│   └── postfix-virtual.cf    # (otomatik oluşturulur) Alias'lar
└── EMAIL_SETUP_GUIDE.md       # Bu dosya
```

---

## 📊 Port Kullanımı

| Port | Protokol | Kullanım | Firewall |
|------|----------|----------|----------|
| 25 | SMTP | Gelen e-posta | Açık olmalı |
| 465 | SMTPS | Güvenli gönderim | Açık olmalı |
| 587 | SMTP | STARTTLS gönderim | Açık olmalı |
| 993 | IMAPS | Mail client bağlantısı | Açık olmalı |
| 443 | HTTPS | Webmail (Roundcube) | Zaten açık |

---

## 🎯 Hızlı Başlangıç Özeti

```bash
# 1. DNS kayıtlarını ekle (Netcup panelinden)
# 2. Sunucuya bağlan
ssh root@152.53.163.22

# 3. Kurulumu çalıştır
cd /opt/kulturplatform/email
./setup-email.sh

# 4. E-posta hesapları ekle
./mail-manage.sh add info@kulturplattformfreiburg.org "Sifre123!"
./mail-manage.sh add destek@kulturplattformfreiburg.org "Sifre456!"

# 5. DKIM DNS kaydını ekle
./mail-manage.sh dkim

# 6. Webmail'i kullan
# https://mail.kulturplattformfreiburg.org
```
