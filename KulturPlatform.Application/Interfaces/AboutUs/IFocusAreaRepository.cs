namespace KulturPlatform.Application.Interfaces.AboutUs;

public interface IFocusAreaRepository
{
    Task<List<Domain.Commons.Entities.FocusArea>> GetAllAsync(CancellationToken ct);
    Task<Domain.Commons.Entities.FocusArea?> GetByIdAsync(Guid id, CancellationToken ct);
    Task AddAsync(Domain.Commons.Entities.FocusArea focusArea, CancellationToken ct);
    Task UpdateAsync(Domain.Commons.Entities.FocusArea focusArea, CancellationToken ct);
    Task DeleteAsync(Guid id, CancellationToken ct);
}
