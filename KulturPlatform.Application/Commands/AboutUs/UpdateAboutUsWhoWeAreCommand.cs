using MediatR;

namespace KulturPlatform.Application.Commands.AboutUs;

public record UpdateAboutUsWhoWeAreCommand(
    Guid Id,
    string WhoWeAreTr,
    string WhoWeAreDe,
    string? BannerImageUrl = null,
    string? BannerImageBase64 = null,
    string? BannerImageFileName = null
) : IRequest;
