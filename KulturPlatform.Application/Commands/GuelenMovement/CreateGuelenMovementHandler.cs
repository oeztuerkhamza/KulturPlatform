using AutoMapper;
using KulturPlatform.Application.Interfaces.GuelenMovement;
using KulturPlatform.Domain.Interfaces;
using MediatR;

namespace KulturPlatform.Application.Commands.GuelenMovement
{
    public class CreateGuelenMovementHandler
        : IRequestHandler<CreateGuelenMovementCommand, Guid>
    {
        private readonly IGuelenMovementRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;

        public CreateGuelenMovementHandler(
            IGuelenMovementRepository repository,
            IMapper mapper,
            IUnitOfWork uow)
        {
            _repository = repository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<Guid> Handle(CreateGuelenMovementCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Domain.Commons.Aggregates.GuelenMovement>(request);

            await _repository.AddAsync(entity, cancellationToken);
            await _uow.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }

}
