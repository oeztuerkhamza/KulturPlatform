using MediatR;

namespace KulturPlatform.Application.Commands.Activity
{
    public record UpdateActivityCommand(
        Guid Id,
        string TitleTr,
        string TitleDe,
        string DescriptionTr,
        string DescriptionDe,
        string? DetailedContentTr,
        string? DetailedContentDe,
        string DateTr,
        string DateDe,
        DateTime DateISO,
        string Street,
        string HouseNo,
        string City,
        string State,
        string Country,
        string ZipCode,
        string Category,
        string? ImageUrl,
        List<string>? GalleryImages,
        string? VideoUrl,
        bool IsActive
    ) : IRequest;


}
