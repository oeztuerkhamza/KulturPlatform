using KulturPlatform.Application.Interfaces.Course;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;
using MediatR;

namespace KulturPlatform.Application.Commands.Course
{
    public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, Guid>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCourseCommandHandler(ICourseRepository courseRepository, IUnitOfWork unitOfWork)
        {
            _courseRepository = courseRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            var titleTr = Title.Create(request.TitleTr);
            var titleDe = Title.Create(request.TitleDe);
            var descriptionTr = new Description(request.DescriptionTr);
            var descriptionDe = new Description(request.DescriptionDe);
            var detailsTr = request.DetailsTr != null ? new CourseDetails(request.DetailsTr) : null;
            var detailsDe = request.DetailsDe != null ? new CourseDetails(request.DetailsDe) : null;
            var scheduleTr = request.ScheduleTr != null ? new CourseSchedule(request.ScheduleTr) : null;
            var scheduleDe = request.ScheduleDe != null ? new CourseSchedule(request.ScheduleDe) : null;
            var instructor = request.Instructor != null ? new InstructorName(request.Instructor) : null;
            var location = request.CourseLocation != null
                ? new Address(
                    request.CourseLocation.Street,
                    request.CourseLocation.HouseNo,
                    request.CourseLocation.City,
                    request.CourseLocation.State,
                    request.CourseLocation.Country,
                    request.CourseLocation.ZipCode
                )
                : null;
            var category = request.Category != null ? new Category(request.Category) : null;

            var course = Domain.Commons.Aggregates.Course.Create(
                titleTr,
                titleDe,
                descriptionTr,
                descriptionDe,
                detailsTr,
                detailsDe,
                scheduleTr,
                scheduleDe,
                request.Icon,
                instructor,
                request.Date,
                location,
                category
            );

            await _courseRepository.AddAsync(course, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return course.Id;
        }
    }
}
