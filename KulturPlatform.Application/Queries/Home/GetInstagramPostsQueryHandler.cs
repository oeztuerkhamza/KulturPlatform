using KulturPlatform.Application.Dtos.Home;
using KulturPlatform.Application.Interfaces.Home;
using MediatR;

namespace KulturPlatform.Application.Queries.Home
{
    public class GetInstagramPostsQueryHandler : IRequestHandler<GetInstagramPostsQuery, List<InstagramPostDto>>
    {
        private readonly IHomeReadService _homeReadService;

        public GetInstagramPostsQueryHandler(IHomeReadService homeReadService)
        {
            _homeReadService = homeReadService;
        }

        public async Task<List<InstagramPostDto>> Handle(GetInstagramPostsQuery request, CancellationToken cancellationToken)
        {
            return await _homeReadService.GetInstagramPostsAsync(request.Count, cancellationToken);
        }
    }
}
