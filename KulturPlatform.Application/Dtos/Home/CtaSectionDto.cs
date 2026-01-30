namespace KulturPlatform.Application.Dtos.Home
{
    public record CtaSectionDto(
        Guid Id,
        string TitleTr,
        string TitleDe,
        string DescriptionTr,
        string DescriptionDe,
        string? BackgroundImageUrl, // Unified image source (URL or data URI)
        string? BackgroundImageBase64,
        string? BackgroundImageFileName,
        string PrimaryButtonTr,
        string PrimaryButtonDe,
        string SecondaryButtonTr,
        string SecondaryButtonDe,
        string DonateButtonTr,
        string DonateButtonDe
    );
}
