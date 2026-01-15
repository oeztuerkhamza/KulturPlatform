using MediatR;

namespace KulturPlatform.Application.Commands.AboutUs;

public record CreateAboutUsHumanRightsCommand(
    string TitleTr,
    string TitleDe,
    string DescriptionTr,
    string DescriptionDe,
    string TenkilMuseumUrl,
    string InstagramUrl
) : IRequest<Guid>;
