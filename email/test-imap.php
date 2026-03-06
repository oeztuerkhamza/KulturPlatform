<?php
$ctx = stream_context_create([
    'ssl' => [
        'verify_peer' => false,
        'verify_peer_name' => false,
        'allow_self_signed' => true,
    ]
]);

echo "Connecting to ssl://mailserver:993...\n";
$fp = @stream_socket_client('ssl://mailserver:993', $errno, $errstr, 10, STREAM_CLIENT_CONNECT, $ctx);
if (!$fp) {
    echo "FAILED: $errstr ($errno)\n";
} else {
    $greeting = fgets($fp);
    echo "Connected! Server says: $greeting\n";
    
    // Try login
    fwrite($fp, "A1 LOGIN info@kulturplattformfreiburg.org \"KpF-Info2026!Secure\"\r\n");
    $response = fgets($fp);
    echo "Login response: $response\n";
    
    fclose($fp);
}
