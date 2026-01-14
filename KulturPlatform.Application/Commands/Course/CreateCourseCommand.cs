using KulturPlatform.Application.Dtos.LocalizationDto;
using MediatR;

namespace KulturPlatform.Application.Commands.Course
{
    public record CreateCourseCommand(
        string TitleTr,
        string TitleDe,
        string DescriptionTr,
        string DescriptionDe,
        string? DetailsTr,
        string? DetailsDe,
        string? ScheduleTr,
        string? ScheduleDe,
        string? Icon,
        string? Instructor,
        DateTime? Date,
        AddressDto? CourseLocation,
        string? Category
    ) : IRequest<Guid>;
}
