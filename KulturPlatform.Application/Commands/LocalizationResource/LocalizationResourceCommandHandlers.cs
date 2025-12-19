using KulturPlatform.Application.Interfaces.LocalizationResource;
using KulturPlatform.Domain.Interfaces;
using MediatR;

namespace KulturPlatform.Application.Commands.LocalizationResource
{
    public class ActivateLocalizationResourceCommandHandler : IRequestHandler<ActivateLocalizationResourceCommand>
    {
        private readonly ILocalizationResourceRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public ActivateLocalizationResourceCommandHandler(ILocalizationResourceRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(ActivateLocalizationResourceCommand request, CancellationToken cancellationToken)
        {
            var resource = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (resource == null)
                throw new KeyNotFoundException($"Localization resource with Id {request.Id} not found.");

            resource.Activate();
            _repository.Update(resource, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }

    public class DeactivateLocalizationResourceCommandHandler : IRequestHandler<DeactivateLocalizationResourceCommand>
    {
        private readonly ILocalizationResourceRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeactivateLocalizationResourceCommandHandler(ILocalizationResourceRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeactivateLocalizationResourceCommand request, CancellationToken cancellationToken)
        {
            var resource = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (resource == null)
                throw new KeyNotFoundException($"Localization resource with Id {request.Id} not found.");

            resource.Deactivate();
            _repository.Update(resource, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }

    public class DeleteLocalizationResourceCommandHandler : IRequestHandler<DeleteLocalizationResourceCommand>
    {
        private readonly ILocalizationResourceRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteLocalizationResourceCommandHandler(ILocalizationResourceRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteLocalizationResourceCommand request, CancellationToken cancellationToken)
        {
            var resource = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (resource == null)
                throw new KeyNotFoundException($"Localization resource with Id {request.Id} not found.");

            _repository.Delete(resource, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
