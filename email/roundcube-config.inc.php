<?php
    $config['plugins'] = [];
    $config['log_driver'] = 'stdout';
    $config['zipdownload_selection'] = true;
    $config['des_key'] = 'jPmytkHK7gXHLwp1JJlxxhOd';
    $config['enable_spellcheck'] = true;
    $config['spellcheck_engine'] = 'pspell';

    // IMAP: plain on Docker internal network (Dovecot allows plaintext on trusted nets)
    $config['imap_host'] = 'mailserver:143';

    // SMTP: port 587 requires STARTTLS
    $config['smtp_host'] = 'tls://mailserver:587';
    $config['smtp_user'] = '%u';
    $config['smtp_pass'] = '%p';

    // Skip certificate verification for internal Docker network
    // (cert is issued for mail.kulturplattformfreiburg.org, not container name)
    $config['smtp_conn_options'] = [
        'ssl' => [
            'verify_peer' => false,
            'verify_peer_name' => false,
            'allow_self_signed' => true,
        ],
    ];
