<?php
// Test Roundcube login flow via internal HTTP
$ch = curl_init();

// Step 1: GET login page to get session cookie and token
curl_setopt($ch, CURLOPT_URL, 'http://localhost:80/');
curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
curl_setopt($ch, CURLOPT_HEADER, true);
curl_setopt($ch, CURLOPT_COOKIEJAR, '/tmp/rc_cookies.txt');
curl_setopt($ch, CURLOPT_COOKIEFILE, '/tmp/rc_cookies.txt');

$response = curl_exec($ch);
$http_code = curl_getinfo($ch, CURLINFO_HTTP_CODE);
echo "GET login page: HTTP $http_code\n";

// Extract request token
if (preg_match('/name="_token"\s+value="([^"]+)"/', $response, $matches)) {
    $token = $matches[1];
    echo "Token: $token\n";
} else {
    echo "No token found in response\n";
    // Show a snippet of the response body
    $header_size = curl_getinfo($ch, CURLINFO_HEADER_SIZE);
    $body = substr($response, $header_size, 2000);
    echo "Body snippet: " . substr($body, 0, 500) . "\n";
    exit(1);
}

// Step 2: POST login
curl_setopt($ch, CURLOPT_URL, 'http://localhost:80/?_task=login');
curl_setopt($ch, CURLOPT_POST, true);
curl_setopt($ch, CURLOPT_POSTFIELDS, http_build_query([
    '_action' => 'login',
    '_task' => 'login',
    '_user' => 'info@kulturplattformfreiburg.org',
    '_pass' => 'KpF-Info2026!Secure',
    '_token' => $token,
]));
curl_setopt($ch, CURLOPT_FOLLOWLOCATION, false);

$response2 = curl_exec($ch);
$http_code2 = curl_getinfo($ch, CURLINFO_HTTP_CODE);
echo "POST login: HTTP $http_code2\n";

// Check for redirect (302 = success, 401 = failure)
if ($http_code2 == 302) {
    echo "LOGIN SUCCESS!\n";
} else {
    echo "Login result code: $http_code2\n";
    $header_size = curl_getinfo($ch, CURLINFO_HEADER_SIZE);
    $headers = substr($response2, 0, $header_size);
    echo "Response headers:\n$headers\n";
}

curl_close($ch);
