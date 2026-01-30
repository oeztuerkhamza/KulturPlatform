using MediatR;

namespace KulturPlatform.Application.Commands.AboutUs;

public record CreateAboutUsWhoWeAreCommand(
    string WhoWeAreTr,
    string WhoWeAreDe,
    string? BannerImageUrl = null,
    string? BannerImageBase64 = null,
    string? BannerImageFileName = null
) : IRequest<Guid>;
