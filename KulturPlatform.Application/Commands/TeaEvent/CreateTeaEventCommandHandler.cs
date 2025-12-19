using KulturPlatform.Application.Interfaces.TeaEvent;
using KulturPlatform.Domain.Commons.ValueObjects;
using MediatR;

namespace KulturPlatform.Application.Commands.TeaEvent
{
    public sealed class CreateTeaEventCommandHandler
        : IRequestHandler<CreateTeaEventCommand, Guid>
    {
        private readonly ITeaEventWriteRepository _writeRepo;

        public CreateTeaEventCommandHandler(ITeaEventWriteRepository writeRepo)
        {
            _writeRepo = writeRepo;
        }

        public async Task<Guid> Handle(
            CreateTeaEventCommand request,
            CancellationToken cancellationToken)
        {
            var content = TeaEventContent.Create(
                request.IntroTr,
                request.IntroDe,
                request.HeritageTextTr,
                request.HeritageTextDe,
                request.ParticipationTextTr,
                request.ParticipationTextDe,
                request.ContactEmail
            );

            var teaEvent = Domain.Commons.Aggregates.TeaEvent.CreateNew(
                Title.Create(request.TitleTr),
                Title.Create(request.TitleDe),
                content,
                request.Date,
                request.Time,
                Location.Create(request.Location),
                Url.Create(request.ImageUrl)
            );

            await _writeRepo.AddAsync(teaEvent, cancellationToken);
            return teaEvent.Id;
        }
    }
}
