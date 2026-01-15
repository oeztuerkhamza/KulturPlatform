using MediatR;

namespace KulturPlatform.Application.Commands.AboutUs;

public record CreateAboutUsWhoWeAreCommand(
    string WhoWeAreTr,
    string WhoWeAreDe
) : IRequest<Guid>;
