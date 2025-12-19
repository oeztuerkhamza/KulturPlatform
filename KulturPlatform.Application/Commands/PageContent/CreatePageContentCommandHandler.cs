using KulturPlatform.Application.Interfaces.PageContent;
using KulturPlatform.Domain.Commons.ValueObjects;
using MediatR;

namespace KulturPlatform.Application.Commands.PageContent
{
    public class CreatePageContentCommandHandler : IRequestHandler<CreatePageContentCommand, Guid>
    {
        private readonly IPageContentRepository _pageContentRepository;

        public CreatePageContentCommandHandler(IPageContentRepository pageContentRepository)
        {
            _pageContentRepository = pageContentRepository;
        }

        public async Task<Guid> Handle(CreatePageContentCommand request, CancellationToken cancellationToken)
        {
            var pageName = new PageName(request.PageName);
            var sectionKey = new SectionKey(request.SectionKey);
            var contentTr = new LocalizedContent(request.ContentTr);
            var contentDe = new LocalizedContent(request.ContentDe);

            var pageContent = Domain.Commons.Aggregates.PageContent.Create(
                pageName, sectionKey, contentTr, contentDe);

            await _pageContentRepository.AddAsync(pageContent, cancellationToken);

            return pageContent.Id;
        }
    }
}
