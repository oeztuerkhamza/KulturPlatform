namespace KulturPlatform.Application.Interfaces.TeaEvent
{
    public interface ITeaEventWriteRepository
    {
        Task<Domain.Commons.Aggregates.TeaEvent?> GetByIdForUpdateAsync(Guid id, CancellationToken cancellationToken);
        
        Task AddAsync(Domain.Commons.Aggregates.TeaEvent teaEvent, CancellationToken cancellationToken);

        Task UpdateAsync(Domain.Commons.Aggregates.TeaEvent teaEvent, CancellationToken cancellationToken);

        Task DeleteAsync(Domain.Commons.Aggregates.TeaEvent teaEvent, CancellationToken cancellationToken);
    }
}
