#!/bin/bash
# ═══════════════════════════════════════════════════════════════════════════════
# E-Posta Yönetim Scripti - kulturplattformfreiburg.org
# ═══════════════════════════════════════════════════════════════════════════════
# Kullanım:
#   ./mail-manage.sh add    kullanici@kulturplattformfreiburg.org sifre123
#   ./mail-manage.sh remove kullanici@kulturplattformfreiburg.org
#   ./mail-manage.sh list
#   ./mail-manage.sh alias  alias@kulturplattformfreiburg.org hedef@kulturplattformfreiburg.org
#   ./mail-manage.sh password kullanici@kulturplattformfreiburg.org yenisifre
#   ./mail-manage.sh dkim
# ═══════════════════════════════════════════════════════════════════════════════

set -euo pipefail

CONTAINER="kpf_mailserver"
DOMAIN="kulturplattformfreiburg.org"

# Renk tanımları
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
BLUE='\033[0;34m'
NC='\033[0m' # Renk sıfırlama

print_header() {
    echo -e "${BLUE}═══════════════════════════════════════════════════════════${NC}"
    echo -e "${BLUE}  E-Posta Yönetimi - ${DOMAIN}${NC}"
    echo -e "${BLUE}═══════════════════════════════════════════════════════════${NC}"
}

print_success() { echo -e "${GREEN}✓ $1${NC}"; }
print_error()   { echo -e "${RED}✗ $1${NC}"; }
print_info()    { echo -e "${YELLOW}→ $1${NC}"; }

# Container'ın çalışıp çalışmadığını kontrol et
check_container() {
    if ! docker ps --format '{{.Names}}' | grep -q "^${CONTAINER}$"; then
        print_error "Mail server container (${CONTAINER}) çalışmıyor!"
        echo "Başlatmak için: cd /opt/kulturplatform/email && docker compose -f docker-compose.email.yml up -d"
        exit 1
    fi
}

# ─── E-posta hesabı ekle ────────────────────────────────────────────
add_account() {
    local email="$1"
    local password="$2"

    if [ -z "$email" ] || [ -z "$password" ]; then
        print_error "Kullanım: $0 add email@${DOMAIN} şifre"
        exit 1
    fi

    # E-posta formatını doğrula
    if [[ ! "$email" =~ ^[a-zA-Z0-9._%+-]+@${DOMAIN}$ ]]; then
        print_error "Geçersiz e-posta adresi. @${DOMAIN} ile bitmeli."
        exit 1
    fi

    print_info "Hesap ekleniyor: ${email}"
    docker exec "${CONTAINER}" setup email add "${email}" "${password}"
    print_success "Hesap başarıyla eklendi: ${email}"
    echo ""
    echo "  IMAP Sunucu: mail.${DOMAIN}"
    echo "  IMAP Port:   993 (SSL/TLS)"
    echo "  SMTP Sunucu: mail.${DOMAIN}"
    echo "  SMTP Port:   587 (STARTTLS)"
    echo "  Kullanıcı:   ${email}"
}

# ─── E-posta hesabı sil ─────────────────────────────────────────────
remove_account() {
    local email="$1"

    if [ -z "$email" ]; then
        print_error "Kullanım: $0 remove email@${DOMAIN}"
        exit 1
    fi

    print_info "Hesap siliniyor: ${email}"
    read -p "Emin misiniz? (e/h): " confirm
    if [ "$confirm" = "e" ] || [ "$confirm" = "E" ]; then
        docker exec "${CONTAINER}" setup email del "${email}"
        print_success "Hesap silindi: ${email}"
    else
        print_info "İptal edildi."
    fi
}

# ─── Hesapları listele ───────────────────────────────────────────────
list_accounts() {
    print_info "Mevcut e-posta hesapları:"
    echo ""
    docker exec "${CONTAINER}" setup email list
    echo ""
    print_info "Mevcut alias'lar:"
    docker exec "${CONTAINER}" setup alias list
}

# ─── Alias ekle ──────────────────────────────────────────────────────
add_alias() {
    local alias_addr="$1"
    local target="$2"

    if [ -z "$alias_addr" ] || [ -z "$target" ]; then
        print_error "Kullanım: $0 alias alias@${DOMAIN} hedef@${DOMAIN}"
        exit 1
    fi

    print_info "Alias ekleniyor: ${alias_addr} → ${target}"
    docker exec "${CONTAINER}" setup alias add "${alias_addr}" "${target}"
    print_success "Alias eklendi: ${alias_addr} → ${target}"
}

# ─── Şifre değiştir ──────────────────────────────────────────────────
change_password() {
    local email="$1"
    local new_password="$2"

    if [ -z "$email" ] || [ -z "$new_password" ]; then
        print_error "Kullanım: $0 password email@${DOMAIN} yenisifre"
        exit 1
    fi

    print_info "Şifre değiştiriliyor: ${email}"
    docker exec "${CONTAINER}" setup email update "${email}" "${new_password}"
    print_success "Şifre başarıyla değiştirildi: ${email}"
}

# ─── DKIM anahtarı oluştur ───────────────────────────────────────────
setup_dkim() {
    print_info "DKIM anahtarı oluşturuluyor..."
    docker exec "${CONTAINER}" setup config dkim keysize 2048
    echo ""
    print_success "DKIM anahtarı oluşturuldu!"
    echo ""
    print_info "Aşağıdaki DNS TXT kaydını ekleyin:"
    echo ""
    docker exec "${CONTAINER}" cat /tmp/docker-mailserver/opendkim/keys/${DOMAIN}/mail.txt 2>/dev/null || \
        echo "(DKIM kaydı /tmp/docker-mailserver/opendkim/keys/ altında oluşturuldu)"
    echo ""
    print_info "DNS kaydı eklendikten sonra mail server'ı yeniden başlatın."
}

# ─── Logları göster ──────────────────────────────────────────────────
show_logs() {
    docker logs "${CONTAINER}" --tail 50
}

# ─── Yardım menüsü ──────────────────────────────────────────────────
show_help() {
    print_header
    echo ""
    echo "Kullanım: $0 <komut> [parametreler]"
    echo ""
    echo "Komutlar:"
    echo "  add <email> <şifre>      Yeni e-posta hesabı ekle"
    echo "  remove <email>           E-posta hesabını sil"
    echo "  list                     Tüm hesapları listele"
    echo "  alias <alias> <hedef>    E-posta yönlendirmesi ekle"
    echo "  password <email> <şifre> Şifre değiştir"
    echo "  dkim                     DKIM anahtarı oluştur"
    echo "  logs                     Son logları göster"
    echo "  help                     Bu yardım mesajını göster"
    echo ""
    echo "Örnekler:"
    echo "  $0 add info@${DOMAIN} GucluSifre123!"
    echo "  $0 add destek@${DOMAIN} BaskaSifre456!"
    echo "  $0 remove test@${DOMAIN}"
    echo "  $0 list"
    echo "  $0 alias iletisim@${DOMAIN} info@${DOMAIN}"
    echo "  $0 password info@${DOMAIN} YeniSifre789!"
    echo "  $0 dkim"
    echo ""
}

# ─── Ana program ─────────────────────────────────────────────────────
check_container

case "${1:-help}" in
    add)      add_account "${2:-}" "${3:-}" ;;
    remove)   remove_account "${2:-}" ;;
    list)     list_accounts ;;
    alias)    add_alias "${2:-}" "${3:-}" ;;
    password) change_password "${2:-}" "${3:-}" ;;
    dkim)     setup_dkim ;;
    logs)     show_logs ;;
    help|*)   show_help ;;
esac
