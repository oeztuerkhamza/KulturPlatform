using KulturPlatform.Application.Dtos;
using KulturPlatform.Application.Interfaces.Course;
using MediatR;

namespace KulturPlatform.Application.Queries.Course
{
    public class GetAllCoursesQueryHandler : IRequestHandler<GetAllCoursesQuery, IEnumerable<CourseDto>>
    {
        private readonly ICourseReadService _courseReadService;

        public GetAllCoursesQueryHandler(ICourseReadService courseReadService)
        {
            _courseReadService = courseReadService;
        }

        public async Task<IEnumerable<CourseDto>> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
        {
            return await _courseReadService.GetAllAsync();
        }
    }
}
