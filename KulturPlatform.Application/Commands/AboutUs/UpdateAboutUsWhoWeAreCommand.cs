using MediatR;

namespace KulturPlatform.Application.Commands.AboutUs;

public record UpdateAboutUsWhoWeAreCommand(
    Guid Id,
    string WhoWeAreTr,
    string WhoWeAreDe
) : IRequest;
