using MediatR;

namespace KulturPlatform.Application.Commands.TeaEvent
{
    public sealed record UpdateTeaEventCommand
    (
        Guid Id,

        string TitleTr,
        string TitleDe,

        string IntroTr,
        string IntroDe,

        string HeritageTextTr,
        string HeritageTextDe,

        string ParticipationTextTr,
        string ParticipationTextDe,

        string ContactEmail,

        string Date,
        string Time,
        string Location,
        
        string? ImageUrl = null,
        string? ImageBase64 = null,
        string? ImageFileName = null
    ) : IRequest;
}
