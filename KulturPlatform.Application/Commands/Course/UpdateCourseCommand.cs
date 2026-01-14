using KulturPlatform.Application.Dtos.LocalizationDto;
using MediatR;

namespace KulturPlatform.Application.Commands.Course
{
    public record UpdateCourseCommand(
        Guid Id,
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
        DateTime Time,
        AddressDto? CourseLocation,
        string? Category,
        bool IsActive
    ) : IRequest;
}
