<?php
    $config['plugins'] = [];
    $config['log_driver'] = 'stdout';
    $config['zipdownload_selection'] = true;
    $config['des_key'] = 'jPmytkHK7gXHLwp1JJlxxhOd';
    $config['enable_spellcheck'] = true;
    $config['spellcheck_engine'] = 'pspell';
    include(__DIR__ . '/config.docker.inc.php');

    // Override IMAP/SMTP to use SSL with no peer verification (internal Docker network)
    $config['imap_host'] = 'ssl://mailserver:993';
    $config['smtp_host'] = 'ssl://mailserver:465';
    $config['imap_conn_options'] = array(
        'ssl' => array(
            'verify_peer' => false,
            'verify_peer_name' => false,
            'allow_self_signed' => true,
        ),
    );
    $config['smtp_conn_options'] = array(
        'ssl' => array(
            'verify_peer' => false,
            'verify_peer_name' => false,
            'allow_self_signed' => true,
        ),
    );
