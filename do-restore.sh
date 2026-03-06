#!/bin/bash
set -e
SA_PASS='KpfSecure@2026!Netcup'
CONTAINER=kpf_sqlserver

echo "=== Checking logical names ==="
docker exec $CONTAINER /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "$SA_PASS" -C -No -Q "RESTORE FILELISTONLY FROM DISK = '/var/opt/mssql/backup/KulturPlatformDb.bak'" 2>&1 | head -5

echo "=== Restoring database ==="
docker exec $CONTAINER /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "$SA_PASS" -C -No -Q "RESTORE DATABASE [KulturPlatformDb] FROM DISK = '/var/opt/mssql/backup/KulturPlatformDb.bak' WITH MOVE 'KulturPlatformDb' TO '/var/opt/mssql/data/KulturPlatformDb.mdf', MOVE 'KulturPlatformDb_log' TO '/var/opt/mssql/data/KulturPlatformDb_log.ldf', REPLACE, STATS = 10;"

echo "=== Verifying ==="
docker exec $CONTAINER /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "$SA_PASS" -C -No -Q "SELECT name, state_desc FROM sys.databases WHERE name='KulturPlatformDb';"
echo "=== Done ==="
