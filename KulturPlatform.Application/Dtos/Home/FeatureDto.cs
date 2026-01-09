namespace KulturPlatform.Application.Dtos.Home
{
    public record FeatureDto(
    Guid Id,
    string TitleTr,
    string TitleDe,
    string DescriptionTr,
    string DescriptionDe,
    string Color
);

}
