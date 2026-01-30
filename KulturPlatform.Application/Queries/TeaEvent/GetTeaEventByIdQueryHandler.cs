using AutoMapper;
using KulturPlatform.Application.Dtos.TeaEventDto;
using KulturPlatform.Application.Interfaces.TeaEvent;
using MediatR;

namespace KulturPlatform.Application.Queries.TeaEvent
{
    public class GetTeaEventByIdQueryHandler : IRequestHandler<GetTeaEventByIdQuery, TeaEventDto>
    {
        private readonly ITeaEventReadRepository _readRepo;
        private readonly IMapper _mapper;

        public GetTeaEventByIdQueryHandler(ITeaEventReadRepository readRepo, IMapper mapper)
        {
            _readRepo = readRepo;
            _mapper = mapper;
        }

        public async Task<TeaEventDto> Handle(GetTeaEventByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _readRepo.GetByIdAsync(request.Id, cancellationToken);
            if (entity == null) return null;

            // Manual mapping to ensure ImageSource is included
            return new TeaEventDto
            {
                Id = entity.Id,
                TitleTr = entity.TitleTurkish.Value,
                TitleDe = entity.TitleGerman.Value,
                IntroTr = entity.Content.IntroTr,
                IntroDe = entity.Content.IntroDe,
                HeritageTextTr = entity.Content.HeritageTextTr,
                HeritageTextDe = entity.Content.HeritageTextDe,
                ParticipationTextTr = entity.Content.ParticipationTextTr,
                ParticipationTextDe = entity.Content.ParticipationTextDe,
                ContactEmail = entity.Content.ContactEmail,
                Date = entity.Date,
                Time = entity.Time,
                Location = entity.Location.Value,
                ImageSource = entity.GetImageSource(),  // ← ÖNEMLİ
                IsActive = entity.IsActive
            };
        }
    }
}