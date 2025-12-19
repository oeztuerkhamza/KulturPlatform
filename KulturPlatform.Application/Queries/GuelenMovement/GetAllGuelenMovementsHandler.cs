using AutoMapper;
using KulturPlatform.Application.Dtos;
using KulturPlatform.Application.Interfaces.GuelenMovement;
using MediatR;

namespace KulturPlatform.Application.Queries.GuelenMovement
{
    public class GetAllGuelenMovementsHandler
        : IRequestHandler<GetAllGuelenMovementsQuery, IEnumerable<GuelenMovementDto>>
    {
        private readonly IGuelenMovementReadService _movementReadService;
        private readonly IMapper _mapper;

        public GetAllGuelenMovementsHandler(IGuelenMovementReadService movementReadService, IMapper mapper)
        {
            _movementReadService = movementReadService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GuelenMovementDto>> Handle(
            GetAllGuelenMovementsQuery request,
            CancellationToken cancellationToken)
        {
            var list = await _movementReadService.GetAllAsync();
            return _mapper.Map<IEnumerable<GuelenMovementDto>>(list);
        }
    }

}
