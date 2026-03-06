#!/bin/bash
# =============================================================================
# KulturPlatform - Netcup Deployment Script
# Kullanım: sudo bash deploy.sh
# =============================================================================
set -e

RED='\033[0;31m'; GREEN='\033[0;32m'; YELLOW='\033[1;33m'; NC='\033[0m'
info()  { echo -e "${GREEN}[INFO]${NC}  $1"; }
warn()  { echo -e "${YELLOW}[WARN]${NC}  $1"; }
error() { echo -e "${RED}[ERROR]${NC} $1"; exit 1; }

DOMAIN="${DOMAIN:-kulturplattformfreiburg.org}"
PROJECT_DIR="/opt/kulturplatform"

# ─── 1. Sistem paketleri ──────────────────────────────────────────────────────
info "Sistem güncelleniyor..."
apt-get update -qq
apt-get install -y -qq curl git unzip

# ─── 2. Docker kurulumu ───────────────────────────────────────────────────────
if ! command -v docker &>/dev/null; then
    info "Docker kuruluyor..."
    curl -fsSL https://get.docker.com | sh
    systemctl enable docker
    systemctl start docker
else
    info "Docker zaten kurulu: $(docker --version)"
fi

if ! command -v docker-compose &>/dev/null && ! docker compose version &>/dev/null 2>&1; then
    info "Docker Compose kuruluyor..."
    apt-get install -y -qq docker-compose-plugin
fi

# ─── 3. Proje dizini hazırla ──────────────────────────────────────────────────
info "Proje dizini hazırlanıyor: $PROJECT_DIR"
mkdir -p "$PROJECT_DIR/db-backup"
mkdir -p "$PROJECT_DIR/nginx/conf.d"
mkdir -p "$PROJECT_DIR/nginx/ssl"

# ─── 4. .env kontrolü ────────────────────────────────────────────────────────
if [ ! -f "$PROJECT_DIR/.env" ]; then
    if [ -f "$PROJECT_DIR/.env.example" ]; then
        cp "$PROJECT_DIR/.env.example" "$PROJECT_DIR/.env"
        warn ".env dosyası .env.example'dan kopyalandı."
        warn "Lütfen $PROJECT_DIR/.env dosyasını düzenleyin ve tekrar çalıştırın!"
        exit 1
    else
        error ".env dosyası bulunamadı! Önce .env.example'ı doldurun."
    fi
fi

source "$PROJECT_DIR/.env"
[ -z "$MSSQL_SA_PASSWORD" ] && error "MSSQL_SA_PASSWORD ayarlanmamış!"
[ -z "$JWT_SECRET_KEY" ]    && error "JWT_SECRET_KEY ayarlanmamış!"

# ─── 5. Frontend build ────────────────────────────────────────────────────────
FRONTEND_DIR="/opt/kulturplatform-frontend"
if [ -d "$FRONTEND_DIR" ]; then
    info "Frontend build ediliyor..."
    cd "$FRONTEND_DIR"
    curl -fsSL https://deb.nodesource.com/setup_22.x | bash - &>/dev/null
    apt-get install -y -qq nodejs
    npm ci --silent
    VITE_API_URL="https://api.$DOMAIN/api" npm run build

    # Statik dosyaları Docker volume'a kopyala
    VOLUME_PATH=$(docker volume inspect kulturplatform_frontend_dist --format '{{.Mountpoint}}' 2>/dev/null || true)
    if [ -z "$VOLUME_PATH" ]; then
        docker volume create kulturplatform_frontend_dist &>/dev/null || true
        VOLUME_PATH=$(docker volume inspect kulturplatform_frontend_dist --format '{{.Mountpoint}}')
    fi
    cp -r dist/. "$VOLUME_PATH/"
    info "Frontend dosyaları kopyalandı: $VOLUME_PATH"
else
    warn "Frontend dizini ($FRONTEND_DIR) bulunamadı. Frontend'i manuel deploy edin."
fi

# ─── 6. SSL - Let's Encrypt (ilk kurulum) ─────────────────────────────────────
cd "$PROJECT_DIR"
if [ ! -d "/etc/letsencrypt/live/$DOMAIN" ] && ! docker volume inspect kulturplatform_certbot_certs &>/dev/null; then
    info "SSL sertifikası ilk kez alınıyor..."

    # Önce nginx'i HTTP modunda başlat (certbot challenge için)
    cat > "$PROJECT_DIR/nginx/conf.d/kulturplatform.conf" << TMPCONF
server {
    listen 80;
    server_name $DOMAIN www.$DOMAIN api.$DOMAIN;
    location /.well-known/acme-challenge/ { root /var/www/certbot; }
    location / { return 200 'ok'; add_header Content-Type text/plain; }
}
TMPCONF

    docker compose up -d nginx certbot --no-recreate 2>/dev/null || docker compose up -d nginx certbot

    sleep 5

    docker compose run --rm certbot certonly \
        --webroot -w /var/www/certbot \
        --non-interactive --agree-tos \
        -m "admin@$DOMAIN" \
        -d "$DOMAIN" -d "www.$DOMAIN" -d "api.$DOMAIN"

    info "SSL sertifikası alındı!"
fi

# ─── 7. Nginx yapılandırmasını geri yükle ────────────────────────────────────
cp "$PROJECT_DIR/nginx/conf.d/kulturplatform.conf.prod" \
   "$PROJECT_DIR/nginx/conf.d/kulturplatform.conf" 2>/dev/null || true

# ─── 8. Tüm servisleri başlat ────────────────────────────────────────────────
info "Servisler başlatılıyor..."
docker compose pull sqlserver nginx certbot 2>/dev/null || true
docker compose up -d --build

info "Kontrol ediliyor..."
sleep 15
docker compose ps

info ""
info "============================================================"
info "  Deployment tamamlandı!"
info "  Frontend : https://$DOMAIN"
info "  API      : https://api.$DOMAIN"
info "============================================================"
