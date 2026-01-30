namespace KulturPlatform.Application.Dtos.Home
{
    public record HeroSectionDto(
        Guid Id,
        string TitleTr,
        string TitleDe,
        string SubtitleTr,
        string SubtitleDe,
        string DescriptionTr,
        string DescriptionDe,
        string? BackgroundImageUrl,
        string? BackgroundImageBase64,
        string? BackgroundImageFileName,
        string PrimaryButtonTextTr,
        string PrimaryButtonTextDe,
        string SecondaryButtonTextTr,
        string SecondaryButtonTextDe
    );
}
