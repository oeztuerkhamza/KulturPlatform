using KulturPlatform.Application.Dtos.SatzungDto;

namespace KulturPlatform.Application.Interfaces.Satzung
{
    public interface ISatzungReadService
    {
        Task<IEnumerable<SatzungDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<SatzungDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        // ✅ Key tabanlı erişim
        Task<SatzungDto?> GetByKeyAsync(string key, CancellationToken cancellationToken = default);
    }

}
