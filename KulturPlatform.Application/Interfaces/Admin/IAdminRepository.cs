using KulturPlatform.Domain.Interfaces;

namespace KulturPlatform.Application.Interfaces.Admin
{
    public interface IAdminRepository : IRepository<Domain.Commons.Aggregates.Admin>
    {
        Task<Domain.Commons.Aggregates.Admin?> GetByEmailAsync(string email, CancellationToken cancellationToken);
    }
}
