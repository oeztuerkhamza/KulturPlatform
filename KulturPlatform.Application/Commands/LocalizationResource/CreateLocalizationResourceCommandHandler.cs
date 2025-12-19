using KulturPlatform.Application.Interfaces.LocalizationResource;
using KulturPlatform.Domain.Interfaces;
using MediatR;

namespace KulturPlatform.Application.Commands.LocalizationResource
{
    public class CreateLocalizationResourceCommandHandler : IRequestHandler<CreateLocalizationResourceCommand, Guid>
    {
        private readonly ILocalizationResourceRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateLocalizationResourceCommandHandler(
            ILocalizationResourceRepository repository,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateLocalizationResourceCommand request, CancellationToken cancellationToken)
        {
            // Check if key already exists
            if (await _repository.KeyExistsAsync(request.Key))
            {
                throw new InvalidOperationException($"Localization resource with key '{request.Key}' already exists.");
            }

            var resource = Domain.Commons.Aggregates.LocalizationResource.CreateNew(
                key: request.Key,
                turkish: request.Turkish,
                german: request.German,
                english: request.English,
                section: request.Section,
                description: request.Description
            );

            await _repository.AddAsync(resource, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return resource.Id;
        }
    }
}
