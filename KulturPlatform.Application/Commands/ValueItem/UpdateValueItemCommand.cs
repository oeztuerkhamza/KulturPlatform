using MediatR;

namespace KulturPlatform.Application.Commands.ValueItem
{
    public record UpdateValueItemCommand(
        Guid Id,
        string TitleTr,
        string TitleDe,
        string SubtitleTr,
        string SubtitleDe,
        string IntroTr,
        string IntroDe,
        SectionCommand NameAndPurpose,
        SectionCommand Why,
        SectionCommand Who,
        SectionCommand How,
        string CtaButtonTr,
        string CtaButtonDe
    ) : IRequest<Unit>;
}
