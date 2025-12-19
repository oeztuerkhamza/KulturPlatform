using MediatR;

namespace KulturPlatform.Application.Commands.Partner
{
    public record UpdatePartnerCommand(
        Guid Id,
        string Name,
        int DisplayOrder,
        string? LogoUrl,
        string? WebsiteUrl,
        bool IsActive
    ) : IRequest;
}
