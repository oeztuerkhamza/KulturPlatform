using KulturPlatform.Application.Interfaces.Newsletter;
using KulturPlatform.Domain.Commons.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace KulturPlatform.Infrastructure.Repositories
{
    public class NewsletterCampaignRepository : INewsletterCampaignRepository
    {
        private readonly AppDbContext _context;

        public NewsletterCampaignRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<NewsletterCampaign?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Set<NewsletterCampaign>()
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public async Task<List<NewsletterCampaign>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Set<NewsletterCampaign>()
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<NewsletterCampaign>> GetScheduledCampaignsAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Set<NewsletterCampaign>()
                .Where(c => c.Status == CampaignStatus.Scheduled && c.ScheduledAt <= DateTime.UtcNow)
                .ToListAsync(cancellationToken);
        }

        public async Task AddAsync(NewsletterCampaign campaign, CancellationToken cancellationToken = default)
        {
            await _context.Set<NewsletterCampaign>().AddAsync(campaign, cancellationToken);
        }

        public Task UpdateAsync(NewsletterCampaign campaign, CancellationToken cancellationToken = default)
        {
            _context.Set<NewsletterCampaign>().Update(campaign);
            return Task.CompletedTask;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var campaign = await GetByIdAsync(id, cancellationToken);
            if (campaign != null)
            {
                _context.Set<NewsletterCampaign>().Remove(campaign);
            }
        }
    }
}
