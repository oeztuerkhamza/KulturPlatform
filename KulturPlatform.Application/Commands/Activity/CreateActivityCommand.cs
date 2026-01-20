using KulturPlatform.Application.Dtos.Activity;
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
        string? ImageBase64, // New: Base64 encoded image data (with or without data URI prefix)
        string? ImageFileName, // New: Original filename for base64 image
        List<GalleryImageDto>? GalleryImages, // Changed from List<string>
        string? VideoUrl,
        bool IsActive = true
    ) : IRequest<Guid>;
}
