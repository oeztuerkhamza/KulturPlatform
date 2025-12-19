using KulturPlatform.Application.Interfaces.TeaEvent;
using KulturPlatform.Domain.Commons.ValueObjects;
using MediatR;

namespace KulturPlatform.Application.Commands.TeaEvent
{
    public sealed class UpdateTeaEventCommandHandler
        : IRequestHandler<UpdateTeaEventCommand>
    {
        private readonly ITeaEventReadRepository _readRepo;
        private readonly ITeaEventWriteRepository _writeRepo;

        public UpdateTeaEventCommandHandler(
            ITeaEventReadRepository readRepo,
            ITeaEventWriteRepository writeRepo)
        {
            _readRepo = readRepo;
            _writeRepo = writeRepo;
        }

        public async Task Handle(
            UpdateTeaEventCommand request,
            CancellationToken cancellationToken)
        {
            var teaEvent = await _readRepo.GetByIdAsync(request.Id, cancellationToken)
                           ?? throw new Exception("TeaEvent not found");

            var content = TeaEventContent.Create(
                request.IntroTr,
                request.IntroDe,
                request.HeritageTextTr,
                request.HeritageTextDe,
                request.ParticipationTextTr,
                request.ParticipationTextDe,
                request.ContactEmail
            );

            teaEvent.UpdateTitle(
                Title.Create(request.TitleTr),
                Title.Create(request.TitleDe)
            );

            teaEvent.UpdateContent(content);
            teaEvent.Reschedule(request.Date, request.Time);

            await _writeRepo.UpdateAsync(teaEvent, cancellationToken);
        }
    }
}
