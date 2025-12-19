namespace KulturPlatform.Application.Interfaces.ValueItem
{
    public interface IValueItemWriteRepository
    {
        Task<Domain.Commons.Aggregates.ValueItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task AddAsync(Domain.Commons.Aggregates.ValueItem valueItem, CancellationToken cancellationToken);
        Task UpdateAsync(Domain.Commons.Aggregates.ValueItem valueItem, CancellationToken cancellationToken);
    }
}
