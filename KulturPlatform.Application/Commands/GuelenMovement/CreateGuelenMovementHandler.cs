using KulturPlatform.Application.Interfaces.GuelenMovement;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;
using MediatR;

namespace KulturPlatform.Application.Commands.GuelenMovement
{
    public class CreateGuelenMovementHandler
        : IRequestHandler<CreateGuelenMovementCommand, Guid>
    {
        private readonly IGuelenMovementRepository _repository;
        private readonly IUnitOfWork _uow;

        public CreateGuelenMovementHandler(
            IGuelenMovementRepository repository,
            IUnitOfWork uow)
        {
            _repository = repository;
            _uow = uow;
        }

        public async Task<Guid> Handle(CreateGuelenMovementCommand request, CancellationToken cancellationToken)
        {
            var entity = Domain.Commons.Aggregates.GuelenMovement.CreateNew(
                Title.Create(request.TitleTr),
                Title.Create(request.TitleDe),
                new Description(request.IntroductionTr),
                new Description(request.IntroductionDe),
                Url.Create(request.ImageUrl),
                Title.Create(request.PhilosophyTitleTr),
                Title.Create(request.PhilosophyTitleDe),
                new Description(request.PhilosophyContentTr),
                new Description(request.PhilosophyContentDe),
                Title.Create(request.DialogTitleTr),
                Title.Create(request.DialogTitleDe),
                new Description(request.DialogContentTr),
                new Description(request.DialogContentDe),
                Title.Create(request.NetworkTitleTr),
                Title.Create(request.NetworkTitleDe),
                new Description(request.NetworkContentTr),
                new Description(request.NetworkContentDe),
                Title.Create(request.SpiritualTitleTr),
                Title.Create(request.SpiritualTitleDe),
                new Description(request.SpiritualContentTr),
                new Description(request.SpiritualContentDe),
                Title.Create(request.VisionTitleTr),
                Title.Create(request.VisionTitleDe),
                new Description(request.VisionContentTr),
                new Description(request.VisionContentDe)
            );

            await _repository.AddAsync(entity, cancellationToken);
            await _uow.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
