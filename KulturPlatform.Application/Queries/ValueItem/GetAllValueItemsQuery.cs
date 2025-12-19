using KulturPlatform.Application.Dtos.NewFolder;
using MediatR;

namespace KulturPlatform.Application.Queries.ValueItem
{
    public record GetAllValueItemsQuery() : IRequest<IReadOnlyList<ValueItemDetailDto>>;
}
