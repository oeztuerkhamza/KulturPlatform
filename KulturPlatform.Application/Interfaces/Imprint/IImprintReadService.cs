namespace KulturPlatform.Application.Interfaces.Imprint
{
    public interface IImprintReadService
    {
        /// <summary>
        /// Get all Imprint records (should be only one in singleton pattern)
        /// </summary>
        Task<IEnumerable<Domain.Commons.Aggregates.Imprint>> GetAllAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Get Imprint by ID
        /// </summary>
        Task<Domain.Commons.Aggregates.Imprint?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get the single Imprint record (singleton pattern)
        /// </summary>
        Task<Domain.Commons.Aggregates.Imprint?> GetSingleAsync(CancellationToken cancellationToken = default);
    }
}
