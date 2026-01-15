namespace KulturPlatform.Application.Interfaces.AboutUs;

public interface IAboutUsMissionRepository
{
    Task<Domain.Commons.Aggregates.AboutUsMission?> GetAsync(CancellationToken ct);
    Task<Domain.Commons.Aggregates.AboutUsMission?> GetByIdAsync(Guid id, CancellationToken ct);
    Task AddAsync(Domain.Commons.Aggregates.AboutUsMission mission, CancellationToken ct);
    Task UpdateAsync(Domain.Commons.Aggregates.AboutUsMission mission, CancellationToken ct);
    Task DeleteAsync(Guid id, CancellationToken ct);
}
