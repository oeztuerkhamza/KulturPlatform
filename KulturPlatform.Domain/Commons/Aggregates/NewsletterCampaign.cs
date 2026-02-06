using KulturPlatform.Domain.Commons.ValueObjects;

namespace KulturPlatform.Domain.Commons.Aggregates
{
    public class NewsletterCampaign
    {
        public Guid Id { get; private set; }
        public Title Subject { get; private set; }
        public Description ContentTr { get; private set; }
        public Description ContentDe { get; private set; }
        public string? HeaderImageUrl { get; private set; }
        public CampaignStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? ScheduledAt { get; private set; }
        public DateTime? SentAt { get; private set; }
        public int TotalRecipients { get; private set; }
        public int SuccessfulSends { get; private set; }
        public int FailedSends { get; private set; }
        public Guid CreatedBy { get; private set; } // Admin user ID

        // EF Core constructor
        private NewsletterCampaign() { }

        public static NewsletterCampaign Create(
            Title subject,
            Description contentTr,
            Description contentDe,
            Guid createdBy,
            string? headerImageUrl = null,
            DateTime? scheduledAt = null)
        {
            return new NewsletterCampaign
            {
                Id = Guid.NewGuid(),
                Subject = subject,
                ContentTr = contentTr,
                ContentDe = contentDe,
                HeaderImageUrl = headerImageUrl,
                Status = CampaignStatus.Draft,
                CreatedAt = DateTime.UtcNow,
                ScheduledAt = scheduledAt,
                CreatedBy = createdBy
            };
        }

        public void Schedule(DateTime scheduledAt)
        {
            if (Status != CampaignStatus.Draft)
                throw new InvalidOperationException("Only draft campaigns can be scheduled");

            if (scheduledAt <= DateTime.UtcNow)
                throw new ArgumentException("Scheduled date must be in the future");

            ScheduledAt = scheduledAt;
            Status = CampaignStatus.Scheduled;
        }

        public void MarkAsSending(int totalRecipients)
        {
            if (Status != CampaignStatus.Draft && Status != CampaignStatus.Scheduled)
                throw new InvalidOperationException("Campaign is not ready to send");

            Status = CampaignStatus.Sending;
            TotalRecipients = totalRecipients;
        }

        public void MarkAsSent(int successful, int failed)
        {
            if (Status != CampaignStatus.Sending)
                throw new InvalidOperationException("Campaign is not in sending status");

            Status = CampaignStatus.Sent;
            SentAt = DateTime.UtcNow;
            SuccessfulSends = successful;
            FailedSends = failed;
        }

        public void MarkAsFailed(string reason)
        {
            Status = CampaignStatus.Failed;
        }

        public void Update(Title subject, Description contentTr, Description contentDe, string? headerImageUrl)
        {
            if (Status != CampaignStatus.Draft)
                throw new InvalidOperationException("Only draft campaigns can be updated");

            Subject = subject;
            ContentTr = contentTr;
            ContentDe = contentDe;
            HeaderImageUrl = headerImageUrl;
        }
    }

    public enum CampaignStatus
    {
        Draft = 1,
        Scheduled = 2,
        Sending = 3,
        Sent = 4,
        Failed = 5
    }
}
