using KulturPlatform.Application.Interfaces;
using KulturPlatform.Application.Interfaces.Newsletter;
using KulturPlatform.Domain.Commons.Aggregates;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace KulturPlatform.Infrastructure.Services
{
    public class NewsletterService : INewsletterService
    {
        private readonly INewsletterSubscriberRepository _subscriberRepository;
        private readonly INewsletterCampaignRepository _campaignRepository;
        private readonly IEmailService _emailService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<NewsletterService> _logger;
        private readonly IConfiguration _configuration;

        public NewsletterService(
            INewsletterSubscriberRepository subscriberRepository,
            INewsletterCampaignRepository campaignRepository,
            IEmailService emailService,
            IUnitOfWork unitOfWork,
            ILogger<NewsletterService> logger,
            IConfiguration configuration)
        {
            _subscriberRepository = subscriberRepository;
            _campaignRepository = campaignRepository;
            _emailService = emailService;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<bool> SubscribeAsync(string email, string? fullName, CancellationToken cancellationToken = default)
        {
            // Check if already subscribed
            var existing = await _subscriberRepository.GetByEmailAsync(email, cancellationToken);
            if (existing != null)
            {
                if (existing.IsActive && existing.IsVerified)
                {
                    _logger.LogInformation("Email {Email} is already subscribed and verified", email);
                    return false;
                }

                if (existing.IsActive && !existing.IsVerified)
                {
                    _logger.LogInformation("Email {Email} is pending verification. Resending verification email", email);
                    await SendVerificationEmailAsync(existing, cancellationToken);
                    return true;
                }

                // Reactivate if unsubscribed
                existing.Reactivate();
                await _subscriberRepository.UpdateAsync(existing, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                await SendVerificationEmailAsync(existing, cancellationToken);
                return true;
            }

            // Create new subscriber
            var subscriber = NewsletterSubscriber.Create(
                new Email(email),
                fullName);

            await _subscriberRepository.AddAsync(subscriber, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Send verification email (Double Opt-In)
            await SendVerificationEmailAsync(subscriber, cancellationToken);

            _logger.LogInformation("New newsletter subscription created for {Email}", email);
            return true;
        }

        public async Task<bool> VerifySubscriptionAsync(string token, CancellationToken cancellationToken = default)
        {
            var subscriber = await _subscriberRepository.GetByVerificationTokenAsync(token, cancellationToken);
            if (subscriber == null)
            {
                _logger.LogWarning("Invalid verification token: {Token}", token);
                return false;
            }

            if (subscriber.IsVerified)
            {
                _logger.LogInformation("Subscription already verified for {Email}", subscriber.Email.Value);
                return true;
            }

            subscriber.Verify();
            await _subscriberRepository.UpdateAsync(subscriber, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Send welcome email
            await SendWelcomeEmailAsync(subscriber, cancellationToken);

            _logger.LogInformation("Newsletter subscription verified for {Email}", subscriber.Email.Value);
            return true;
        }

        public async Task<bool> UnsubscribeAsync(string token, CancellationToken cancellationToken = default)
        {
            var subscriber = await _subscriberRepository.GetByTokenAsync(token, cancellationToken);
            if (subscriber == null)
            {
                _logger.LogWarning("Invalid unsubscribe token: {Token}", token);
                return false;
            }

            subscriber.Unsubscribe();
            await _subscriberRepository.UpdateAsync(subscriber, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("User unsubscribed: {Email}", subscriber.Email.Value);
            return true;
        }

        public async Task<(int successful, int failed)> SendCampaignAsync(Guid campaignId, CancellationToken cancellationToken = default)
        {
            var campaign = await _campaignRepository.GetByIdAsync(campaignId, cancellationToken);
            if (campaign == null)
                throw new InvalidOperationException($"Campaign {campaignId} not found");

            // Get verified subscribers only
            var subscribers = await _subscriberRepository.GetVerifiedSubscribersAsync(cancellationToken);

            if (subscribers.Count == 0)
            {
                _logger.LogWarning("No verified subscribers found for campaign {CampaignId}", campaignId);
                return (0, 0);
            }

            campaign.MarkAsSending(subscribers.Count);
            await _campaignRepository.UpdateAsync(campaign, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            int successful = 0;
            int failed = 0;

            // Send in batches to avoid overwhelming the service
            const int batchSize = 10;
            var batches = subscribers.Chunk(batchSize);

            foreach (var batch in batches)
            {
                var tasks = batch.Select(async subscriber =>
                {
                    try
                    {
                        var personalizedContent = PersonalizeContent(campaign.ContentDe.Value, subscriber);
                        var unsubscribeUrl = GenerateUnsubscribeUrl(subscriber.UnsubscribeToken);
                        var htmlBody = BuildNewsletterHtml(
                            campaign.Subject.Value,
                            personalizedContent,
                            campaign.HeaderImageUrl,
                            unsubscribeUrl);

                        await _emailService.SendEmailAsync(
                            subscriber.Email.Value,
                            campaign.Subject.Value,
                            htmlBody,
                            null,
                            cancellationToken);

                        Interlocked.Increment(ref successful);
                        _logger.LogInformation("Newsletter sent to {Email} for campaign {CampaignId}", 
                            subscriber.Email.Value, campaignId);
                    }
                    catch (Exception ex)
                    {
                        Interlocked.Increment(ref failed);
                        _logger.LogError(ex, "Failed to send newsletter to {Email} for campaign {CampaignId}", 
                            subscriber.Email.Value, campaignId);
                    }
                });

                await Task.WhenAll(tasks);

                // Rate limiting: wait between batches
                if (batches.Any())
                    await Task.Delay(TimeSpan.FromSeconds(2), cancellationToken);
            }

            campaign.MarkAsSent(successful, failed);
            await _campaignRepository.UpdateAsync(campaign, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Campaign {CampaignId} sent. Successful: {Successful}, Failed: {Failed}", 
                campaignId, successful, failed);

            return (successful, failed);
        }

        public async Task SendTestEmailAsync(Guid campaignId, string testEmail, CancellationToken cancellationToken = default)
        {
            var campaign = await _campaignRepository.GetByIdAsync(campaignId, cancellationToken);
            if (campaign == null)
                throw new InvalidOperationException($"Campaign {campaignId} not found");

            var htmlBody = BuildNewsletterHtml(
                campaign.Subject.Value,
                campaign.ContentDe.Value,
                campaign.HeaderImageUrl,
                "#"); // Dummy unsubscribe link for test

            await _emailService.SendEmailAsync(
                testEmail,
                $"[TEST] {campaign.Subject.Value}",
                htmlBody,
                null,
                cancellationToken);

            _logger.LogInformation("Test email sent to {Email} for campaign {CampaignId}", testEmail, campaignId);
        }

        private async Task SendVerificationEmailAsync(NewsletterSubscriber subscriber, CancellationToken cancellationToken)
        {
            var verificationUrl = GenerateVerificationUrl(subscriber.VerificationToken!);
            var htmlBody = $@"
<!DOCTYPE html>
<html lang=""de"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
</head>
<body style=""font-family: Arial, sans-serif; margin: 0; padding: 0; background-color: #f4f4f4;"">
    <table role=""presentation"" style=""width: 100%; max-width: 600px; margin: 20px auto; background-color: #ffffff; border-radius: 8px; overflow: hidden;"">
        <tr>
            <td style=""padding: 40px 30px; text-align: center;"">
                <h2 style=""color: #333; margin: 0 0 20px 0;"">Newsletter-Anmeldung bestätigen</h2>
                <p style=""color: #666; line-height: 1.6; margin: 20px 0;"">
                    Vielen Dank für Ihr Interesse an unserem Newsletter!
                </p>
                <p style=""color: #666; line-height: 1.6; margin: 20px 0;"">
                    Bitte bestätigen Sie Ihre E-Mail-Adresse, indem Sie auf den folgenden Button klicken:
                </p>
                <a href=""{verificationUrl}"" style=""display: inline-block; padding: 15px 30px; background-color: #007bff; color: #ffffff; text-decoration: none; border-radius: 5px; margin: 20px 0;"">
                    E-Mail bestätigen / Confirm Email
                </a>
                <p style=""color: #999; font-size: 12px; margin: 30px 0 0 0;"">
                    Oder kopieren Sie diesen Link in Ihren Browser:<br/>
                    <a href=""{verificationUrl}"" style=""color: #007bff;"">{verificationUrl}</a>
                </p>
            </td>
        </tr>
    </table>
</body>
</html>";

            await _emailService.SendEmailAsync(
                subscriber.Email.Value,
                "Newsletter-Anmeldung bestätigen / Confirm Newsletter Subscription",
                htmlBody,
                null,
                cancellationToken);
        }

        private async Task SendWelcomeEmailAsync(NewsletterSubscriber subscriber, CancellationToken cancellationToken)
        {
            var unsubscribeUrl = GenerateUnsubscribeUrl(subscriber.UnsubscribeToken);
            var htmlBody = $@"
<!DOCTYPE html>
<html lang=""de"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
</head>
<body style=""font-family: Arial, sans-serif; margin: 0; padding: 0; background-color: #f4f4f4;"">
    <table role=""presentation"" style=""width: 100%; max-width: 600px; margin: 20px auto; background-color: #ffffff; border-radius: 8px; overflow: hidden;"">
        <tr>
            <td style=""padding: 40px 30px;"">
                <h2 style=""color: #333; margin: 0 0 20px 0;"">Willkommen! / Welcome!</h2>
                <p style=""color: #666; line-height: 1.6;"">
                    Vielen Dank für die Bestätigung Ihrer E-Mail-Adresse. Sie sind jetzt für unseren Newsletter angemeldet.
                </p>
                <p style=""color: #666; line-height: 1.6;"">
                    Sie erhalten nun regelmäßig Updates über unsere Aktivitäten und Veranstaltungen.
                </p>
                <hr style=""border: none; border-top: 1px solid #ddd; margin: 30px 0;"" />
                <p style=""color: #999; font-size: 12px;"">
                    Falls Sie sich abmelden möchten, klicken Sie <a href=""{unsubscribeUrl}"" style=""color: #dc3545;"">hier</a>.
                </p>
            </td>
        </tr>
    </table>
</body>
</html>";

            await _emailService.SendEmailAsync(
                subscriber.Email.Value,
                "Willkommen bei Kultur Platform Freiburg Newsletter",
                htmlBody,
                null,
                cancellationToken);
        }

        private string PersonalizeContent(string content, NewsletterSubscriber subscriber)
        {
            var result = content;
            result = result.Replace("{{name}}", subscriber.FullName ?? "Werte Leserin/Leser");
            result = result.Replace("{{email}}", subscriber.Email.Value);
            return result;
        }

        private string GenerateVerificationUrl(string token)
        {
            var baseUrl = _configuration["AppSettings:BaseUrl"] ?? "https://kulturplattformfreiburg.org";
            return $"{baseUrl}/newsletter/verify?token={token}";
        }

        private string GenerateUnsubscribeUrl(string token)
        {
            var baseUrl = _configuration["AppSettings:BaseUrl"] ?? "https://kulturplattformfreiburg.org";
            return $"{baseUrl}/newsletter/unsubscribe?token={token}";
        }

        private string BuildNewsletterHtml(string subject, string content, string? headerImageUrl, string unsubscribeUrl)
        {
            return $@"
<!DOCTYPE html>
<html lang=""de"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>{subject}</title>
</head>
<body style=""font-family: Arial, sans-serif; margin: 0; padding: 0; background-color: #f4f4f4;"">
    <table role=""presentation"" style=""width: 100%; max-width: 600px; margin: 20px auto; background-color: #ffffff; border-radius: 8px; overflow: hidden; box-shadow: 0 2px 4px rgba(0,0,0,0.1);"">
        <tr>
            <td style=""padding: 30px; text-align: center; background-color: #007bff;"">
                <h1 style=""color: #ffffff; margin: 0; font-size: 24px;"">Kultur Platform Freiburg</h1>
            </td>
        </tr>
        {(headerImageUrl != null ? $@"
        <tr>
            <td style=""padding: 0;"">
                <img src=""{headerImageUrl}"" alt=""Header"" style=""width: 100%; height: auto; display: block;"" />
            </td>
        </tr>" : "")}
        <tr>
            <td style=""padding: 40px 30px;"">
                <h2 style=""color: #333; margin-top: 0; font-size: 20px;"">{subject}</h2>
                <div style=""color: #666; line-height: 1.6; font-size: 16px;"">
                    {content}
                </div>
            </td>
        </tr>
        <tr>
            <td style=""padding: 30px; background-color: #f8f9fa; text-align: center;"">
                <p style=""color: #666; font-size: 14px; margin: 0 0 10px 0;"">
                    Kultur Platform Freiburg<br/>
                    <a href=""https://kulturplattformfreiburg.org"" style=""color: #007bff; text-decoration: none;"">kulturplattformfreiburg.org</a>
                </p>
                <p style=""color: #999; font-size: 12px; margin: 10px 0;"">
                    Sie erhalten diese E-Mail, weil Sie sich für unseren Newsletter angemeldet haben.
                </p>
                <p style=""margin: 10px 0;"">
                    <a href=""{unsubscribeUrl}"" style=""color: #dc3545; font-size: 12px; text-decoration: underline;"">
                        Abmelden / Unsubscribe
                    </a>
                </p>
            </td>
        </tr>
    </table>
    <table role=""presentation"" style=""width: 100%; max-width: 600px; margin: 0 auto;"">
        <tr>
            <td style=""padding: 20px; text-align: center;"">
                <p style=""color: #999; font-size: 11px; margin: 0;"">
                    © {DateTime.UtcNow.Year} Kultur Platform Freiburg. Alle Rechte vorbehalten.
                </p>
            </td>
        </tr>
    </table>
</body>
</html>";
        }
    }
}
