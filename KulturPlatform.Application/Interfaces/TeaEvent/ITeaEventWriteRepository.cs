namespace KulturPlatform.Application.Interfaces.TeaEvent
{
    public interface ITeaEventWriteRepository
    {
        Task AddAsync(Domain.Commons.Aggregates.TeaEvent teaEvent, CancellationToken cancellationToken);

        Task UpdateAsync(Domain.Commons.Aggregates.TeaEvent teaEvent, CancellationToken cancellationToken);

        Task DeleteAsync(Domain.Commons.Aggregates.TeaEvent teaEvent, CancellationToken cancellationToken);
    }
}
