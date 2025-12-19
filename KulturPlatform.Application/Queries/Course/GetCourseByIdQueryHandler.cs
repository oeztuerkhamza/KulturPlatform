using AutoMapper;
using KulturPlatform.Application.Dtos;
using KulturPlatform.Application.Interfaces.Course;
using MediatR;

namespace KulturPlatform.Application.Queries.Course
{
    public class GetCourseByIdQueryHandler : IRequestHandler<GetCourseByIdQuery, CourseDto?>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public GetCourseByIdQueryHandler(ICourseRepository courseRepository, IMapper mapper)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
        }

        public async Task<CourseDto?> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.GetByIdAsync(request.Id, cancellationToken);
            if (course == null) return null;

            return _mapper.Map<CourseDto>(course);
        }
    }
}
