using KulturPlatform.Application.Dtos.Activity;
using KulturPlatform.Application.Dtos.Common;

namespace KulturPlatform.Application.Interfaces.Activity
{
    public interface IActivityReadService
    {
        Task<IEnumerable<ActivityDto>> GetAllAsync();
        Task<PagedResult<ActivityDto>> GetAllPagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
        Task<IEnumerable<ActivityDto>> GetUpcomingAsync(CancellationToken cancellationToken);
        Task<ActivityDto?> GetByIdAsync(Guid id);
    }
}
