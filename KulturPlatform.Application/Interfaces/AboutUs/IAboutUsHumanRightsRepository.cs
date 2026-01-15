namespace KulturPlatform.Application.Interfaces.AboutUs;

public interface IAboutUsHumanRightsRepository
{
    Task<Domain.Commons.Aggregates.AboutUsHumanRights?> GetAsync(CancellationToken ct);
    Task<Domain.Commons.Aggregates.AboutUsHumanRights?> GetByIdAsync(Guid id, CancellationToken ct);
    Task AddAsync(Domain.Commons.Aggregates.AboutUsHumanRights humanRights, CancellationToken ct);
    Task UpdateAsync(Domain.Commons.Aggregates.AboutUsHumanRights humanRights, CancellationToken ct);
    Task DeleteAsync(Guid id, CancellationToken ct);
}
