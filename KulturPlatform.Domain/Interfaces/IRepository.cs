namespace KulturPlatform.Domain.Interfaces
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task AddAsync(T entity, CancellationToken cancellationToken);
        void Update(T entity, CancellationToken cancellationToken);
        void Delete(T entity, CancellationToken cancellationToken);
    }


}
