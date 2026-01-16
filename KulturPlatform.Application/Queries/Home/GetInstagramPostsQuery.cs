using KulturPlatform.Application.Dtos.Home;
using MediatR;

namespace KulturPlatform.Application.Queries.Home
{
    public record GetInstagramPostsQuery(int Count = 6) : IRequest<List<InstagramPostDto>>;
}
