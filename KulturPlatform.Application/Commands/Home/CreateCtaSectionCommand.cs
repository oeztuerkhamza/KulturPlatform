using MediatR;

namespace KulturPlatform.Application.Commands.Home
{
    public record CreateCtaSectionCommand(
        string TitleTr,
        string TitleDe,
        string DescriptionTr,
        string DescriptionDe,
        string? BackgroundImageUrl,
        string? BackgroundImageBase64,
        string? BackgroundImageFileName,
        string PrimaryButtonTr,
        string PrimaryButtonDe,
        string SecondaryButtonTr,
        string SecondaryButtonDe,
        string DonateButtonTr,
        string DonateButtonDe
    ) : IRequest<Guid>;
}
