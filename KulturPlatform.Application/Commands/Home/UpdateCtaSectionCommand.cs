using KulturPlatform.Application.Dtos.Home;
using MediatR;

namespace KulturPlatform.Application.Commands.Home
{
    public record UpdateCtaSectionCommand(
        Guid Id,
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
    ) : IRequest<CtaSectionDto>;
}
