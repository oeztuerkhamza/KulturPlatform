namespace KulturPlatform.Application.Dtos.ContactMessages
{
    public class ContactMessageDto
    {
        public Guid Id { get; set; }
        public string? Anrede { get; set; }
        public string SenderName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime SubmittedAt { get; set; }
        public bool IsRead { get; set; }
        public DateTime? ReadAt { get; set; }
    }
}
