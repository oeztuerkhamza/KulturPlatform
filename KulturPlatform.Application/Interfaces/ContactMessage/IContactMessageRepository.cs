using KulturPlatform.Domain.Commons.Aggregates;

namespace KulturPlatform.Application.Interfaces.ContactMessages
{
    public interface IContactMessageRepository
    {
        Task<ContactMessage?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task AddAsync(ContactMessage message, CancellationToken cancellationToken);
        void Update(ContactMessage message);
        void Delete(ContactMessage message);
    }
}
