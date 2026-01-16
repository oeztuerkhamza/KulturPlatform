using KulturPlatform.Application.Dtos.Home;
using KulturPlatform.Application.Interfaces.Home;
using MediatR;

namespace KulturPlatform.Application.Queries.Home
{
    public class GetFeaturesQueryHandler : IRequestHandler<GetFeaturesQuery, List<FeatureDto>>
    {
        private readonly IHomeReadService _homeReadService;

        public GetFeaturesQueryHandler(IHomeReadService homeReadService)
        {
            _homeReadService = homeReadService;
        }

        public async Task<List<FeatureDto>> Handle(GetFeaturesQuery request, CancellationToken cancellationToken)
        {
            return await _homeReadService.GetFeaturesAsync(request.Language, cancellationToken);
        }
    }
}
