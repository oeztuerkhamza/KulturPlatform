using Azure;
using Azure.Communication.Email;
using KulturPlatform.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

namespace KulturPlatform.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailService> _logger;
        private readonly EmailClient? _azureEmailClient;
        private readonly string _emailProvider;
        private static readonly ConcurrentDictionary<string, List<DateTime>> _rateLimitTracker = new();
        private static readonly object _cleanupLock = new();
        private static DateTime _lastCleanup = DateTime.UtcNow;

        public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
        {
            _configuration = configuration;
            _logger = logger;

            var emailSettings = _configuration.GetSection("EmailSettings");
            _emailProvider = emailSettings["Provider"] ?? "Smtp";

            // Initialize Azure Email Client if configured
            if (_emailProvider.Equals("AzureCommunicationServices", StringComparison.OrdinalIgnoreCase))
            {
                var connectionString = emailSettings["AzureConnectionString"];
                if (!string.IsNullOrWhiteSpace(connectionString))
                {
                    _azureEmailClient = new EmailClient(connectionString);
                    _logger.LogInformation("? Azure Communication Services Email initialized");
                }
                else
                {
                    _logger.LogWarning("?? Azure Connection String not configured, falling back to SMTP");
                    _emailProvider = "Smtp";
                }
            }
        }

        public async Task<bool> IsRateLimitExceededAsync(string identifier)
        {
            var emailSettings = _configuration.GetSection("EmailSettings");
            var maxEmailsPerHour = int.Parse(emailSettings["MaxEmailsPerHour"] ?? "10");
            var rateLimitWindowMinutes = int.Parse(emailSettings["RateLimitWindowMinutes"] ?? "60");

            // Cleanup old entries every 15 minutes
            lock (_cleanupLock)
            {
                if ((DateTime.UtcNow - _lastCleanup).TotalMinutes > 15)
                {
                    CleanupOldEntries(rateLimitWindowMinutes);
                    _lastCleanup = DateTime.UtcNow;
                }
            }

            var now = DateTime.UtcNow;
            var cutoffTime = now.AddMinutes(-rateLimitWindowMinutes);

            var timestamps = _rateLimitTracker.GetOrAdd(identifier, _ => new List<DateTime>());

            lock (timestamps)
            {
                // Remove old timestamps
                timestamps.RemoveAll(t => t < cutoffTime);

                if (timestamps.Count >= maxEmailsPerHour)
                {
                    _logger.LogWarning("Rate limit exceeded for {Identifier}. {Count} emails in last {Minutes} minutes",
                        identifier, timestamps.Count, rateLimitWindowMinutes);
                    return true;
                }

                timestamps.Add(now);
            }

            return false;
        }

        public string SanitizeHtmlContent(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
                return string.Empty;

            // Remove potentially dangerous HTML tags and attributes
            var sanitized = content;

            // Remove script tags
            sanitized = Regex.Replace(sanitized, @"<script[^>]*>[\s\S]*?</script>", string.Empty, RegexOptions.IgnoreCase);

            // Remove iframe tags
            sanitized = Regex.Replace(sanitized, @"<iframe[^>]*>[\s\S]*?</iframe>", string.Empty, RegexOptions.IgnoreCase);

            // Remove object/embed tags
            sanitized = Regex.Replace(sanitized, @"<(object|embed)[^>]*>[\s\S]*?</\1>", string.Empty, RegexOptions.IgnoreCase);

            // Remove on* event handlers
            sanitized = Regex.Replace(sanitized, @"\s*on\w+\s*=\s*[""'][^""']*[""']", string.Empty, RegexOptions.IgnoreCase);
            sanitized = Regex.Replace(sanitized, @"\s*on\w+\s*=\s*[^\s>]*", string.Empty, RegexOptions.IgnoreCase);

            // HTML encode to prevent XSS
            sanitized = System.Net.WebUtility.HtmlEncode(sanitized);

            return sanitized;
        }

        private void CleanupOldEntries(int rateLimitWindowMinutes)
        {
            var cutoffTime = DateTime.UtcNow.AddMinutes(-rateLimitWindowMinutes * 2);

            foreach (var kvp in _rateLimitTracker.ToArray())
            {
                lock (kvp.Value)
                {
                    kvp.Value.RemoveAll(t => t < cutoffTime);
                    if (kvp.Value.Count == 0)
                    {
                        _rateLimitTracker.TryRemove(kvp.Key, out _);
                    }
                }
            }
        }

        public async Task SendContactMessageNotificationAsync(
            string senderName,
            string senderEmail,
            string? senderPhone,
            string subject,
            string message,
            CancellationToken cancellationToken = default)
        {
            // Check rate limit
            if (await IsRateLimitExceededAsync(senderEmail))
            {
                _logger.LogWarning("Rate limit exceeded for contact message from {Email}", senderEmail);
                throw new InvalidOperationException("Too many emails sent. Please try again later.");
            }

            var emailSettings = _configuration.GetSection("EmailSettings");
            var recipientEmail = emailSettings["RecipientEmail"] ?? "info@kulturplattformfreiburg.org";

            // Sanitize user inputs
            var sanitizedName = SanitizeHtmlContent(senderName);
            var sanitizedSubject = SanitizeHtmlContent(subject);
            var sanitizedMessage = SanitizeHtmlContent(message);
            var sanitizedPhone = SanitizeHtmlContent(senderPhone ?? string.Empty);

            var emailBody = $@"
<html>
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
</head>
<body style='font-family: Arial, sans-serif; margin: 0; padding: 0;'>
    <table role=""presentation"" style=""width: 100%; border-collapse: collapse;"">
        <tr>
            <td style=""padding: 20px 0; text-align: center; background-color: #f8f9fa;"">
                <h2 style='color: #333; margin: 0;'>Neue Kontaktnachricht</h2>
            </td>
        </tr>
        <tr>
            <td style=""padding: 20px;"">
                <div style='background-color: #f5f5f5; padding: 20px; border-radius: 5px; border: 1px solid #ddd;'>
                    <p style=""margin: 10px 0;""><strong>Von:</strong> {sanitizedName}</p>
                    <p style=""margin: 10px 0;""><strong>E-Mail:</strong> {senderEmail}</p>
                    {(!string.IsNullOrWhiteSpace(sanitizedPhone) ? $"<p style=\"margin: 10px 0;\"><strong>Telefon:</strong> {sanitizedPhone}</p>" : "")}
                    <p style=""margin: 10px 0;""><strong>Betreff:</strong> {sanitizedSubject}</p>
                    <hr style='border: none; border-top: 1px solid #ddd; margin: 20px 0;' />
                    <p style=""margin: 10px 0;""><strong>Nachricht:</strong></p>
                    <p style='white-space: pre-wrap; margin: 10px 0; padding: 10px; background-color: #fff; border-radius: 3px;'>{sanitizedMessage}</p>
                </div>
            </td>
        </tr>
        <tr>
            <td style=""padding: 20px; text-align: center; background-color: #f8f9fa;"">
                <p style='color: #666; font-size: 12px; margin: 0;'>
                    Diese Nachricht wurde über das Kontaktformular auf der Website gesendet.
                </p>
                <p style='color: #666; font-size: 12px; margin: 5px 0 0 0;'>
                    Kultur Platform Freiburg | <a href=""https://kulturplattformfreiburg.org"" style=""color: #007bff;"">kulturplattformfreiburg.org</a>
                </p>
            </td>
        </tr>
    </table>
</body>
</html>";

            await SendEmailAsync(
                recipientEmail,
                $"Neue Kontaktnachricht: {sanitizedSubject}",
                emailBody,
                senderEmail,
                cancellationToken);
        }

        public async Task SendVolunteerSubmissionNotificationAsync(
            string fullName,
            string email,
            string? phoneNumber,
            string message,
            CancellationToken cancellationToken = default)
        {
            // Check rate limit
            if (await IsRateLimitExceededAsync(email))
            {
                _logger.LogWarning("Rate limit exceeded for volunteer submission from {Email}", email);
                throw new InvalidOperationException("Too many emails sent. Please try again later.");
            }

            var emailSettings = _configuration.GetSection("EmailSettings");
            var recipientEmail = emailSettings["RecipientEmail"] ?? "info@kulturplattformfreiburg.org";

            // Sanitize user inputs
            var sanitizedName = SanitizeHtmlContent(fullName);
            var sanitizedMessage = SanitizeHtmlContent(message);
            var sanitizedPhone = SanitizeHtmlContent(phoneNumber ?? string.Empty);

            var emailBody = $@"
<html>
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
</head>
<body style='font-family: Arial, sans-serif; margin: 0; padding: 0;'>
    <table role=""presentation"" style=""width: 100%; border-collapse: collapse;"">
        <tr>
            <td style=""padding: 20px 0; text-align: center; background-color: #f8f9fa;"">
                <h2 style='color: #333; margin: 0;'>Neue Freiwilligenbewerbung</h2>
            </td>
        </tr>
        <tr>
            <td style=""padding: 20px;"">
                <div style='background-color: #f5f5f5; padding: 20px; border-radius: 5px; border: 1px solid #ddd;'>
                    <p style=""margin: 10px 0;""><strong>Name:</strong> {sanitizedName}</p>
                    <p style=""margin: 10px 0;""><strong>E-Mail:</strong> {email}</p>
                    {(!string.IsNullOrWhiteSpace(sanitizedPhone) ? $"<p style=\"margin: 10px 0;\"><strong>Telefon:</strong> {sanitizedPhone}</p>" : "")}
                    <hr style='border: none; border-top: 1px solid #ddd; margin: 20px 0;' />
                    <p style=""margin: 10px 0;""><strong>Nachricht:</strong></p>
                    <p style='white-space: pre-wrap; margin: 10px 0; padding: 10px; background-color: #fff; border-radius: 3px;'>{sanitizedMessage}</p>
                </div>
            </td>
        </tr>
        <tr>
            <td style=""padding: 20px; text-align: center; background-color: #f8f9fa;"">
                <p style='color: #666; font-size: 12px; margin: 0;'>
                    Diese Bewerbung wurde über das Freiwilligenformular auf der Website eingereicht.
                </p>
                <p style='color: #666; font-size: 12px; margin: 5px 0 0 0;'>
                    Kultur Platform Freiburg | <a href=""https://kulturplattformfreiburg.org"" style=""color: #007bff;"">kulturplattformfreiburg.org</a>
                </p>
            </td>
        </tr>
    </table>
</body>
</html>";

            await SendEmailAsync(
                recipientEmail,
                "Neue Freiwilligenbewerbung",
                emailBody,
                email,
                cancellationToken);
        }

        private async Task SendEmailAsync(
            string toEmail,
            string subject,
            string htmlBody,
            string? replyToEmail = null,
            CancellationToken cancellationToken = default)
        {
            if (_emailProvider.Equals("AzureCommunicationServices", StringComparison.OrdinalIgnoreCase)
                && _azureEmailClient != null)
            {
                await SendEmailViaAzureAsync(toEmail, subject, htmlBody, replyToEmail, cancellationToken);
            }
            else
            {
                await SendEmailViaSmtpAsync(toEmail, subject, htmlBody, replyToEmail, cancellationToken);
            }
        }

        // Public method for newsletter and other services (implements IEmailService)
        async Task IEmailService.SendEmailAsync(
            string toEmail,
            string subject,
            string htmlBody,
            string? replyToEmail,
            CancellationToken cancellationToken)
        {
            await SendEmailAsync(toEmail, subject, htmlBody, replyToEmail, cancellationToken);
        }

        private async Task SendEmailViaAzureAsync(
            string toEmail,
            string subject,
            string htmlBody,
            string? replyToEmail,
            CancellationToken cancellationToken)
        {
            try
            {
                var emailSettings = _configuration.GetSection("EmailSettings");
                var senderEmail = emailSettings["AzureSenderEmail"]
                    ?? "DoNotReply@kulturplattformfreiburg.org";
                var senderName = emailSettings["AzureSenderName"]
                    ?? "Kultur Platform Freiburg";

                var emailContent = new EmailContent(subject)
                {
                    Html = htmlBody
                };

                var emailRecipients = new EmailRecipients(new List<EmailAddress>
                {
                    new EmailAddress(toEmail)
                });

                var emailMessage = new EmailMessage(
                    senderAddress: senderEmail,
                    emailRecipients,
                    emailContent);

                // Add Reply-To if provided
                if (!string.IsNullOrWhiteSpace(replyToEmail))
                {
                    emailMessage.ReplyTo.Add(new EmailAddress(replyToEmail));
                }

                // Add custom headers for better deliverability
                emailMessage.Headers.Add("X-Mailer", "KulturPlatformFreiburg-Azure/1.0");
                emailMessage.Headers.Add("Organization", "Kultur Platform Freiburg");
                emailMessage.Headers.Add("List-Unsubscribe-Post", "List-Unsubscribe=One-Click");

                EmailSendOperation emailSendOperation = await _azureEmailClient!.SendAsync(
                    WaitUntil.Completed,
                    emailMessage,
                    cancellationToken);

                _logger.LogInformation(
                    "? Email sent via Azure to {Email}. MessageId: {MessageId}, Status: {Status}",
                    toEmail,
                    emailSendOperation.Id,
                    emailSendOperation.Value.Status);

                // Check delivery status - Azure SDK 1.0.1 doesn't expose error details in EmailSendResult
                if (emailSendOperation.Value.Status == EmailSendStatus.Failed)
                {
                    _logger.LogError(
                        "? Azure email delivery failed to {Email}",
                        toEmail);
                    throw new InvalidOperationException("Email delivery failed");
                }
            }
            catch (RequestFailedException azureEx)
            {
                _logger.LogError(azureEx,
                    "? Azure Communication Services error for {Email}. ErrorCode: {ErrorCode}",
                    toEmail, azureEx.ErrorCode);

                // Fallback to SMTP if Azure fails
                _logger.LogWarning("?? Falling back to SMTP due to Azure error");
                await SendEmailViaSmtpAsync(toEmail, subject, htmlBody, replyToEmail, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "? Unexpected error while sending email via Azure to {Email}", toEmail);
                throw;
            }
        }

        private async Task SendEmailViaSmtpAsync(
            string toEmail,
            string subject,
            string htmlBody,
            string? replyToEmail,
            CancellationToken cancellationToken)
        {
            try
            {
                var emailSettings = _configuration.GetSection("EmailSettings");
                var smtpHost = emailSettings["SmtpHost"];
                var smtpPort = int.Parse(emailSettings["SmtpPort"] ?? "587");
                var fromEmail = emailSettings["FromEmail"];
                var fromName = emailSettings["FromName"] ?? "Kultur Platform Freiburg";
                var username = emailSettings["Username"];
                var password = emailSettings["Password"];
                var enableSsl = bool.Parse(emailSettings["EnableSsl"] ?? "true");

                if (string.IsNullOrWhiteSpace(smtpHost) || string.IsNullOrWhiteSpace(fromEmail))
                {
                    _logger.LogWarning("?? Email settings not configured. Email not sent.");
                    return;
                }

                using var smtpClient = new SmtpClient(smtpHost, smtpPort)
                {
                    EnableSsl = enableSsl,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(username, password),
                    Timeout = 30000 // 30 seconds timeout
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(fromEmail, fromName),
                    Subject = subject,
                    Body = htmlBody,
                    IsBodyHtml = true,
                    BodyEncoding = Encoding.UTF8,
                    SubjectEncoding = Encoding.UTF8,
                    Priority = MailPriority.Normal
                };

                // Add recipient
                mailMessage.To.Add(toEmail);

                // Add Reply-To header if provided (important for spam prevention)
                if (!string.IsNullOrWhiteSpace(replyToEmail))
                {
                    mailMessage.ReplyToList.Add(new MailAddress(replyToEmail));
                }

                // Add anti-spam headers
                mailMessage.Headers.Add("X-Mailer", "KulturPlatformFreiburg/1.0");
                mailMessage.Headers.Add("X-Priority", "3"); // Normal priority
                mailMessage.Headers.Add("X-MSMail-Priority", "Normal");
                mailMessage.Headers.Add("Importance", "Normal");
                
                // Add List-Unsubscribe header (recommended for transactional emails)
                mailMessage.Headers.Add("List-Unsubscribe", $"<mailto:{fromEmail}?subject=unsubscribe>");
                
                // Add organization info
                mailMessage.Headers.Add("Organization", "Kultur Platform Freiburg");
                
                // Add Message-ID (auto-generated by SmtpClient but we ensure proper format)
                var messageId = $"<{Guid.NewGuid()}@kulturplattformfreiburg.org>";
                mailMessage.Headers.Add("Message-ID", messageId);

                // Add MIME version
                mailMessage.Headers.Add("MIME-Version", "1.0");

                // Add proper content type
                mailMessage.Headers.Add("Content-Type", "text/html; charset=utf-8");

                await smtpClient.SendMailAsync(mailMessage, cancellationToken);
                _logger.LogInformation("? Email sent successfully to {Email} with subject: {Subject}", toEmail, subject);
            }
            catch (SmtpException smtpEx)
            {
                _logger.LogError(smtpEx, "? SMTP error while sending email to {Email}. StatusCode: {StatusCode}", 
                    toEmail, smtpEx.StatusCode);
                throw; // Re-throw SMTP exceptions to inform the caller
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "? Failed to send email to {Email}", toEmail);
                throw; // Re-throw to inform the caller
            }
        }
    }
}
