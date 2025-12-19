using KulturPlatform.Application.Dtos;

namespace KulturPlatform.Application.Interfaces.Activity
{
    public interface IActivityReadService
    {
        Task<IEnumerable<ActivityDto>> GetAllAsync();
        Task<IEnumerable<ActivityDto>> GetUpcomingAsync();
    }

}
