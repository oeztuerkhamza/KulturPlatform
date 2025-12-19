using KulturPlatform.Application.Interfaces.Imprint;
using KulturPlatform.Domain.Interfaces;
using MediatR;

namespace KulturPlatform.Application.Commands.Imprint
{
    public class DeleteImprintCommandHandler : IRequestHandler<DeleteImprintCommand>
    {
        private readonly IImprintRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteImprintCommandHandler(IImprintRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteImprintCommand request, CancellationToken cancellationToken)
        {
            var imprint = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (imprint == null)
                throw new KeyNotFoundException($"Imprint with Id {request.Id} not found.");

            _repository.Delete(imprint, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
