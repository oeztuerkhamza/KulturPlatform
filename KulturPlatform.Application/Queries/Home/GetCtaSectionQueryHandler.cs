using KulturPlatform.Application.Dtos.Home;
using KulturPlatform.Application.Interfaces.Home;
using MediatR;

namespace KulturPlatform.Application.Queries.Home
{
    public class GetCtaSectionQueryHandler : IRequestHandler<GetCtaSectionQuery, CtaSectionDto?>
    {
        private readonly IHomeReadService _homeReadService;

        public GetCtaSectionQueryHandler(IHomeReadService homeReadService)
        {
            _homeReadService = homeReadService;
        }

        public async Task<CtaSectionDto?> Handle(GetCtaSectionQuery request, CancellationToken cancellationToken)
        {
            return await _homeReadService.GetCtaSectionAsync(request.Language, cancellationToken);
        }
    }
}
