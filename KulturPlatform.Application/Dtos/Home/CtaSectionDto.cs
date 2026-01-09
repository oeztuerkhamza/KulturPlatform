namespace KulturPlatform.Application.Dtos.Home
{
    public record CtaSectionDto(
    Guid Id,
    string TitleTr,
    string TitleDe,
    string DescriptionTr,
    string DescriptionDe,
    string PrimaryButtonTr,
    string PrimaryButtonDe,
    string SecondaryButtonTr,
    string SecondaryButtonDe,
    string DonateButtonTr,
    string DonateButtonDe
);

}
