using KulturPlatform.Application.Dtos.Home;
using KulturPlatform.Application.Interfaces.Home;
using MediatR;

namespace KulturPlatform.Application.Queries.Home
{
    public class GetHeroSectionQueryHandler : IRequestHandler<GetHeroSectionQuery, HeroSectionDto?>
    {
        private readonly IHomeReadService _homeReadService;

        public GetHeroSectionQueryHandler(IHomeReadService homeReadService)
        {
            _homeReadService = homeReadService;
        }

        public async Task<HeroSectionDto?> Handle(GetHeroSectionQuery request, CancellationToken cancellationToken)
        {
            return await _homeReadService.GetHeroSectionAsync(request.Language, cancellationToken);
        }
    }
}
