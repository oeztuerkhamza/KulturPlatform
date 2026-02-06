namespace KulturPlatform.Application.Interfaces.Newsletter
{
    public interface INewsletterService
    {
        /// <summary>
        /// Subscribe a new email (with double opt-in)
        /// </summary>
        Task<bool> SubscribeAsync(string email, string? fullName, CancellationToken cancellationToken = default);

        /// <summary>
        /// Verify email subscription (double opt-in confirmation)
        /// </summary>
        Task<bool> VerifySubscriptionAsync(string token, CancellationToken cancellationToken = default);

        /// <summary>
        /// Unsubscribe from newsletter
        /// </summary>
        Task<bool> UnsubscribeAsync(string token, CancellationToken cancellationToken = default);

        /// <summary>
        /// Send newsletter campaign to all verified subscribers
        /// </summary>
        Task<(int successful, int failed)> SendCampaignAsync(Guid campaignId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Send test email to specific address
        /// </summary>
        Task SendTestEmailAsync(Guid campaignId, string testEmail, CancellationToken cancellationToken = default);
    }
}
