<?php
    $config['plugins'] = [];
    $config['log_driver'] = 'stdout';
    $config['zipdownload_selection'] = true;
    $config['des_key'] = 'jPmytkHK7gXHLwp1JJlxxhOd';
    $config['enable_spellcheck'] = true;
    $config['spellcheck_engine'] = 'pspell';

    // Use plain IMAP/SMTP on Docker internal network (no SSL needed between containers)
    $config['imap_host'] = 'mailserver:143';
    $config['smtp_host'] = 'mailserver:587';
    $config['smtp_user'] = '%u';
    $config['smtp_pass'] = '%p';
