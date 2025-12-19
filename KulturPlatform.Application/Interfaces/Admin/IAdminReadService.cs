using KulturPlatform.Domain.Commons.Aggregates;

namespace KulturPlatform.Application.Interfaces.Admin
{
    public interface IAdminReadService
    {
        Task<Domain.Commons.Aggregates.Admin?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Domain.Commons.Aggregates.Admin>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Domain.Commons.Aggregates.Admin?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    }
}
