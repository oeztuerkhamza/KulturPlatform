using MediatR;

namespace KulturPlatform.Application.Commands.Newsletter
{
    public class CreateNewsletterCampaignCommand : IRequest<Guid>
    {
        public string Subject { get; set; } = string.Empty;
        public string ContentTr { get; set; } = string.Empty;
        public string ContentDe { get; set; } = string.Empty;
        public string? HeaderImageUrl { get; set; }
        public DateTime? ScheduledAt { get; set; }
    }

    public class UpdateNewsletterCampaignCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string ContentTr { get; set; } = string.Empty;
        public string ContentDe { get; set; } = string.Empty;
        public string? HeaderImageUrl { get; set; }
    }

    public class SendNewsletterCampaignCommand : IRequest<(int successful, int failed)>
    {
        public Guid CampaignId { get; set; }
    }

    public class SendTestNewsletterCommand : IRequest
    {
        public Guid CampaignId { get; set; }
        public string TestEmail { get; set; } = string.Empty;
    }

    public class DeleteNewsletterCampaignCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
