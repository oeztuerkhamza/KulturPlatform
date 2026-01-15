using MediatR;

namespace KulturPlatform.Application.Commands.AboutUs;

public record CreateAboutUsVisionCommand(
    string VisionTr,
    string VisionDe
) : IRequest<Guid>;
