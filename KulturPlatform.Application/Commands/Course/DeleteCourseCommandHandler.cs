using KulturPlatform.Application.Interfaces.Course;
using MediatR;

namespace KulturPlatform.Application.Commands.Course
{
    public class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommand>
    {
        private readonly ICourseRepository _courseRepository;

        public DeleteCourseCommandHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.GetByIdAsync(request.Id, cancellationToken);
            if (course == null)
                throw new KeyNotFoundException($"Course with Id {request.Id} not found.");

            _courseRepository.Delete(course, cancellationToken);
        }
    }
}
