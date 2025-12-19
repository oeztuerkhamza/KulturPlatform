using KulturPlatform.Application.Interfaces.GuelenMovement;
using KulturPlatform.Domain.Interfaces;
using MediatR;

namespace KulturPlatform.Application.Commands.GuelenMovement
{
    public class DeleteGuelenMovementHandler
        : IRequestHandler<DeleteGuelenMovementCommand, bool>
    {
        private readonly IGuelenMovementRepository _repository;
        private readonly IUnitOfWork _uow;

        public DeleteGuelenMovementHandler(
            IGuelenMovementRepository repository,
            IUnitOfWork uow)
        {
            _repository = repository;
            _uow = uow;
        }

        public async Task<bool> Handle(DeleteGuelenMovementCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (entity is null) return false;

            _repository.Delete(entity, cancellationToken);
            await _uow.SaveChangesAsync(cancellationToken);

            return true;
        }
    }

}
