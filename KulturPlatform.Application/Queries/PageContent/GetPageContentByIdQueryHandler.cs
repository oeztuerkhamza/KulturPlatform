using AutoMapper;
using KulturPlatform.Application.Dtos;
using KulturPlatform.Application.Interfaces.PageContent;
using MediatR;

namespace KulturPlatform.Application.Queries.PageContent
{
    public class GetPageContentByIdQueryHandler : IRequestHandler<GetPageContentByIdQuery, PageContentDto?>
    {
        private readonly IPageContentRepository _pageContentRepository;
        private readonly IMapper _mapper;

        public GetPageContentByIdQueryHandler(IPageContentRepository pageContentRepository, IMapper mapper)
        {
            _pageContentRepository = pageContentRepository;
            _mapper = mapper;
        }

        public async Task<PageContentDto?> Handle(GetPageContentByIdQuery request, CancellationToken cancellationToken)
        {
            var pageContent = await _pageContentRepository.GetByIdAsync(request.Id, cancellationToken);
            if (pageContent == null) return null;

            return _mapper.Map<PageContentDto>(pageContent);
        }
    }
}
