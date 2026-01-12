using MediatR;

namespace KulturPlatform.Application.Queries.Home
{
    public record GetHomePageQuery(string Language = "tr") : IRequest<KulturPlatform.Application.Dtos.Home.HomeDto>;
}
