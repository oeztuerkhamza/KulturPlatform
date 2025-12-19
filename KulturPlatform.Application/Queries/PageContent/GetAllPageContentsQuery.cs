using KulturPlatform.Application.Dtos;
using MediatR;

namespace KulturPlatform.Application.Queries.PageContent
{
    public record GetAllPageContentsQuery() : IRequest<IEnumerable<PageContentDto>>;
}
