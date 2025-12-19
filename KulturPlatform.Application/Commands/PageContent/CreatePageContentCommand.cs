using MediatR;

namespace KulturPlatform.Application.Commands.PageContent
{
    public record CreatePageContentCommand(
        string PageName,
        string SectionKey,
        string ContentTr,
        string ContentDe
    ) : IRequest<Guid>;
}
