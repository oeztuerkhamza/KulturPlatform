namespace KulturPlatform.Application.Dtos.Newsletter
{
    public class NewsletterSubscriberDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string? FullName { get; set; }
        public bool IsActive { get; set; }
        public bool IsVerified { get; set; }
        public DateTime SubscribedAt { get; set; }
        public DateTime? VerifiedAt { get; set; }
        public string Source { get; set; } = string.Empty;
    }

    public class NewsletterCampaignDto
    {
        public Guid Id { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string ContentTr { get; set; } = string.Empty;
        public string ContentDe { get; set; } = string.Empty;
        public string? HeaderImageUrl { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? ScheduledAt { get; set; }
        public DateTime? SentAt { get; set; }
        public int TotalRecipients { get; set; }
        public int SuccessfulSends { get; set; }
        public int FailedSends { get; set; }
    }

    public class NewsletterStatsDto
    {
        public int TotalSubscribers { get; set; }
        public int ActiveSubscribers { get; set; }
        public int VerifiedSubscribers { get; set; }
        public int UnsubscribedCount { get; set; }
        public int TotalCampaignsSent { get; set; }
        public int TotalEmailsSent { get; set; }
    }
}
