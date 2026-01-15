namespace KulturPlatform.Application.Interfaces.AboutUs;

public interface IAboutUsGoalsRepository
{
    Task<Domain.Commons.Aggregates.AboutUsGoals?> GetAsync(CancellationToken ct);
    Task<Domain.Commons.Aggregates.AboutUsGoals?> GetByIdAsync(Guid id, CancellationToken ct);
    Task AddAsync(Domain.Commons.Aggregates.AboutUsGoals goals, CancellationToken ct);
    Task UpdateAsync(Domain.Commons.Aggregates.AboutUsGoals goals, CancellationToken ct);
    Task DeleteAsync(Guid id, CancellationToken ct);
}
