using MediatR;

namespace KulturPlatform.Application.Commands.AboutUs;

public record CreateAboutUsMissionCommand(
    string MissionTr,
    string MissionDe
) : IRequest<Guid>;
