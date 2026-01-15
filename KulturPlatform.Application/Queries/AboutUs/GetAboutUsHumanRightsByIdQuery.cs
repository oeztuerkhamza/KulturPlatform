using KulturPlatform.Application.Dtos.AboutUs;
using MediatR;

namespace KulturPlatform.Application.Queries.AboutUs;

public record GetAboutUsHumanRightsByIdQuery(Guid Id) : IRequest<AboutUsHumanRightsDto?>;
