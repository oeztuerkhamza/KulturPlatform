#!/bin/bash
# ═══════════════════════════════════════════════════════════════════════════════
# Firewall Ayarları - E-posta portlarını aç
# ═══════════════════════════════════════════════════════════════════════════════

set -euo pipefail

echo "E-posta portları açılıyor..."

# UFW varsa
if command -v ufw &> /dev/null; then
    ufw allow 25/tcp   comment "SMTP - Incoming mail"
    ufw allow 465/tcp  comment "SMTPS - Secure submission"
    ufw allow 587/tcp  comment "SMTP - STARTTLS submission"
    ufw allow 993/tcp  comment "IMAPS - Mail client"
    ufw reload
    echo "✓ UFW kuralları eklendi"
fi

# iptables ile de ekle (güvenlik için)
iptables -A INPUT -p tcp --dport 25 -j ACCEPT 2>/dev/null || true
iptables -A INPUT -p tcp --dport 465 -j ACCEPT 2>/dev/null || true
iptables -A INPUT -p tcp --dport 587 -j ACCEPT 2>/dev/null || true
iptables -A INPUT -p tcp --dport 993 -j ACCEPT 2>/dev/null || true

echo "✓ Firewall ayarları tamamlandı"
echo ""
echo "Açık portlar:"
echo "  25  - SMTP (gelen e-posta)"
echo "  465 - SMTPS (güvenli gönderim)"
echo "  587 - SMTP STARTTLS (gönderim)"
echo "  993 - IMAPS (mail client)"
