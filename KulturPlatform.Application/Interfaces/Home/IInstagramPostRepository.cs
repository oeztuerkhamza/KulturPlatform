using KulturPlatform.Domain.Commons.AggregateRoot;
using KulturPlatform.Domain.Interfaces;

namespace KulturPlatform.Application.Interfaces.Home
{
    public interface IInstagramPostRepository : IRepository<InstagramPost>
    {
        Task<List<InstagramPost>> GetRecentAsync(int count = 6, CancellationToken cancellationToken = default);
    }
}
