using MediatR;

namespace KulturPlatform.Application.Commands.Partner
{
    public record UpdatePartnerCommand(
        Guid Id,
        string Name,
        string DescriptionTr,
        string DescriptionDe,
        int DisplayOrder,
        string? LogoUrl,
        string? WebsiteUrl,
        bool IsActive
    ) : IRequest;
}
