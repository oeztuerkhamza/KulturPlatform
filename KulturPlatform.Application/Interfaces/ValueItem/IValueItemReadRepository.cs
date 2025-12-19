using KulturPlatform.Application.Dtos.NewFolder;

namespace KulturPlatform.Application.Interfaces.ValueItem
{
    public interface IValueItemReadRepository
    {
        // Listeleme için
        Task<IReadOnlyList<ValueItemDetailDto>> GetAllAsync(CancellationToken cancellationToken);

        // Detay için
        Task<ValueItemDetailDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
