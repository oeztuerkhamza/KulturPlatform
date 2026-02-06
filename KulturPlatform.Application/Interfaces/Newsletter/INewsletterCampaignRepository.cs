using KulturPlatform.Domain.Commons.Aggregates;

namespace KulturPlatform.Application.Interfaces.Newsletter
{
    public interface INewsletterCampaignRepository
    {
        Task<NewsletterCampaign?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<List<NewsletterCampaign>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<List<NewsletterCampaign>> GetScheduledCampaignsAsync(CancellationToken cancellationToken = default);
        Task AddAsync(NewsletterCampaign campaign, CancellationToken cancellationToken = default);
        Task UpdateAsync(NewsletterCampaign campaign, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
