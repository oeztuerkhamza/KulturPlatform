using KulturPlatform.Application.Dtos.AboutUs;
using MediatR;

namespace KulturPlatform.Application.Commands.AboutUs
{
    public record UpdateFocusAreasCommand(
        List<AboutUsItemDto> FocusAreas
    ) : IRequest;
}
