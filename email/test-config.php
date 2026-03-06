<?php
include('/var/www/html/config/config.inc.php');
echo "IMAP: " . $config['imap_host'] . "\n";
echo "SMTP: " . $config['smtp_host'] . "\n";
echo "IMAP OPTIONS:\n";
print_r($config['imap_conn_options'] ?? 'NOT SET');
echo "\nSMTP OPTIONS:\n";
print_r($config['smtp_conn_options'] ?? 'NOT SET');
