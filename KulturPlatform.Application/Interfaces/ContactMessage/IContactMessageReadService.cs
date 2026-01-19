using KulturPlatform.Application.Dtos.ContactMessages;

namespace KulturPlatform.Application.Interfaces.ContactMessages
{
    public interface IContactMessageReadService
    {
        Task<IEnumerable<ContactMessageDto>> GetAllAsync(CancellationToken cancellationToken);
        Task<ContactMessageDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<ContactMessageDto>> GetUnreadAsync(CancellationToken cancellationToken);
        Task<int> GetUnreadCountAsync(CancellationToken cancellationToken);
    }
}
