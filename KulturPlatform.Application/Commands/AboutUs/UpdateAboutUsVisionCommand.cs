using MediatR;

namespace KulturPlatform.Application.Commands.AboutUs;

public record UpdateAboutUsVisionCommand(
    Guid Id,
    string VisionTr,
    string VisionDe
) : IRequest;
