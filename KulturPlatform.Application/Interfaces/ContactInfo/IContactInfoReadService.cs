using KulturPlatform.Application.Dtos;

namespace KulturPlatform.Application.Interfaces.ContactInfo
{
    public interface IContactInfoReadService
    {
        /// <summary>
        /// Get the contact info (should be only one record in database)
        /// </summary>
        Task<Domain.Commons.Aggregates.ContactInfo?> GetContactInfoAsync(CancellationToken cancellationToken = default);
        
        Task<Domain.Commons.Aggregates.ContactInfo?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        
        Task<IEnumerable<Domain.Commons.Aggregates.ContactInfo>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}
