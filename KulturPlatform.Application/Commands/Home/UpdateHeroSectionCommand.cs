using KulturPlatform.Application.Dtos.Home;
using MediatR;

namespace KulturPlatform.Application.Commands.Home
{
    public record UpdateHeroSectionCommand(
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
    ) : IRequest<HeroSectionDto>;
}
