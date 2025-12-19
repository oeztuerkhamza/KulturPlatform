using KulturPlatform.Application.Dtos;

namespace KulturPlatform.Application.Interfaces.Partner
{
    public interface IPartnerReadService
    {
        Task<IEnumerable<PartnerDto>> GetAllAsync();
        Task<PartnerDto?> GetByIdAsync(Guid id);
    }
}
