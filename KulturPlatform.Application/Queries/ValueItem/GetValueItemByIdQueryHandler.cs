using KulturPlatform.Application.Dtos.NewFolder;
using KulturPlatform.Application.Interfaces.ValueItem;
using MediatR;

namespace KulturPlatform.Application.Queries.ValueItem
{
    public class GetValueItemByIdQueryHandler : IRequestHandler<GetValueItemByIdQuery, ValueItemDetailDto?>
    {
        private readonly IValueItemReadRepository _readRepository;

        public GetValueItemByIdQueryHandler(IValueItemReadRepository readRepository)
        {
            _readRepository = readRepository;
        }

        public async Task<ValueItemDetailDto?> Handle(GetValueItemByIdQuery request, CancellationToken cancellationToken)
        {
            return await _readRepository.GetByIdAsync(request.Id, cancellationToken);
        }
    }
}
