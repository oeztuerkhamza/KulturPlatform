using KulturPlatform.Application.Interfaces.Satzung;
using KulturPlatform.Domain.Interfaces;
using MediatR;

namespace KulturPlatform.Application.Commands.Satzung
{
    public class DeleteSatzungHandler : IRequestHandler<DeleteSatzungCommand>
    {
        private readonly ISatzungRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteSatzungHandler(ISatzungRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteSatzungCommand request, CancellationToken cancellationToken)
        {
            // Entity'yi DB'den al
            var satzung = await _repository.GetByIdAsync(request.Id, cancellationToken)
                          ?? throw new KeyNotFoundException($"Satzung with Id {request.Id} not found.");

            // Repository Delete metodun synchronous olduğu için direkt çağır
            _repository.Delete(satzung, cancellationToken);

            // Değişiklikleri kaydet
            await _unitOfWork.SaveChangesAsync(cancellationToken);

        }
    }
}
