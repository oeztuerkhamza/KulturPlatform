using KulturPlatform.Application.Dtos;
using MediatR;

namespace KulturPlatform.Application.Queries.PageContent
{
    public record GetPageContentByIdQuery(Guid Id) : IRequest<PageContentDto?>;
}
