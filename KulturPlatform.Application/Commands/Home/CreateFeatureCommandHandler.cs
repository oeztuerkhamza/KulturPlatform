using KulturPlatform.Application.Interfaces.Home;
using KulturPlatform.Domain.Commons.AggregateRoot;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;
using MediatR;

namespace KulturPlatform.Application.Commands.Home
{
    public class CreateFeatureCommandHandler : IRequestHandler<CreateFeatureCommand, Guid>
    {
        private readonly IFeatureRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateFeatureCommandHandler(IFeatureRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateFeatureCommand request, CancellationToken cancellationToken)
        {
            var feature = Feature.Create(
                new Title(request.TitleTr),
                new Title(request.TitleDe),
                new Description(request.DescriptionTr),
                new Description(request.DescriptionDe),
                request.Color
            );

            await _repository.AddAsync(feature, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return feature.Id;
        }
    }
}
