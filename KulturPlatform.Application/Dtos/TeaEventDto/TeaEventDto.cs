namespace KulturPlatform.Application.Dtos.TeaEventDto
{
    public record class TeaEventDto
    {
        public Guid Id { get; init; }
        public string TitleTr { get; init; }
        public string TitleDe { get; init; }
        public string IntroTr { get; init; }
        public string IntroDe { get; init; }
        public string HeritageTextTr { get; init; }
        public string HeritageTextDe { get; init; }
        public string ParticipationTextTr { get; init; }
        public string ParticipationTextDe { get; init; }
        public string Date { get; init; }
        public string Time { get; init; }
        public string Location { get; init; }
        
        /// <summary>
        /// Deprecated: Use ImageSource instead
        /// </summary>
        public string? ImageUrl { get; init; }
        
        /// <summary>
        /// Unified image source - either URL or data URI from database
        /// </summary>
        public string? ImageSource { get; init; }
        
        /// <summary>
        /// Metadata about the stored image
        /// </summary>
        public ImageMetadataDto? ImageMetadata { get; init; }
        
        public string ContactEmail { get; init; }
        public bool IsActive { get; init; }
    }

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
