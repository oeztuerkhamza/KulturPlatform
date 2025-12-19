using KulturPlatform.Application.Interfaces.PageContent;
using MediatR;

namespace KulturPlatform.Application.Commands.PageContent
{
    public class DeletePageContentCommandHandler : IRequestHandler<DeletePageContentCommand>
    {
        private readonly IPageContentRepository _pageContentRepository;

        public DeletePageContentCommandHandler(IPageContentRepository pageContentRepository)
        {
            _pageContentRepository = pageContentRepository;
        }

        public async Task Handle(DeletePageContentCommand request, CancellationToken cancellationToken)
        {
            var pageContent = await _pageContentRepository.GetByIdAsync(request.Id, cancellationToken);
            if (pageContent == null)
                throw new KeyNotFoundException($"PageContent with Id {request.Id} not found.");

            _pageContentRepository.Delete(pageContent, cancellationToken);
        }
    }
}
