using MediatR;

namespace KulturPlatform.Application.Commands.Course
{
    public record DeleteCourseCommand(Guid Id) : IRequest;
}
