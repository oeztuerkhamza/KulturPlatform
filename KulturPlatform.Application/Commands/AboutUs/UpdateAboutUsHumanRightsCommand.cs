using MediatR;

namespace KulturPlatform.Application.Commands.AboutUs;

public record UpdateAboutUsHumanRightsCommand(
    Guid Id,
    string TitleTr,
    string TitleDe,
    string DescriptionTr,
    string DescriptionDe,
    string TenkilMuseumUrl,
    string InstagramUrl
) : IRequest;
