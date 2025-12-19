using KulturPlatform.Application.Dtos;
using KulturPlatform.Application.Interfaces.Course;
using Microsoft.EntityFrameworkCore;

namespace KulturPlatform.Infrastructure.ReadServices
{
    public class CourseReadService : ICourseReadService
    {
        private readonly AppDbContext _context;

        public CourseReadService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CourseDto>> GetAllAsync()
        {
            return await _context.Courses
                .AsNoTracking()
                .Select(c => new CourseDto
                {
                    Id = c.Id,
                    TitleTr = c.TitleTr.Value,
                    TitleDe = c.TitleDe.Value,
                    DescriptionTr = c.DescriptionTr.Value,
                    DescriptionDe = c.DescriptionDe.Value,
                    DetailsTr = c.DetailsTr != null ? c.DetailsTr.Value : null,
                    DetailsDe = c.DetailsDe != null ? c.DetailsDe.Value : null,
                    ScheduleTr = c.ScheduleTr != null ? c.ScheduleTr.Value : null,
                    ScheduleDe = c.ScheduleDe != null ? c.ScheduleDe.Value : null,
                    Icon = c.Icon,
                    Instructor = c.Instructor != null ? c.Instructor.Value : null,
                    Date = c.Date,
                    CourseLocation = c.CourseLocation != null ? c.CourseLocation.Street : null,
                    CourseCategory = c.CourseCategory != null ? c.CourseCategory.Value : null,
                    IsActive = c.IsActive,
                    CreatedAt = c.CreatedAt,
                    UpdatedAt = c.UpdatedAt
                })
                .ToListAsync();
        }

        public async Task<CourseDto?> GetByIdAsync(Guid id)
        {
            return await _context.Courses
                .AsNoTracking()
                .Where(c => c.Id == id)
                .Select(c => new CourseDto
                {
                    Id = c.Id,
                    TitleTr = c.TitleTr.Value,
                    TitleDe = c.TitleDe.Value,
                    DescriptionTr = c.DescriptionTr.Value,
                    DescriptionDe = c.DescriptionDe.Value,
                    DetailsTr = c.DetailsTr != null ? c.DetailsTr.Value : null,
                    DetailsDe = c.DetailsDe != null ? c.DetailsDe.Value : null,
                    ScheduleTr = c.ScheduleTr != null ? c.ScheduleTr.Value : null,
                    ScheduleDe = c.ScheduleDe != null ? c.ScheduleDe.Value : null,
                    Icon = c.Icon,
                    Instructor = c.Instructor != null ? c.Instructor.Value : null,
                    Date = c.Date,
                    CourseLocation = c.CourseLocation != null ? c.CourseLocation.Street : null,
                    CourseCategory = c.CourseCategory != null ? c.CourseCategory.Value : null,
                    IsActive = c.IsActive,
                    CreatedAt = c.CreatedAt,
                    UpdatedAt = c.UpdatedAt
                })
                .FirstOrDefaultAsync();
        }
    }
}
