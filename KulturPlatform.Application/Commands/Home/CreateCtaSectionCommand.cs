namespace KulturPlatform.Application.Commands.Home
{
    public record CreateCtaSectionCommand(
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
