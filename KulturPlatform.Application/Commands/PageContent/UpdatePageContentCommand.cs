using MediatR;

namespace KulturPlatform.Application.Commands.PageContent
{
    public record UpdatePageContentCommand(
        Guid Id,
        string PageName,
        string SectionKey,
        string ContentTr,
        string ContentDe,
        bool IsActive
    ) : IRequest;
}
