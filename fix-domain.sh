#!/bin/bash
# =============================================================================
# KulturPlatform - Domain + SSL Fix Script
# IP-based deploy'dan domain-based'e geçiş
# Kullanım: sudo bash fix-domain.sh
# =============================================================================
set -e

RED='\033[0;31m'; GREEN='\033[0;32m'; YELLOW='\033[1;33m'; NC='\033[0m'
info()  { echo -e "${GREEN}[INFO]${NC}  $1"; }
warn()  { echo -e "${YELLOW}[WARN]${NC}  $1"; }
error() { echo -e "${RED}[ERROR]${NC} $1"; exit 1; }

DOMAIN="kulturplattformfreiburg.org"
SERVER_IP="152.53.163.22"
PROJECT_DIR="/opt/kulturplatform"
FRONTEND_DIR="/opt/kulturplatform-frontend"

# ─── 0. Ön kontroller ────────────────────────────────────────────────────────
[ "$(id -u)" -ne 0 ] && error "Bu script root olarak çalıştırılmalı! (sudo bash fix-domain.sh)"
[ ! -f "$PROJECT_DIR/.env" ] && error ".env dosyası bulunamadı: $PROJECT_DIR/.env"
[ ! -f "$PROJECT_DIR/docker-compose.yml" ] && error "docker-compose.yml bulunamadı!"

source "$PROJECT_DIR/.env"

# ─── 1. DNS kontrolü ─────────────────────────────────────────────────────────
info "DNS kontrolü yapılıyor..."
RESOLVED_IP=$(dig +short "$DOMAIN" 2>/dev/null | head -1 || true)
if [ -z "$RESOLVED_IP" ]; then
    RESOLVED_IP=$(host "$DOMAIN" 2>/dev/null | awk '/has address/ {print $4}' | head -1 || true)
fi
if [ -z "$RESOLVED_IP" ]; then
    RESOLVED_IP=$(nslookup "$DOMAIN" 2>/dev/null | awk '/^Address: / {print $2}' | head -1 || true)
fi

if [ -n "$RESOLVED_IP" ]; then
    info "DNS çözümleme: $DOMAIN -> $RESOLVED_IP"
    if [ "$RESOLVED_IP" != "$SERVER_IP" ]; then
        warn "DNS IP ($RESOLVED_IP) sunucu IP'si ($SERVER_IP) ile eşleşmiyor!"
        warn "DNS ayarlarınızı kontrol edin. Devam ediliyor..."
    fi
else
    warn "DNS çözümlenemedi. DNS kayıtlarınızı kontrol edin."
    warn "A kaydı: $DOMAIN -> $SERVER_IP"
    warn "CNAME kaydı: www.$DOMAIN -> $DOMAIN"
fi

# ─── 2. Geçici HTTP-only nginx config (certbot challenge için) ───────────────
info "Geçici HTTP nginx config oluşturuluyor (SSL sertifikası almak için)..."

# Mevcut config'i yedekle
cp "$PROJECT_DIR/nginx/conf.d/kulturplatform.conf" \
   "$PROJECT_DIR/nginx/conf.d/kulturplatform.conf.backup" 2>/dev/null || true

cat > "$PROJECT_DIR/nginx/conf.d/kulturplatform.conf" << 'TMPCONF'
# Geçici config - SSL sertifikası almak için
server {
    listen 80;
    server_name kulturplattformfreiburg.org www.kulturplattformfreiburg.org
                api.kulturplattformfreiburg.org 152.53.163.22;

    # Let's Encrypt challenge
    location /.well-known/acme-challenge/ {
        root /var/www/certbot;
    }

    root /usr/share/nginx/html;
    index index.html;

    # Uploads proxy
    location ^~ /uploads/ {
        proxy_pass         http://api:8080/uploads/;
        proxy_http_version 1.1;
        proxy_set_header   Host $host;
        proxy_set_header   X-Real-IP $remote_addr;
        proxy_set_header   X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header   X-Forwarded-Proto $scheme;
    }

    # API proxy
    location /api/ {
        proxy_pass         http://api:8080/api/;
        proxy_http_version 1.1;
        proxy_set_header   Host $host;
        proxy_set_header   X-Real-IP $remote_addr;
        proxy_set_header   X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header   X-Forwarded-Proto $scheme;
        client_max_body_size 20M;
    }

    # Health check proxy
    location /health {
        proxy_pass         http://api:8080/health;
        proxy_http_version 1.1;
        proxy_set_header   Host $host;
    }

    # SPA fallback
    location / {
        try_files $uri $uri/ /index.html;
    }
}
TMPCONF

# Nginx'i yeniden başlat
cd "$PROJECT_DIR"
docker compose restart nginx 2>/dev/null || docker compose up -d nginx
sleep 3

# ─── 3. SSL Sertifikası al (Let's Encrypt) ──────────────────────────────────
info "SSL sertifikası alınıyor..."

# certbot_webroot volume'unu oluştur
docker volume create kulturplatform_certbot_webroot 2>/dev/null || true
docker volume create kulturplatform_certbot_certs 2>/dev/null || true

# Certbot ile sertifika al
docker compose run --rm certbot certonly \
    --webroot -w /var/www/certbot \
    --non-interactive --agree-tos \
    --email "info@${DOMAIN}" \
    -d "$DOMAIN" -d "www.$DOMAIN" -d "api.$DOMAIN" \
    --force-renewal 2>&1 || {
        warn "Certbot başarısız oldu. Standalone modda deneniyor..."
        # Nginx'i durdur ve standalone dene
        docker compose stop nginx
        sleep 2
        docker run --rm -p 80:80 \
            -v kulturplatform_certbot_certs:/etc/letsencrypt \
            certbot/certbot certonly \
            --standalone --non-interactive --agree-tos \
            --email "info@${DOMAIN}" \
            -d "$DOMAIN" -d "www.$DOMAIN" -d "api.$DOMAIN" 2>&1 || {
                error "SSL sertifikası alınamadı! DNS ayarlarınızı kontrol edin."
            }
    }

info "SSL sertifikası başarıyla alındı!"

# ─── 4. Production nginx config'ini aktif et ─────────────────────────────────
info "Production nginx config aktif ediliyor..."

cat > "$PROJECT_DIR/nginx/conf.d/kulturplatform.conf" << 'PRODCONF'
# ─── HTTP → HTTPS Redirect ───────────────────────────────────────────────────
server {
    listen 80;
    server_name kulturplattformfreiburg.org www.kulturplattformfreiburg.org
                api.kulturplattformfreiburg.org;

    # Let's Encrypt challenge
    location /.well-known/acme-challenge/ {
        root /var/www/certbot;
    }

    location / {
        return 301 https://$host$request_uri;
    }
}

# ─── IP üzerinden erişim (HTTP) ──────────────────────────────────────────────
server {
    listen 80;
    server_name 152.53.163.22;

    root /usr/share/nginx/html;
    index index.html;

    # Security headers
    add_header X-Frame-Options "SAMEORIGIN" always;
    add_header X-Content-Type-Options "nosniff" always;
    add_header Referrer-Policy "strict-origin-when-cross-origin" always;

    # Static assets – long cache
    location ~* \.(js|css|png|jpg|jpeg|gif|ico|svg|woff|woff2|ttf|eot)$ {
        expires 1y;
        add_header Cache-Control "public, immutable";
        try_files $uri =404;
    }

    # Uploads proxy
    location ^~ /uploads/ {
        proxy_pass         http://api:8080/uploads/;
        proxy_http_version 1.1;
        proxy_set_header   Host $host;
        proxy_set_header   X-Real-IP $remote_addr;
        proxy_set_header   X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header   X-Forwarded-Proto $scheme;
        expires 1y;
        add_header Cache-Control "public, immutable";
    }

    # API proxy
    location /api/ {
        proxy_pass         http://api:8080/api/;
        proxy_http_version 1.1;
        proxy_set_header   Upgrade $http_upgrade;
        proxy_set_header   Connection keep-alive;
        proxy_set_header   Host $host;
        proxy_set_header   X-Real-IP $remote_addr;
        proxy_set_header   X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header   X-Forwarded-Proto $scheme;
        proxy_cache_bypass $http_upgrade;
        client_max_body_size 20M;
        proxy_read_timeout 300;
        proxy_connect_timeout 300;
    }

    # Health check proxy
    location /health {
        proxy_pass         http://api:8080/health;
        proxy_http_version 1.1;
        proxy_set_header   Host $host;
    }

    # SPA fallback
    location / {
        try_files $uri $uri/ /index.html;
    }
}

# ─── Frontend (kulturplattformfreiburg.org) ───────────────────────────────────
server {
    listen 443 ssl;
    http2 on;
    server_name kulturplattformfreiburg.org www.kulturplattformfreiburg.org;

    ssl_certificate     /etc/letsencrypt/live/kulturplattformfreiburg.org/fullchain.pem;
    ssl_certificate_key /etc/letsencrypt/live/kulturplattformfreiburg.org/privkey.pem;
    ssl_protocols TLSv1.2 TLSv1.3;
    ssl_ciphers HIGH:!aNULL:!MD5;
    ssl_prefer_server_ciphers on;

    root /usr/share/nginx/html;
    index index.html;

    # Security headers
    add_header X-Frame-Options "SAMEORIGIN" always;
    add_header X-Content-Type-Options "nosniff" always;
    add_header Referrer-Policy "strict-origin-when-cross-origin" always;

    # Static assets – long cache
    location ~* \.(js|css|png|jpg|jpeg|gif|ico|svg|woff|woff2|ttf|eot)$ {
        expires 1y;
        add_header Cache-Control "public, immutable";
        try_files $uri =404;
    }

    # Uploads proxy
    location ^~ /uploads/ {
        proxy_pass         http://api:8080/uploads/;
        proxy_http_version 1.1;
        proxy_set_header   Host $host;
        proxy_set_header   X-Real-IP $remote_addr;
        proxy_set_header   X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header   X-Forwarded-Proto $scheme;
        expires 1y;
        add_header Cache-Control "public, immutable";
    }

    # SPA fallback
    location / {
        try_files $uri $uri/ /index.html;
    }
}

# ─── API (api.kulturplattformfreiburg.org) ────────────────────────────────────
server {
    listen 443 ssl;
    http2 on;
    server_name api.kulturplattformfreiburg.org;

    ssl_certificate     /etc/letsencrypt/live/kulturplattformfreiburg.org/fullchain.pem;
    ssl_certificate_key /etc/letsencrypt/live/kulturplattformfreiburg.org/privkey.pem;
    ssl_protocols TLSv1.2 TLSv1.3;
    ssl_ciphers HIGH:!aNULL:!MD5;
    ssl_prefer_server_ciphers on;

    # Security headers
    add_header X-Frame-Options "DENY" always;
    add_header X-Content-Type-Options "nosniff" always;

    # Increase buffer for large file uploads
    client_max_body_size 20M;
    proxy_read_timeout 300;
    proxy_connect_timeout 300;

    location / {
        proxy_pass         http://api:8080;
        proxy_http_version 1.1;
        proxy_set_header   Upgrade $http_upgrade;
        proxy_set_header   Connection keep-alive;
        proxy_set_header   Host $host;
        proxy_set_header   X-Real-IP $remote_addr;
        proxy_set_header   X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header   X-Forwarded-Proto $scheme;
        proxy_cache_bypass $http_upgrade;
    }
}
PRODCONF

# ─── 5. Frontend'i domain URL ile yeniden build et ───────────────────────────
if [ -d "$FRONTEND_DIR" ]; then
    info "Frontend domain URL ile yeniden build ediliyor..."
    cd "$FRONTEND_DIR"

    # Node.js kontrolü
    if ! command -v node &>/dev/null; then
        curl -fsSL https://deb.nodesource.com/setup_22.x | bash - &>/dev/null
        apt-get install -y -qq nodejs
    fi

    npm ci --silent 2>/dev/null || npm install --silent
    VITE_API_URL="https://api.$DOMAIN/api" npm run build

    # Docker volume'a kopyala
    VOLUME_PATH=$(docker volume inspect kulturplatform_frontend_dist --format '{{.Mountpoint}}' 2>/dev/null || true)
    if [ -z "$VOLUME_PATH" ]; then
        docker volume create kulturplatform_frontend_dist &>/dev/null || true
        VOLUME_PATH=$(docker volume inspect kulturplatform_frontend_dist --format '{{.Mountpoint}}')
    fi
    rm -rf "$VOLUME_PATH"/*
    cp -r dist/. "$VOLUME_PATH/"
    info "Frontend dosyaları kopyalandı."
else
    warn "Frontend dizini ($FRONTEND_DIR) bulunamadı!"
    warn "Frontend'i manuel olarak build edip kopyalamanız gerekebilir."
    warn "  VITE_API_URL=https://api.$DOMAIN/api npm run build"
fi

# ─── 6. Backend CORS ayarlarını güncelle ─────────────────────────────────────
info "Backend ortam değişkenleri güncelleniyor..."
cd "$PROJECT_DIR"

# docker-compose.yml'deki environment değişkenlerini kontrol et
# (zaten doğru ayarlanmış olmalı, ama IP-based değerleri düzelt)
if grep -q "152.53.163.22" docker-compose.yml 2>/dev/null; then
    warn "docker-compose.yml'de IP referansları bulundu, güncelleniyor..."
    sed -i "s|http://152.53.163.22|https://$DOMAIN|g" docker-compose.yml
fi

# ─── 7. Tüm servisleri yeniden başlat ────────────────────────────────────────
info "Tüm servisler yeniden başlatılıyor..."
cd "$PROJECT_DIR"
docker compose down
docker compose up -d --build
sleep 15

# ─── 8. Kontrol ──────────────────────────────────────────────────────────────
info "Container durumları:"
docker compose ps

# HTTP -> HTTPS yönlendirme testi
HTTP_STATUS=$(curl -s -o /dev/null -w "%{http_code}" "http://$DOMAIN" 2>/dev/null || echo "000")
info "HTTP yanıt kodu (http://$DOMAIN): $HTTP_STATUS"

HTTPS_STATUS=$(curl -s -o /dev/null -w "%{http_code}" "https://$DOMAIN" 2>/dev/null || echo "000")
info "HTTPS yanıt kodu (https://$DOMAIN): $HTTPS_STATUS"

API_STATUS=$(curl -s -o /dev/null -w "%{http_code}" "https://api.$DOMAIN/health" 2>/dev/null || echo "000")
info "API sağlık kontrolü (https://api.$DOMAIN/health): $API_STATUS"

IP_STATUS=$(curl -s -o /dev/null -w "%{http_code}" "http://$SERVER_IP" 2>/dev/null || echo "000")
info "IP erişim kontrolü (http://$SERVER_IP): $IP_STATUS"

echo ""
info "============================================================"
info "  Domain Fix tamamlandı!"
info ""
info "  Frontend : https://$DOMAIN"
info "  API      : https://api.$DOMAIN"
info "  IP       : http://$SERVER_IP (yedek erişim)"
info ""
info "  SSL sertifikası otomatik yenileme: certbot container"
info "============================================================"
