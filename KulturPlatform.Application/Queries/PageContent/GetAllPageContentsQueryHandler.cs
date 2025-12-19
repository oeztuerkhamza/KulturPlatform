using KulturPlatform.Application.Dtos;
using KulturPlatform.Application.Interfaces.PageContent;
using MediatR;

namespace KulturPlatform.Application.Queries.PageContent
{
    public class GetAllPageContentsQueryHandler : IRequestHandler<GetAllPageContentsQuery, IEnumerable<PageContentDto>>
    {
        private readonly IPageContentReadService _pageContentReadService;

        public GetAllPageContentsQueryHandler(IPageContentReadService pageContentReadService)
        {
            _pageContentReadService = pageContentReadService;
        }

        public async Task<IEnumerable<PageContentDto>> Handle(GetAllPageContentsQuery request, CancellationToken cancellationToken)
        {
            return await _pageContentReadService.GetAllAsync();
        }
    }
}
