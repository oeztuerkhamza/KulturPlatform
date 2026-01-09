using KulturPlatform.Application.Dtos.LocalizationDto;

namespace KulturPlatform.Application.Dtos.Activity
{
    public record ActivityDto(
         Guid Id,
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
         List<string> GalleryImages,
         string? VideoUrl,
         bool IsActive
     );
}
