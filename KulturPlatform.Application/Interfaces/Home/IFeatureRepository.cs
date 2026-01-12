using KulturPlatform.Domain.Commons.AggregateRoot;
using KulturPlatform.Domain.Interfaces;

namespace KulturPlatform.Application.Interfaces.Home
{
    public interface IFeatureRepository : IRepository<Feature>
    {
        Task<List<Feature>> GetAllActiveAsync(CancellationToken cancellationToken = default);
    }
}
