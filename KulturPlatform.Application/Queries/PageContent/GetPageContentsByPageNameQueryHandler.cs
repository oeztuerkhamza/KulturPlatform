using KulturPlatform.Application.Dtos;
using KulturPlatform.Application.Interfaces.PageContent;
using MediatR;

namespace KulturPlatform.Application.Queries.PageContent
{
    public class GetPageContentsByPageNameQueryHandler : IRequestHandler<GetPageContentsByPageNameQuery, IEnumerable<PageContentDto>>
    {
        private readonly IPageContentReadService _pageContentReadService;

        public GetPageContentsByPageNameQueryHandler(IPageContentReadService pageContentReadService)
        {
            _pageContentReadService = pageContentReadService;
        }

        public async Task<IEnumerable<PageContentDto>> Handle(GetPageContentsByPageNameQuery request, CancellationToken cancellationToken)
        {
            return await _pageContentReadService.GetByPageNameAsync(request.PageName);
        }
    }
}
