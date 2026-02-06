using KulturPlatform.Application.Interfaces.Newsletter;
using KulturPlatform.Domain.Commons.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace KulturPlatform.Infrastructure.Repositories
{
    public class NewsletterSubscriberRepository : INewsletterSubscriberRepository
    {
        private readonly AppDbContext _context;

        public NewsletterSubscriberRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<NewsletterSubscriber?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Set<NewsletterSubscriber>()
                .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        }

        public async Task<NewsletterSubscriber?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return await _context.Set<NewsletterSubscriber>()
                .FirstOrDefaultAsync(s => s.Email.Value == email, cancellationToken);
        }

        public async Task<NewsletterSubscriber?> GetByTokenAsync(string token, CancellationToken cancellationToken = default)
        {
            return await _context.Set<NewsletterSubscriber>()
                .FirstOrDefaultAsync(s => s.UnsubscribeToken == token, cancellationToken);
        }

        public async Task<NewsletterSubscriber?> GetByVerificationTokenAsync(string token, CancellationToken cancellationToken = default)
        {
            return await _context.Set<NewsletterSubscriber>()
                .FirstOrDefaultAsync(s => s.VerificationToken == token, cancellationToken);
        }

        public async Task<List<NewsletterSubscriber>> GetActiveSubscribersAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Set<NewsletterSubscriber>()
                .Where(s => s.IsActive)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<NewsletterSubscriber>> GetVerifiedSubscribersAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Set<NewsletterSubscriber>()
                .Where(s => s.IsActive && s.IsVerified)
                .ToListAsync(cancellationToken);
        }

        public async Task AddAsync(NewsletterSubscriber subscriber, CancellationToken cancellationToken = default)
        {
            await _context.Set<NewsletterSubscriber>().AddAsync(subscriber, cancellationToken);
        }

        public Task UpdateAsync(NewsletterSubscriber subscriber, CancellationToken cancellationToken = default)
        {
            _context.Set<NewsletterSubscriber>().Update(subscriber);
            return Task.CompletedTask;
        }

        public async Task<int> GetTotalSubscribersCountAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Set<NewsletterSubscriber>()
                .CountAsync(cancellationToken);
        }

        public async Task<int> GetActiveSubscribersCountAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Set<NewsletterSubscriber>()
                .CountAsync(s => s.IsActive && s.IsVerified, cancellationToken);
        }
    }
}
