using KulturPlatform.Application.Interfaces.PageContent;
using KulturPlatform.Domain.Commons.ValueObjects;
using MediatR;

namespace KulturPlatform.Application.Commands.PageContent
{
    public class UpdatePageContentCommandHandler : IRequestHandler<UpdatePageContentCommand>
    {
        private readonly IPageContentRepository _pageContentRepository;

        public UpdatePageContentCommandHandler(IPageContentRepository pageContentRepository)
        {
            _pageContentRepository = pageContentRepository;
        }

        public async Task Handle(UpdatePageContentCommand request, CancellationToken cancellationToken)
        {
            var pageContent = await _pageContentRepository.GetByIdAsync(request.Id, cancellationToken);
            if (pageContent == null)
            {
                throw new KeyNotFoundException($"PageContent with Id {request.Id} not found.");
            }

            var pageName = new PageName(request.PageName);
            var sectionKey = new SectionKey(request.SectionKey);
            var contentTr = new LocalizedContent(request.ContentTr);
            var contentDe = new LocalizedContent(request.ContentDe);

            pageContent.UpdatePageName(pageName);
            pageContent.UpdateSectionKey(sectionKey);
            pageContent.UpdateContent(contentTr, contentDe);

            if (request.IsActive)
                pageContent.Activate();
            else
                pageContent.Deactivate();

            _pageContentRepository.Update(pageContent, cancellationToken);
        }
    }
}
