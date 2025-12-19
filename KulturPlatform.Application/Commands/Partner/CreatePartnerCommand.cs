using MediatR;

namespace KulturPlatform.Application.Commands.Partner
{
    public record CreatePartnerCommand(
        string Name,
        int DisplayOrder,
        string? LogoUrl,
        string? WebsiteUrl
    ) : IRequest<Guid>;
}
