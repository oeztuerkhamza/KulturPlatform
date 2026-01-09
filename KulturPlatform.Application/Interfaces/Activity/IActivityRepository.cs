namespace KulturPlatform.Application.Interfaces.Activity
{
    public interface IActivityRepository
    {
        Task AddAsync(Domain.Commons.AggregateRoot.Activity activity, CancellationToken cancellationToken = default);
        Task UpdateAsync(Domain.Commons.AggregateRoot.Activity activity, CancellationToken cancellationToken = default);
        Task DeleteAsync(Domain.Commons.AggregateRoot.Activity activity, CancellationToken cancellationToken = default);
        Task<Domain.Commons.AggregateRoot.Activity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
