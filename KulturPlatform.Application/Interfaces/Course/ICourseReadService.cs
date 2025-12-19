using KulturPlatform.Application.Dtos;

namespace KulturPlatform.Application.Interfaces.Course
{
    public interface ICourseReadService
    {
        Task<IEnumerable<CourseDto>> GetAllAsync();
        Task<CourseDto?> GetByIdAsync(Guid id);
    }
}
