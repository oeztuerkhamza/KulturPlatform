using KulturPlatform.Application.Interfaces.Course;
using KulturPlatform.Domain.Commons.Aggregates;

namespace KulturPlatform.Infrastructure.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly AppDbContext _context;

        public CourseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Course?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Courses.FindAsync([id], cancellationToken);
        }

        public async Task AddAsync(Course course, CancellationToken cancellationToken)
        {
            await _context.Courses.AddAsync(course, cancellationToken);
        }

        public void Update(Course course, CancellationToken cancellationToken)
        {
            _context.Courses.Update(course);
        }

        public void Delete(Course course, CancellationToken cancellationToken)
        {
            _context.Courses.Remove(course);
        }
    }
}
