using KulturPlatform.Application.Dtos.Newsletter;
using KulturPlatform.Application.Interfaces.Newsletter;
using KulturPlatform.Application.Queries.Newsletter;
using MediatR;

namespace KulturPlatform.Application.Queries.Newsletter
{
    public class GetNewsletterSubscribersQueryHandler : IRequestHandler<GetNewsletterSubscribersQuery, List<NewsletterSubscriberDto>>
    {
        private readonly INewsletterSubscriberRepository _repository;

        public GetNewsletterSubscribersQueryHandler(INewsletterSubscriberRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<NewsletterSubscriberDto>> Handle(GetNewsletterSubscribersQuery request, CancellationToken cancellationToken)
        {
            var subscribers = await _repository.GetActiveSubscribersAsync(cancellationToken);
            
            return subscribers.Select(s => new NewsletterSubscriberDto
            {
                Id = s.Id,
                Email = s.Email.Value,
                FullName = s.FullName,
                IsActive = s.IsActive,
                IsVerified = s.IsVerified,
                SubscribedAt = s.SubscribedAt,
                VerifiedAt = s.VerifiedAt,
                Source = s.Source.ToString()
            }).ToList();
        }
    }

    public class GetNewsletterStatsQueryHandler : IRequestHandler<GetNewsletterStatsQuery, NewsletterStatsDto>
    {
        private readonly INewsletterSubscriberRepository _subscriberRepository;
        private readonly INewsletterCampaignRepository _campaignRepository;

        public GetNewsletterStatsQueryHandler(
            INewsletterSubscriberRepository subscriberRepository,
            INewsletterCampaignRepository campaignRepository)
        {
            _subscriberRepository = subscriberRepository;
            _campaignRepository = campaignRepository;
        }

        public async Task<NewsletterStatsDto> Handle(GetNewsletterStatsQuery request, CancellationToken cancellationToken)
        {
            var totalSubscribers = await _subscriberRepository.GetTotalSubscribersCountAsync(cancellationToken);
            var activeSubscribers = await _subscriberRepository.GetActiveSubscribersCountAsync(cancellationToken);
            var campaigns = await _campaignRepository.GetAllAsync(cancellationToken);
            
            var sentCampaigns = campaigns.Where(c => c.Status == Domain.Commons.Aggregates.CampaignStatus.Sent).ToList();

            return new NewsletterStatsDto
            {
                TotalSubscribers = totalSubscribers,
                ActiveSubscribers = activeSubscribers,
                VerifiedSubscribers = activeSubscribers,
                UnsubscribedCount = totalSubscribers - activeSubscribers,
                TotalCampaignsSent = sentCampaigns.Count,
                TotalEmailsSent = sentCampaigns.Sum(c => c.SuccessfulSends)
            };
        }
    }

    public class GetNewsletterCampaignsQueryHandler : IRequestHandler<GetNewsletterCampaignsQuery, List<NewsletterCampaignDto>>
    {
        private readonly INewsletterCampaignRepository _repository;

        public GetNewsletterCampaignsQueryHandler(INewsletterCampaignRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<NewsletterCampaignDto>> Handle(GetNewsletterCampaignsQuery request, CancellationToken cancellationToken)
        {
            var campaigns = await _repository.GetAllAsync(cancellationToken);
            
            return campaigns.Select(c => new NewsletterCampaignDto
            {
                Id = c.Id,
                Subject = c.Subject.Value,
                ContentTr = c.ContentTr.Value,
                ContentDe = c.ContentDe.Value,
                HeaderImageUrl = c.HeaderImageUrl,
                Status = c.Status.ToString(),
                CreatedAt = c.CreatedAt,
                ScheduledAt = c.ScheduledAt,
                SentAt = c.SentAt,
                TotalRecipients = c.TotalRecipients,
                SuccessfulSends = c.SuccessfulSends,
                FailedSends = c.FailedSends
            }).ToList();
        }
    }

    public class GetNewsletterCampaignByIdQueryHandler : IRequestHandler<GetNewsletterCampaignByIdQuery, NewsletterCampaignDto?>
    {
        private readonly INewsletterCampaignRepository _repository;

        public GetNewsletterCampaignByIdQueryHandler(INewsletterCampaignRepository repository)
        {
            _repository = repository;
        }

        public async Task<NewsletterCampaignDto?> Handle(GetNewsletterCampaignByIdQuery request, CancellationToken cancellationToken)
        {
            var campaign = await _repository.GetByIdAsync(request.Id, cancellationToken);
            
            if (campaign == null)
                return null;

            return new NewsletterCampaignDto
            {
                Id = campaign.Id,
                Subject = campaign.Subject.Value,
                ContentTr = campaign.ContentTr.Value,
                ContentDe = campaign.ContentDe.Value,
                HeaderImageUrl = campaign.HeaderImageUrl,
                Status = campaign.Status.ToString(),
                CreatedAt = campaign.CreatedAt,
                ScheduledAt = campaign.ScheduledAt,
                SentAt = campaign.SentAt,
                TotalRecipients = campaign.TotalRecipients,
                SuccessfulSends = campaign.SuccessfulSends,
                FailedSends = campaign.FailedSends
            };
        }
    }
}
