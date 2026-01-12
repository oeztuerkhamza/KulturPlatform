using KulturPlatform.Domain.Commons.AggregateRoot;
using KulturPlatform.Domain.Interfaces;

namespace KulturPlatform.Application.Interfaces.Home
{
    public interface ICtaSectionRepository : IRepository<CtaSection>
    {
        Task<CtaSection?> GetActiveAsync(CancellationToken cancellationToken = default);
    }
}
