namespace KulturPlatform.Application.Interfaces.AboutUs;

public interface IAboutUsWhoWeAreRepository
{
    Task<Domain.Commons.Aggregates.AboutUsWhoWeAre?> GetAsync(CancellationToken ct);
    Task<Domain.Commons.Aggregates.AboutUsWhoWeAre?> GetByIdAsync(Guid id, CancellationToken ct);
    Task AddAsync(Domain.Commons.Aggregates.AboutUsWhoWeAre whoWeAre, CancellationToken ct);
    Task UpdateAsync(Domain.Commons.Aggregates.AboutUsWhoWeAre whoWeAre, CancellationToken ct);
    Task DeleteAsync(Guid id, CancellationToken ct);
}
