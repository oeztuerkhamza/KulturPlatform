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
        string? ImageUrl, // Deprecated: Use ImageSource instead
        string? ImageSource, // New: Returns either URL or data URI from database
        ImageMetadataDto? ImageMetadata, // New: Metadata about the image
        List<GalleryImageDto> GalleryImages, // ✅ Returns GalleryImageDto objects
        string? VideoUrl,
        bool IsActive
    );

    /// <summary>
    /// Metadata about the stored image
    /// </summary>
    public record ImageMetadataDto(
        string StorageType, // "URL" or "Database"
        string? MimeType, // Only for database storage
        string? FileName, // Only for database storage
        int? FileSizeKB // Only for database storage
    );
}
