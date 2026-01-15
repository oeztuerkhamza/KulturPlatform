using KulturPlatform.Application.Dtos.AboutUs;
using MediatR;

namespace KulturPlatform.Application.Queries.AboutUs;

public record GetAboutUsHumanRightsQuery : IRequest<AboutUsHumanRightsDto?>;
