namespace KulturPlatform.Application.Dtos.Activity
{
    /// <summary>
    /// DTO for gallery image upload - supports both URL and base64
    /// </summary>
    public record GalleryImageDto
    {
        public string? Url { get; init; }
        public string? Base64Data { get; init; }
        public string? FileName { get; init; }
    }
}
