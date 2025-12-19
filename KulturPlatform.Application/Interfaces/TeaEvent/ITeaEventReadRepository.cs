namespace KulturPlatform.Application.Interfaces.TeaEvent
{
    public interface ITeaEventReadRepository
    {
        Task<Domain.Commons.Aggregates.TeaEvent?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<Domain.Commons.Aggregates.TeaEvent?> GetActiveAsync(CancellationToken cancellationToken);

        Task<IReadOnlyList<Domain.Commons.Aggregates.TeaEvent>> GetAllAsync(CancellationToken cancellationToken);
    }
}
