namespace KulturPlatform.Application.Interfaces
{
    public interface IEmailService
    {
        /// <summary>
        /// Sends a contact message notification email with anti-spam protection
        /// </summary>
        Task SendContactMessageNotificationAsync(
            string senderName,
            string senderEmail,
            string? senderPhone,
            string subject,
            string message,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Sends a volunteer submission notification email with anti-spam protection
        /// </summary>
        Task SendVolunteerSubmissionNotificationAsync(
            string fullName,
            string email,
            string? phoneNumber,
            string message,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Checks if an email address or IP has exceeded rate limits
        /// </summary>
        Task<bool> IsRateLimitExceededAsync(string identifier);

        /// <summary>
        /// Sanitizes HTML content to prevent XSS and spam injection
        /// </summary>
        string SanitizeHtmlContent(string content);
    }
}
