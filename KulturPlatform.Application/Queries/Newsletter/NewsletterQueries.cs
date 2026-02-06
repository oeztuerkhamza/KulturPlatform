using KulturPlatform.Application.Dtos.Newsletter;
using MediatR;

namespace KulturPlatform.Application.Queries.Newsletter
{
    public class GetNewsletterSubscribersQuery : IRequest<List<NewsletterSubscriberDto>>
    {
    }

    public class GetNewsletterStatsQuery : IRequest<NewsletterStatsDto>
    {
    }

    public class GetNewsletterCampaignsQuery : IRequest<List<NewsletterCampaignDto>>
    {
    }

    public class GetNewsletterCampaignByIdQuery : IRequest<NewsletterCampaignDto?>
    {
        public Guid Id { get; set; }
    }
}
