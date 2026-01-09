namespace KulturPlatform.Application.Commands.Home
{
    public record UpdateFeatureCommand(
        Guid Id,
        string TitleTr,
        string TitleDe,
        string DescriptionTr,
        string DescriptionDe,
        string Color
    );

}
