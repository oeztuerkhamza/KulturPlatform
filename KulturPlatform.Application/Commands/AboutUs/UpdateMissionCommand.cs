using KulturPlatform.Application.Dtos;
using MediatR;

namespace KulturPlatform.Application.Commands.AboutUs
{
    public record UpdateMissionCommand(
        DescriptionDto MissionTr,
        DescriptionDto MissionDe
    ) : IRequest;

    public record CreateMissionCommand(
        DescriptionDto MissionTr,
        DescriptionDto MissionDe
    ) : IRequest;
}
