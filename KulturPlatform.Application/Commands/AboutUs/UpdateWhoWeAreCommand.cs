using KulturPlatform.Application.Dtos;
using MediatR;

namespace KulturPlatform.Application.Commands.AboutUs
{
    public record UpdateWhoWeAreCommand(
        DescriptionDto WhoWeAreTr,
        DescriptionDto WhoWeAreDe
    ) : IRequest;

    public record CreateWhoWeAreCommand(
        DescriptionDto WhoWeAreTr,
        DescriptionDto WhoWeAreDe
    ) : IRequest;
}
