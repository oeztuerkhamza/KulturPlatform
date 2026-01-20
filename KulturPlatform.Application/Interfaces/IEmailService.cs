namespace KulturPlatform.Application.Interfaces
{
    public interface IEmailService
    {
        /// <summary>
        /// Sends a contact message notification email
        /// </summary>
        Task SendContactMessageNotificationAsync(
            string senderName,
            string senderEmail,
            string? senderPhone,
            string subject,
            string message,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Sends a volunteer submission notification email
        /// </summary>
        Task SendVolunteerSubmissionNotificationAsync(
            string fullName,
            string email,
            string? phoneNumber,
            string message,
            CancellationToken cancellationToken = default);
    }
}
