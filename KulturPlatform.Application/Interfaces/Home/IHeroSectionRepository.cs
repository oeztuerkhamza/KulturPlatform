using KulturPlatform.Domain.Commons.AggregateRoot;
using KulturPlatform.Domain.Interfaces;

namespace KulturPlatform.Application.Interfaces.Home
{
    public interface IHeroSectionRepository : IRepository<HeroSection>
    {
        Task<HeroSection?> GetActiveAsync(CancellationToken cancellationToken = default);
    }
}
