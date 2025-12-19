using KulturPlatform.Application.Dtos;
using MediatR;

namespace KulturPlatform.Application.Queries.PageContent
{
    public record GetPageContentsByPageNameQuery(string PageName) : IRequest<IEnumerable<PageContentDto>>;
}
