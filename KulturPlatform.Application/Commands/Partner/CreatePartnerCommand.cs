using MediatR;

namespace KulturPlatform.Application.Commands.Partner
{
    public record CreatePartnerCommand(
        string Name,
        string DescriptionTr,
        string DescriptionDe,
        int DisplayOrder,
        string? LogoUrl,
        string? WebsiteUrl
    ) : IRequest<Guid>;
}
