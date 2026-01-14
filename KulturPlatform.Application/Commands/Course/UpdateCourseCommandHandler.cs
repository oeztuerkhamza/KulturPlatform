using KulturPlatform.Application.Interfaces.Course;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;
using MediatR;

namespace KulturPlatform.Application.Commands.Course
{
    public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCourseCommandHandler(ICourseRepository courseRepository, IUnitOfWork unitOfWork)
        {
            _courseRepository = courseRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.GetByIdAsync(request.Id, cancellationToken);
            if (course == null)
            {
                throw new KeyNotFoundException($"Course with Id {request.Id} not found.");
            }

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
                ? new Address(request.CourseLocation.Street, request.CourseLocation.HouseNo, request.CourseLocation.ZipCode, request.CourseLocation.City, request.CourseLocation.State, request.CourseLocation.Country)
                : null;
            var category = request.Category != null ? new Category(request.Category) : null;

            course.UpdateTitle(titleTr, titleDe);
            course.UpdateDescription(descriptionTr, descriptionDe);
            course.UpdateDetails(detailsTr, detailsDe);
            course.UpdateSchedule(scheduleTr, scheduleDe);
            course.UpdateIcon(request.Icon);
            course.UpdateInstructor(instructor);
            course.UpdateDate(request.Date);
            course.UpdateLocation(location);
            course.UpdateCategory(category);

            if (request.IsActive)
                course.Activate();
            else
                course.Deactivate();

            _courseRepository.Update(course, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
