using KulturPlatform.Application.Dtos.LocalizationDto;
using MediatR;

namespace KulturPlatform.Application.Commands.Activity
{
    public record CreateActivityCommand(
        string TitleTr,
        string TitleDe,
        string DescriptionTr,
        string DescriptionDe,
        string? DetailedContentTr,
        string? DetailedContentDe,
        string Date,
        AddressDto Address,
        string Category,
        string? ImageUrl,
        List<string>? GalleryImages,
        string? VideoUrl,
        bool IsActive = true
    ) : IRequest<Guid>;
}
