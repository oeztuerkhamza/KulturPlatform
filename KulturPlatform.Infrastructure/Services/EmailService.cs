using KulturPlatform.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Mail;

namespace KulturPlatform.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task SendContactMessageNotificationAsync(
            string senderName,
            string senderEmail,
            string? senderPhone,
            string subject,
            string message,
            CancellationToken cancellationToken = default)
        {
            var emailSettings = _configuration.GetSection("EmailSettings");
            var recipientEmail = emailSettings["RecipientEmail"] ?? "info@kulturplattformfreiburg.org";

            var emailBody = $@"
<html>
<body style='font-family: Arial, sans-serif;'>
    <h2 style='color: #333;'>Neue Kontaktnachricht</h2>
    <div style='background-color: #f5f5f5; padding: 20px; border-radius: 5px;'>
        <p><strong>Von:</strong> {senderName}</p>
        <p><strong>E-Mail:</strong> {senderEmail}</p>
        {(!string.IsNullOrWhiteSpace(senderPhone) ? $"<p><strong>Telefon:</strong> {senderPhone}</p>" : "")}
        <p><strong>Betreff:</strong> {subject}</p>
        <hr style='border: 1px solid #ddd;' />
        <p><strong>Nachricht:</strong></p>
        <p style='white-space: pre-wrap;'>{message}</p>
    </div>
    <p style='color: #666; font-size: 12px; margin-top: 20px;'>
        Diese Nachricht wurde über das Kontaktformular auf der Website gesendet.
    </p>
</body>
</html>";

            await SendEmailAsync(
                recipientEmail,
                $"Neue Kontaktnachricht: {subject}",
                emailBody,
                cancellationToken);
        }

        public async Task SendVolunteerSubmissionNotificationAsync(
            string fullName,
            string email,
            string? phoneNumber,
            string message,
            CancellationToken cancellationToken = default)
        {
            var emailSettings = _configuration.GetSection("EmailSettings");
            var recipientEmail = emailSettings["RecipientEmail"] ?? "info@kulturplattformfreiburg.org";

            var emailBody = $@"
<html>
<body style='font-family: Arial, sans-serif;'>
    <h2 style='color: #333;'>Neue Freiwilligenbewerbung</h2>
    <div style='background-color: #f5f5f5; padding: 20px; border-radius: 5px;'>
        <p><strong>Name:</strong> {fullName}</p>
        <p><strong>E-Mail:</strong> {email}</p>
        {(!string.IsNullOrWhiteSpace(phoneNumber) ? $"<p><strong>Telefon:</strong> {phoneNumber}</p>" : "")}
        <hr style='border: 1px solid #ddd;' />
        <p><strong>Nachricht:</strong></p>
        <p style='white-space: pre-wrap;'>{message}</p>
    </div>
    <p style='color: #666; font-size: 12px; margin-top: 20px;'>
        Diese Bewerbung wurde über das Freiwilligenformular auf der Website eingereicht.
    </p>
</body>
</html>";

            await SendEmailAsync(
                recipientEmail,
                "Neue Freiwilligenbewerbung",
                emailBody,
                cancellationToken);
        }

        private async Task SendEmailAsync(
            string toEmail,
            string subject,
            string htmlBody,
            CancellationToken cancellationToken = default)
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
                    Credentials = new NetworkCredential(username, password)
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(fromEmail, fromName),
                    Subject = subject,
                    Body = htmlBody,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(toEmail);

                await smtpClient.SendMailAsync(mailMessage, cancellationToken);
                _logger.LogInformation("? Email sent successfully to {Email}", toEmail);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "? Failed to send email to {Email}", toEmail);
                // Don't throw - we don't want email failures to break the application flow
            }
        }
    }
}
