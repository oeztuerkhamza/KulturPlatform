namespace KulturPlatform.Application.Interfaces.AboutUs;

public interface IAboutUsVisionRepository
{
    Task<Domain.Commons.Aggregates.AboutUsVision?> GetAsync(CancellationToken ct);
    Task<Domain.Commons.Aggregates.AboutUsVision?> GetByIdAsync(Guid id, CancellationToken ct);
    Task AddAsync(Domain.Commons.Aggregates.AboutUsVision vision, CancellationToken ct);
    Task UpdateAsync(Domain.Commons.Aggregates.AboutUsVision vision, CancellationToken ct);
    Task DeleteAsync(Guid id, CancellationToken ct);
}
