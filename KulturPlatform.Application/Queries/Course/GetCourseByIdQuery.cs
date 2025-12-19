using KulturPlatform.Application.Dtos;
using MediatR;

namespace KulturPlatform.Application.Queries.Course
{
    public record GetCourseByIdQuery(Guid Id) : IRequest<CourseDto?>;
}
