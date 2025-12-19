using KulturPlatform.Application.Dtos;
using MediatR;

namespace KulturPlatform.Application.Queries.Course
{
    public record GetAllCoursesQuery() : IRequest<IEnumerable<CourseDto>>;
}
