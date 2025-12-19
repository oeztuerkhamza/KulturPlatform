namespace KulturPlatform.Application.Dtos.NewFolder
{
    public record ValueItemDetailDto(
        Guid Id,
        string TitleTr,
        string TitleDe,
        string SubtitleTr,
        string SubtitleDe,
        string IntroTr,
        string IntroDe,
        List<SectionDto> Sections,
        string CtaButtonTr,
        string CtaButtonDe,
        int DisplayOrder,
        bool IsActive,
        DateTime CreatedAt,
        DateTime? UpdatedAt
    );
}
