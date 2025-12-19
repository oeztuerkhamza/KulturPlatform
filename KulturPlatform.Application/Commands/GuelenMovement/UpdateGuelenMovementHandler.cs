using AutoMapper;
using KulturPlatform.Application.Interfaces.GuelenMovement;
using KulturPlatform.Domain.Interfaces;
using MediatR;

namespace KulturPlatform.Application.Commands.GuelenMovement
{
    public class UpdateGuelenMovementHandler
        : IRequestHandler<UpdateGuelenMovementCommand, bool>
    {
        private readonly IGuelenMovementRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;

        public UpdateGuelenMovementHandler(
            IGuelenMovementRepository repository,
            IMapper mapper,
            IUnitOfWork uow)
        {
            _repository = repository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<bool> Handle(UpdateGuelenMovementCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (entity is null) return false;

            _mapper.Map(request, entity);

            _repository.Update(entity, cancellationToken);
            await _uow.SaveChangesAsync(cancellationToken);

            return true;
        }
    }

}
