using KulturPlatform.Application.Dtos.Home;
using KulturPlatform.Application.Interfaces.Home;
using MediatR;

namespace KulturPlatform.Application.Queries.Home
{
    public class GetHomePageQueryHandler : IRequestHandler<GetHomePageQuery, HomeDto>
    {
        private readonly IHomeReadService _homeReadService;

        public GetHomePageQueryHandler(IHomeReadService homeReadService)
        {
            _homeReadService = homeReadService;
        }

        public async Task<HomeDto> Handle(GetHomePageQuery request, CancellationToken cancellationToken)
        {
            return await _homeReadService.GetHomePageDataAsync(request.Language, cancellationToken);
        }
    }
}
