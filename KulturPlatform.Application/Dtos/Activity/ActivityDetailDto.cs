namespace KulturPlatform.Application.Dtos.Activity
{
    public record ActivityDetailDto(
    Guid Id,
    TranslationDto Title,
    TranslationDto Description,
    TranslationDto? DetailedContent,
    TranslationDto Date,
    string Address,
    string Category,
    string? ImageUrl,
    string? VideoUrl,
    List<string> GalleryImages
);

}
