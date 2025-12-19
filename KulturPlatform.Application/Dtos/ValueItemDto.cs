namespace KulturPlatform.Application.Dtos
{
    public record ValueItemDto(
        Guid Id,
        string TitleTr,
        string TitleDe,
        string SubtitleTr,   // eklendi
        string SubtitleDe,
        string DescriptionTr,
        string DescriptionDe,
        int DisplayOrder,
        bool IsActive,
        DateTime CreatedAt,
        DateTime? UpdatedAt
    );
}