#!/bin/bash
# =============================================================================
# KulturPlatform - IP-Based Deployment Script (152.53.163.22)
# Kullanım: sudo bash deploy-ip.sh
# =============================================================================
set -e

RED='\033[0;31m'; GREEN='\033[0;32m'; YELLOW='\033[1;33m'; NC='\033[0m'
info()  { echo -e "${GREEN}[INFO]${NC}  $1"; }
warn()  { echo -e "${YELLOW}[WARN]${NC}  $1"; }
error() { echo -e "${RED}[ERROR]${NC} $1"; exit 1; }

SERVER_IP="152.53.163.22"
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

if ! docker compose version &>/dev/null 2>&1; then
    info "Docker Compose kuruluyor..."
    apt-get install -y -qq docker-compose-plugin
fi

# ─── 3. Proje dizini hazırla ──────────────────────────────────────────────────
info "Proje dizini hazırlanıyor: $PROJECT_DIR"
mkdir -p "$PROJECT_DIR/db-backup"
mkdir -p "$PROJECT_DIR/nginx/conf.d"

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

# ─── 5. IP-based Nginx config kullan ──────────────────────────────────────────
info "Nginx IP-based config yükleniyor..."
cp "$PROJECT_DIR/nginx/conf.d/kulturplatform.conf.ip" \
   "$PROJECT_DIR/nginx/conf.d/kulturplatform.conf"

# ─── 6. Frontend build ────────────────────────────────────────────────────────
FRONTEND_DIR="/opt/kulturplatform-frontend"
if [ -d "$FRONTEND_DIR" ]; then
    info "Frontend build ediliyor..."
    
    # Node.js kontrolü
    if ! command -v node &>/dev/null; then
        curl -fsSL https://deb.nodesource.com/setup_22.x | bash - &>/dev/null
        apt-get install -y -qq nodejs
    fi

    cd "$FRONTEND_DIR"
    npm ci --silent
    VITE_API_URL="http://$SERVER_IP/api" npm run build

    # Docker volume oluştur ve dosyaları kopyala
    docker volume create kulturplatform_frontend_dist &>/dev/null 2>&1 || true
    VOLUME_PATH=$(docker volume inspect kulturplatform_frontend_dist --format '{{.Mountpoint}}')
    cp -r dist/. "$VOLUME_PATH/"
    info "Frontend dosyaları kopyalandı: $VOLUME_PATH"
else
    warn "Frontend dizini ($FRONTEND_DIR) bulunamadı. Frontend'i manuel deploy edin."
fi

# ─── 7. Tüm servisleri başlat ────────────────────────────────────────────────
cd "$PROJECT_DIR"
info "Servisler başlatılıyor..."
docker compose pull sqlserver nginx 2>/dev/null || true
docker compose up -d --build

info "Container'lar başlatılıyor, 15 saniye bekleniyor..."
sleep 15
docker compose ps

# ─── 8. Veritabanı restore kontrolü ──────────────────────────────────────────
if [ -f "$PROJECT_DIR/db-backup/KulturPlatformDb.bak" ]; then
    info "Veritabanı backup dosyası bulundu. Restore etmek için:"
    info "  sudo bash restore-db.sh"
    info "  docker compose restart api"
fi

info ""
info "============================================================"
info "  Deployment tamamlandı!"
info "  Frontend : http://$SERVER_IP"
info "  API      : http://$SERVER_IP/api"
info "============================================================"
