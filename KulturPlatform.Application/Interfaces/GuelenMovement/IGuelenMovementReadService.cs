using KulturPlatform.Application.Dtos;

namespace KulturPlatform.Application.Interfaces.GuelenMovement
{
    public interface IGuelenMovementReadService
    {
        Task<IEnumerable<GuelenMovementDto>> GetAllAsync();
    }
}
