using KulturPlatform.Application.Dtos.AboutUs;
using MediatR;

namespace KulturPlatform.Application.Commands.AboutUs
{
    public record UpdateActivityAreasCommand(
        List<AboutUsItemDto> ActivityAreas
    ) : IRequest;
}
