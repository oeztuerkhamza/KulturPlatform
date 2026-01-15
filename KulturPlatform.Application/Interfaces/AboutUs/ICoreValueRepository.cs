namespace KulturPlatform.Application.Interfaces.AboutUs;

public interface ICoreValueRepository
{
    Task<List<Domain.Commons.Entities.CoreValue>> GetAllAsync(CancellationToken ct);
    Task<Domain.Commons.Entities.CoreValue?> GetByIdAsync(Guid id, CancellationToken ct);
    Task AddAsync(Domain.Commons.Entities.CoreValue coreValue, CancellationToken ct);
    Task UpdateAsync(Domain.Commons.Entities.CoreValue coreValue, CancellationToken ct);
    Task DeleteAsync(Guid id, CancellationToken ct);
}
