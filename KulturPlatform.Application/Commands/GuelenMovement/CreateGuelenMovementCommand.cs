using MediatR;

namespace KulturPlatform.Application.Commands.GuelenMovement
{
    public record CreateGuelenMovementCommand(
        // Main Content
        string TitleTr,
        string TitleDe,
        string IntroductionTr,
        string IntroductionDe,
        string ImageUrl,
        // Philosophy Section
        string PhilosophyTitleTr,
        string PhilosophyTitleDe,
        string PhilosophyContentTr,
        string PhilosophyContentDe,
        // Dialog Section
        string DialogTitleTr,
        string DialogTitleDe,
        string DialogContentTr,
        string DialogContentDe,
        // Network Section
        string NetworkTitleTr,
        string NetworkTitleDe,
        string NetworkContentTr,
        string NetworkContentDe,
        // Spiritual Section
        string SpiritualTitleTr,
        string SpiritualTitleDe,
        string SpiritualContentTr,
        string SpiritualContentDe,
        // Vision Section
        string VisionTitleTr,
        string VisionTitleDe,
        string VisionContentTr,
        string VisionContentDe
    ) : IRequest<Guid>;
}
