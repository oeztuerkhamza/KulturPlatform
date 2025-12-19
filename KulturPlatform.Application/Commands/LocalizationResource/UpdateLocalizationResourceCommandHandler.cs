using KulturPlatform.Application.Interfaces.LocalizationResource;
using KulturPlatform.Domain.Interfaces;
using MediatR;

namespace KulturPlatform.Application.Commands.LocalizationResource
{
    public class UpdateLocalizationResourceCommandHandler : IRequestHandler<UpdateLocalizationResourceCommand>
    {
        private readonly ILocalizationResourceRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateLocalizationResourceCommandHandler(
            ILocalizationResourceRepository repository,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateLocalizationResourceCommand request, CancellationToken cancellationToken)
        {
            var resource = await _repository.GetByIdAsync(request.Id, cancellationToken);
            
            if (resource == null)
            {
                throw new KeyNotFoundException($"Localization resource with Id {request.Id} not found.");
            }

            resource.UpdateTranslations(
                turkish: request.Turkish,
                german: request.German,
                english: request.English,
                description: request.Description
            );

            _repository.Update(resource, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
