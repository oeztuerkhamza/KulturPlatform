/* ============================================================================
   Newsletter-Abonnentenliste bereinigen (SQL Server / T-SQL)

   Hintergrund
   -----------
   Der oeffentliche Endpunkt /api/newsletter/subscribe hat ohne Limit fuer jede
   beliebige Adresse eine Bestaetigungsmail verschickt. Dadurch sind fremde
   Adressen in die Liste geraten. Solange sie drin stehen, erzeugt jede Kampagne
   weiter Bounces und beschaedigt die Reputation der sendenden IP.

   Diese Datei ist in drei Teile geteilt. SCHRITT 1 und 2 lesen nur. Erst
   SCHRITT 3 loescht, und auch das in einer Transaktion, die bewusst nicht
   automatisch committet wird.

   NICHT die ganze Datei auf einmal ausfuehren. Block fuer Block markieren
   und einzeln starten.

   Vorher ein Backup ziehen:
     BACKUP DATABASE KulturPlatformDb
       TO DISK = '/var/opt/mssql/backup/vor-newsletter-cleanup.bak' WITH INIT;
   ============================================================================ */


/* ---------------------------------------------------------------------------
   SCHRITT 1 - Lagebild. Veraendert nichts.
   --------------------------------------------------------------------------- */

-- 1a) Gesamtzahlen
SELECT
    COUNT(*)                                                   AS Gesamt,
    SUM(CASE WHEN IsVerified = 1 THEN 1 ELSE 0 END)            AS Bestaetigt,
    SUM(CASE WHEN IsVerified = 0 THEN 1 ELSE 0 END)            AS Unbestaetigt,
    SUM(CASE WHEN IsActive  = 0 THEN 1 ELSE 0 END)             AS Abgemeldet
FROM dbo.NewsletterSubscribers;

-- 1b) Anmeldungen pro Tag. Der Angriffszeitraum faellt hier als Ausreisser auf:
--     an normalen Tagen stehen einstellige Zahlen, an Angriffstagen Hunderte.
SELECT
    CAST(SubscribedAt AS date)                                 AS Tag,
    COUNT(*)                                                   AS Anmeldungen,
    SUM(CASE WHEN IsVerified = 1 THEN 1 ELSE 0 END)            AS DavonBestaetigt
FROM dbo.NewsletterSubscribers
GROUP BY CAST(SubscribedAt AS date)
ORDER BY Tag DESC;

-- 1c) Anmeldungen pro Stunde an den auffaelligsten Tagen. Ein Bot erzeugt
--     Dutzende Eintraege innerhalb weniger Minuten, ein Mensch nicht.
SELECT TOP (50)
    DATEADD(hour, DATEDIFF(hour, 0, SubscribedAt), 0)          AS Stunde,
    COUNT(*)                                                   AS Anmeldungen
FROM dbo.NewsletterSubscribers
GROUP BY DATEADD(hour, DATEDIFF(hour, 0, SubscribedAt), 0)
HAVING COUNT(*) > 5
ORDER BY Anmeldungen DESC;


/* ---------------------------------------------------------------------------
   SCHRITT 2 - Vorschau. Zeigt genau die Zeilen, die SCHRITT 3 loeschen wuerde.
   Veraendert nichts.

   Kriterium: nie bestaetigt UND aelter als die Karenzzeit.

   Die Karenzzeit schuetzt echte Interessenten, die sich gerade erst angemeldet
   und noch nicht geklickt haben. 7 Tage sind grosszuegig - wer nach einer Woche
   nicht bestaetigt hat, tut es erfahrungsgemaess nicht mehr. Unbestaetigte
   Adressen duerfen nach DSGVO ohnehin nicht angeschrieben werden, der Eintrag
   hat also keinen Wert.

   Zum Anpassen: @KarenzTage aendern.
   --------------------------------------------------------------------------- */

DECLARE @KarenzTage int = 7;

SELECT
    Id,
    Email,
    FullName,
    SubscribedAt,
    Source,
    IsActive
FROM dbo.NewsletterSubscribers
WHERE IsVerified = 0
  AND SubscribedAt < DATEADD(day, -@KarenzTage, SYSUTCDATETIME())
ORDER BY SubscribedAt DESC;

-- Wie viele waeren es?
SELECT COUNT(*) AS WirdGeloescht
FROM dbo.NewsletterSubscribers
WHERE IsVerified = 0
  AND SubscribedAt < DATEADD(day, -@KarenzTage, SYSUTCDATETIME());


/* ---------------------------------------------------------------------------
   SCHRITT 3 - Loeschen.

   Erst ausfuehren, wenn die Liste aus SCHRITT 2 plausibel aussieht.

   Der Block laeuft in einer offenen Transaktion und endet NICHT mit COMMIT.
   Nach dem Durchlauf die Zeilenzahl pruefen und dann von Hand entscheiden:
       COMMIT TRANSACTION;    -- uebernehmen
       ROLLBACK TRANSACTION;  -- verwerfen
   Solange nichts davon abgesetzt ist, haelt die Transaktion Sperren auf der
   Tabelle. Also nicht offen stehen lassen.
   --------------------------------------------------------------------------- */

DECLARE @KarenzTageDelete int = 7;

BEGIN TRANSACTION;

    -- Sicherheitskopie der betroffenen Zeilen in eine Tabelle mit Datumsstempel.
    -- Damit laesst sich die Loeschung auch nach dem COMMIT noch nachvollziehen.
    DECLARE @Backup nvarchar(128) =
        N'NewsletterSubscribers_Geloescht_' + FORMAT(SYSUTCDATETIME(), 'yyyyMMdd_HHmm');

    DECLARE @sql nvarchar(max) = N'
        SELECT *
        INTO dbo.' + QUOTENAME(@Backup) + N'
        FROM dbo.NewsletterSubscribers
        WHERE IsVerified = 0
          AND SubscribedAt < DATEADD(day, -' + CAST(@KarenzTageDelete AS nvarchar(10)) + N', SYSUTCDATETIME());';

    EXEC sp_executesql @sql;

    PRINT 'Sicherungstabelle: ' + @Backup;

    DELETE FROM dbo.NewsletterSubscribers
    WHERE IsVerified = 0
      AND SubscribedAt < DATEADD(day, -@KarenzTageDelete, SYSUTCDATETIME());

    PRINT 'Geloeschte Zeilen: ' + CAST(@@ROWCOUNT AS nvarchar(10));

-- Jetzt pruefen, dann von Hand COMMIT TRANSACTION; oder ROLLBACK TRANSACTION;


/* ---------------------------------------------------------------------------
   Danach
   ------
   Die Bounces hoeren damit nicht sofort auf: in der Postfix-Queue koennen noch
   Nachrichten an die alten Adressen liegen. Auf dem Server:

     docker exec kpf_mailserver postqueue -p          # Queue ansehen
     docker exec kpf_mailserver postsuper -d ALL      # Queue komplett leeren

   postsuper -d ALL verwirft auch legitime wartende Mails. Vorher mit
   postqueue -p anschauen, was drin liegt.
   --------------------------------------------------------------------------- */
