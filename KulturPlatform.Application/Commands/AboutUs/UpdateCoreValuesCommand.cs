using KulturPlatform.Application.Dtos.AboutUs;
using MediatR;

namespace KulturPlatform.Application.Commands.AboutUs
{
    public record UpdateCoreValuesCommand(
        List<AboutUsItemDto> CoreValues
    ) : IRequest;
}
