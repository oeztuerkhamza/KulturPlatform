using KulturPlatform.Domain.Commons.Aggregates;

namespace KulturPlatform.Application.Interfaces.Newsletter
{
    public interface INewsletterSubscriberRepository
    {
        Task<NewsletterSubscriber?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<NewsletterSubscriber?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
        Task<NewsletterSubscriber?> GetByTokenAsync(string token, CancellationToken cancellationToken = default);
        Task<NewsletterSubscriber?> GetByVerificationTokenAsync(string token, CancellationToken cancellationToken = default);
        Task<List<NewsletterSubscriber>> GetActiveSubscribersAsync(CancellationToken cancellationToken = default);
        Task<List<NewsletterSubscriber>> GetVerifiedSubscribersAsync(CancellationToken cancellationToken = default);
        Task AddAsync(NewsletterSubscriber subscriber, CancellationToken cancellationToken = default);
        Task UpdateAsync(NewsletterSubscriber subscriber, CancellationToken cancellationToken = default);
        Task<int> GetTotalSubscribersCountAsync(CancellationToken cancellationToken = default);
        Task<int> GetActiveSubscribersCountAsync(CancellationToken cancellationToken = default);
    }
}
