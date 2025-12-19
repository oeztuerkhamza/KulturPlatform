using KulturPlatform.Application.Dtos.NewFolder;
using KulturPlatform.Application.Interfaces.ValueItem;
using MediatR;

namespace KulturPlatform.Application.Queries.ValueItem
{
    public class GetAllValueItemsQueryHandler : IRequestHandler<GetAllValueItemsQuery, IReadOnlyList<ValueItemDetailDto>>
    {
        private readonly IValueItemReadRepository _readRepository;

        public GetAllValueItemsQueryHandler(IValueItemReadRepository readRepository)
        {
            _readRepository = readRepository;
        }

        public async Task<IReadOnlyList<ValueItemDetailDto>> Handle(GetAllValueItemsQuery request, CancellationToken cancellationToken)
        {
            return await _readRepository.GetAllAsync(cancellationToken);
        }
    }
}
