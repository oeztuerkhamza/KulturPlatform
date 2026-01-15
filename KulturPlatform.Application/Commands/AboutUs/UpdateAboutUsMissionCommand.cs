using MediatR;

namespace KulturPlatform.Application.Commands.AboutUs;

public record UpdateAboutUsMissionCommand(
    Guid Id,
    string MissionTr,
    string MissionDe
) : IRequest;
