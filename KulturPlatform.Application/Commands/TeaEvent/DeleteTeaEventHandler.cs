using KulturPlatform.Application.Interfaces.TeaEvent;
using KulturPlatform.Domain.Interfaces;
using MediatR;

namespace KulturPlatform.Application.Commands.TeaEvent
{
    public class DeleteTeaEventHandler : IRequestHandler<DeleteTeaEventCommand, bool>
    {
        private readonly ITeaEventReadRepository _readRepo;
        private readonly ITeaEventWriteRepository _writeRepo;
        private readonly IUnitOfWork _uow;

        public DeleteTeaEventHandler(
            ITeaEventReadRepository readRepo,
            ITeaEventWriteRepository writeRepo,
            IUnitOfWork uow)
        {
            _readRepo = readRepo;
            _writeRepo = writeRepo;
            _uow = uow;
        }

        public async Task<bool> Handle(DeleteTeaEventCommand request, CancellationToken cancellationToken)
        {
            // Önce read repository ile entity al
            var entity = await _readRepo.GetByIdAsync(request.Id, cancellationToken);
            if (entity is null) return false;

            // Write repository ile sil
            await _writeRepo.DeleteAsync(entity, cancellationToken);

            // Unit of Work ile commit et
            await _uow.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}