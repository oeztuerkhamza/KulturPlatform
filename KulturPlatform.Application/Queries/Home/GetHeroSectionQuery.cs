using KulturPlatform.Application.Dtos.Home;
using MediatR;

namespace KulturPlatform.Application.Queries.Home
{
    public record GetHeroSectionQuery(string Language = "tr") : IRequest<HeroSectionDto?>;
}
