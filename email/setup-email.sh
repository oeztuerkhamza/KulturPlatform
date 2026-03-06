#!/bin/bash
# ═══════════════════════════════════════════════════════════════════════════════
# E-Posta Sunucusu Kurulum Scripti - kulturplattformfreiburg.org
# ═══════════════════════════════════════════════════════════════════════════════
# Bu script sunucuda çalıştırılmalıdır.
# Kullanım: ./setup-email.sh
# ═══════════════════════════════════════════════════════════════════════════════

set -euo pipefail

DOMAIN="kulturplattformfreiburg.org"
MAIL_DOMAIN="mail.${DOMAIN}"
EMAIL_DIR="/opt/kulturplatform/email"
CONFIG_DIR="${EMAIL_DIR}/config"

RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
BLUE='\033[0;34m'
NC='\033[0m'

print_header() {
    echo -e "${BLUE}═══════════════════════════════════════════════════════════${NC}"
    echo -e "${BLUE}  $1${NC}"
    echo -e "${BLUE}═══════════════════════════════════════════════════════════${NC}"
}

print_success() { echo -e "${GREEN}✓ $1${NC}"; }
print_error()   { echo -e "${RED}✗ $1${NC}"; }
print_info()    { echo -e "${YELLOW}→ $1${NC}"; }

# ═══════════════════════════════════════════════════════════════════════════════
# 1. ADIM: Dizinleri oluştur
# ═══════════════════════════════════════════════════════════════════════════════
print_header "ADIM 1: Dizinler oluşturuluyor"

mkdir -p "${CONFIG_DIR}"
mkdir -p "${EMAIL_DIR}"

print_success "Dizinler oluşturuldu: ${EMAIL_DIR}"

# ═══════════════════════════════════════════════════════════════════════════════
# 2. ADIM: SSL Sertifikası al (mail subdomain için)
# ═══════════════════════════════════════════════════════════════════════════════
print_header "ADIM 2: SSL Sertifikası"

# Mevcut sertifika var mı kontrol et
if [ -d "/etc/letsencrypt/live/${MAIL_DOMAIN}" ]; then
    print_info "SSL sertifikası zaten mevcut: ${MAIL_DOMAIN}"
else
    print_info "SSL sertifikası alınıyor: ${MAIL_DOMAIN}"
    
    # Nginx'i geçici olarak durdur (port 80 serbest olmalı)
    docker stop kpf_nginx 2>/dev/null || true
    
    certbot certonly --standalone \
        -d "${MAIL_DOMAIN}" \
        --non-interactive \
        --agree-tos \
        --email "admin@${DOMAIN}" \
        --preferred-challenges http
    
    # Nginx'i tekrar başlat
    docker start kpf_nginx 2>/dev/null || true
    
    print_success "SSL sertifikası alındı: ${MAIL_DOMAIN}"
fi

# ═══════════════════════════════════════════════════════════════════════════════
# 3. ADIM: Docker Compose dosyasını kopyala
# ═══════════════════════════════════════════════════════════════════════════════
print_header "ADIM 3: Docker Compose dosyası"

# docker-compose.email.yml zaten email/ dizininde var
if [ -f "${EMAIL_DIR}/docker-compose.email.yml" ]; then
    print_success "docker-compose.email.yml mevcut"
else
    print_error "docker-compose.email.yml bulunamadı!"
    echo "Lütfen KulturPlatform/email/docker-compose.email.yml dosyasını ${EMAIL_DIR}/ dizinine kopyalayın."
    exit 1
fi

# ═══════════════════════════════════════════════════════════════════════════════
# 4. ADIM: Nginx yapılandırması (Roundcube webmail için)
# ═══════════════════════════════════════════════════════════════════════════════
print_header "ADIM 4: Nginx yapılandırması (webmail)"

WEBMAIL_CONF="/opt/kulturplatform/nginx/conf.d/webmail.conf"

cat > "${WEBMAIL_CONF}" << 'NGINX_CONF'
# ─── Webmail (Roundcube) ─────────────────────────────────────────────
server {
    listen 80;
    server_name mail.kulturplattformfreiburg.org;

    location /.well-known/acme-challenge/ {
        root /var/www/certbot;
    }

    location / {
        return 301 https://$server_name$request_uri;
    }
}

server {
    listen 443 ssl http2;
    server_name mail.kulturplattformfreiburg.org;

    ssl_certificate     /etc/letsencrypt/live/mail.kulturplattformfreiburg.org/fullchain.pem;
    ssl_certificate_key /etc/letsencrypt/live/mail.kulturplattformfreiburg.org/privkey.pem;
    ssl_protocols       TLSv1.2 TLSv1.3;
    ssl_ciphers         HIGH:!aNULL:!MD5;

    # Security headers
    add_header X-Frame-Options "SAMEORIGIN" always;
    add_header X-Content-Type-Options "nosniff" always;
    add_header X-XSS-Protection "1; mode=block" always;
    add_header Strict-Transport-Security "max-age=31536000; includeSubDomains" always;

    client_max_body_size 25M;

    location / {
        proxy_pass http://roundcube:8080;
        proxy_set_header Host $host;
        proxy_set_header X-Real-IP $remote_addr;
        proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header X-Forwarded-Proto $scheme;
    }
}
NGINX_CONF

print_success "Webmail nginx yapılandırması oluşturuldu"

# ═══════════════════════════════════════════════════════════════════════════════
# 5. ADIM: Mail server'ı başlat
# ═══════════════════════════════════════════════════════════════════════════════
print_header "ADIM 5: Mail server başlatılıyor"

cd "${EMAIL_DIR}"
docker compose -f docker-compose.email.yml up -d

print_info "Container'lar başlatılıyor, 30 saniye bekleniyor..."
sleep 30

# Container durumunu kontrol et
if docker ps | grep -q kpf_mailserver; then
    print_success "Mail server çalışıyor"
else
    print_error "Mail server başlatılamadı! Logları kontrol edin:"
    echo "  docker logs kpf_mailserver"
    exit 1
fi

if docker ps | grep -q kpf_roundcube; then
    print_success "Roundcube webmail çalışıyor"
else
    print_error "Roundcube başlatılamadı! Logları kontrol edin:"
    echo "  docker logs kpf_roundcube"
fi

# ═══════════════════════════════════════════════════════════════════════════════
# 6. ADIM: İlk e-posta hesabını oluştur
# ═══════════════════════════════════════════════════════════════════════════════
print_header "ADIM 6: İlk e-posta hesabı"

echo ""
read -p "info@${DOMAIN} hesabı için şifre girin: " -s INFO_PASSWORD
echo ""

if [ -n "${INFO_PASSWORD}" ]; then
    docker exec kpf_mailserver setup email add "info@${DOMAIN}" "${INFO_PASSWORD}"
    print_success "info@${DOMAIN} hesabı oluşturuldu"
else
    print_info "Şifre girilmedi, hesap oluşturulmadı."
    echo "Sonra eklemek için: ./mail-manage.sh add info@${DOMAIN} şifreniz"
fi

# ═══════════════════════════════════════════════════════════════════════════════
# 7. ADIM: DKIM anahtarı oluştur
# ═══════════════════════════════════════════════════════════════════════════════
print_header "ADIM 7: DKIM anahtarı"

docker exec kpf_mailserver setup config dkim keysize 2048
print_success "DKIM anahtarı oluşturuldu"

echo ""
print_info "DKIM DNS kaydı:"
docker exec kpf_mailserver cat /tmp/docker-mailserver/opendkim/keys/${DOMAIN}/mail.txt 2>/dev/null || \
    echo "(Anahtar oluşturuldu, sunucu yeniden başlattıktan sonra görüntülenebilir)"

# ═══════════════════════════════════════════════════════════════════════════════
# 8. ADIM: Nginx'i yeniden yükle
# ═══════════════════════════════════════════════════════════════════════════════
print_header "ADIM 8: Nginx yeniden yükleniyor"

docker exec kpf_nginx nginx -s reload 2>/dev/null && \
    print_success "Nginx yeniden yüklendi" || \
    print_info "Nginx yeniden yükleme başarısız, manuel yeniden başlatma gerekebilir"

# ═══════════════════════════════════════════════════════════════════════════════
# ÖZET
# ═══════════════════════════════════════════════════════════════════════════════
print_header "KURULUM TAMAMLANDI"

echo ""
echo -e "${GREEN}E-posta sunucusu başarıyla kuruldu!${NC}"
echo ""
echo "╔══════════════════════════════════════════════════════════════╗"
echo "║  Webmail:  https://mail.${DOMAIN}            ║"
echo "║  IMAP:     mail.${DOMAIN}:993 (SSL)          ║"
echo "║  SMTP:     mail.${DOMAIN}:587 (STARTTLS)     ║"
echo "╚══════════════════════════════════════════════════════════════╝"
echo ""
echo -e "${YELLOW}ÖNEMLİ: DNS kayıtlarını eklemeyi unutmayın!${NC}"
echo "Detaylar için: EMAIL_SETUP_GUIDE.md dosyasına bakın."
echo ""
echo "E-posta yönetimi için:"
echo "  ./mail-manage.sh add    kullanici@${DOMAIN} şifre"
echo "  ./mail-manage.sh remove kullanici@${DOMAIN}"
echo "  ./mail-manage.sh list"
echo ""
