using KulturPlatform.Application.Dtos;
using MediatR;

namespace KulturPlatform.Application.Commands.AboutUs
{
    public record UpdateVisionCommand(
        DescriptionDto VisionTr,
        DescriptionDto VisionDe
    ) : IRequest;

    public record CreateVisionCommand(
        DescriptionDto VisionTr,
        DescriptionDto VisionDe
    ) : IRequest;
}
