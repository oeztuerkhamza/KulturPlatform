using AutoMapper;
using KulturPlatform.Application.Dtos;
using KulturPlatform.Application.Interfaces.GuelenMovement;
using MediatR;

namespace KulturPlatform.Application.Queries.GuelenMovement
{
    public class GetGuelenMovementByIdHandler
        : IRequestHandler<GetGuelenMovementByIdQuery, GuelenMovementDto?>
    {
        private readonly IGuelenMovementRepository _repository;
        private readonly IMapper _mapper;

        public GetGuelenMovementByIdHandler(
            IGuelenMovementRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GuelenMovementDto?> Handle(GetGuelenMovementByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
            return _mapper.Map<GuelenMovementDto>(entity);
        }
    }

}
