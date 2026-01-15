namespace KulturPlatform.Application.Interfaces.AboutUs;

public interface IActivityAreaRepository
{
    Task<List<Domain.Commons.Entities.ActivityArea>> GetAllAsync(CancellationToken ct);
    Task<Domain.Commons.Entities.ActivityArea?> GetByIdAsync(Guid id, CancellationToken ct);
    Task AddAsync(Domain.Commons.Entities.ActivityArea activityArea, CancellationToken ct);
    Task UpdateAsync(Domain.Commons.Entities.ActivityArea activityArea, CancellationToken ct);
    Task DeleteAsync(Guid id, CancellationToken ct);
}
