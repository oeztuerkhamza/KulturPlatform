#!/bin/bash
# =============================================================================
# KulturPlatformDb.bak - SQL Server Restore Script
# Kullanım: sudo bash restore-db.sh
#
# Önce: KulturPlatformDb.bak dosyasını db-backup/ klasörüne kopyalayın
# =============================================================================
set -e

RED='\033[0;31m'; GREEN='\033[0;32m'; YELLOW='\033[1;33m'; NC='\033[0m'
info()  { echo -e "${GREEN}[INFO]${NC}  $1"; }
error() { echo -e "${RED}[ERROR]${NC} $1"; exit 1; }

[ ! -f ".env" ] && error ".env dosyası bulunamadı! Önce: cp .env.example .env"
source .env

BAK_FILE="./db-backup/KulturPlatformDb.bak"
[ ! -f "$BAK_FILE" ] && error "Backup dosyası bulunamadı: $BAK_FILE"

info "SQL Server container'ın hazır olması bekleniyor..."
MAX_WAIT=120
WAITED=0
until docker compose exec -T sqlserver \
    /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "$MSSQL_SA_PASSWORD" \
    -Q "SELECT 1" -C -No &>/dev/null; do
    sleep 5
    WAITED=$((WAITED+5))
    [ $WAITED -ge $MAX_WAIT ] && error "SQL Server $MAX_WAIT saniye içinde hazır olmadı!"
    info "Bekleniyor... ($WAITED/$MAX_WAIT sn)"
done

info "Backup dosyası logical adları sorgulanıyor..."
LOGICAL=$(docker compose exec -T sqlserver \
    /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "$MSSQL_SA_PASSWORD" \
    -Q "RESTORE FILELISTONLY FROM DISK='/var/opt/mssql/backup/KulturPlatformDb.bak'" \
    -C -No -h -1 2>/dev/null | awk 'NR==1{data=$1} NR==2{log=$1} END{print data" "log}')

DATA_NAME=$(echo $LOGICAL | cut -d' ' -f1)
LOG_NAME=$(echo  $LOGICAL | cut -d' ' -f2)

info "Logical names: Data=$DATA_NAME, Log=$LOG_NAME"

info "Veritabanı restore ediliyor (bu birkaç dakika sürebilir)..."
docker compose exec -T sqlserver \
    /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "$MSSQL_SA_PASSWORD" -C -No \
    -Q "
RESTORE DATABASE [KulturPlatformDb]
FROM DISK = '/var/opt/mssql/backup/KulturPlatformDb.bak'
WITH MOVE '$DATA_NAME' TO '/var/opt/mssql/data/KulturPlatformDb.mdf',
     MOVE '$LOG_NAME'  TO '/var/opt/mssql/data/KulturPlatformDb_log.ldf',
     REPLACE,
     STATS = 10;
"

info "Restore tamamlandı! Veritabanı kontrol ediliyor..."
docker compose exec -T sqlserver \
    /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "$MSSQL_SA_PASSWORD" -C -No \
    -Q "SELECT name, state_desc FROM sys.databases WHERE name='KulturPlatformDb';"

info ""
info "======================================================"
info "  KulturPlatformDb başarıyla restore edildi!"
info "  API container'ını yeniden başlatın:"
info "    docker compose restart api"
info "======================================================"
