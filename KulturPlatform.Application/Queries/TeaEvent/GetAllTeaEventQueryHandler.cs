using AutoMapper;
using KulturPlatform.Application.Dtos.TeaEventDto;
using KulturPlatform.Application.Interfaces.TeaEvent;
using MediatR;

namespace KulturPlatform.Application.Queries.TeaEvent
{
    public class GetAllTeaEventQueryHandler : IRequestHandler<GetAllTeaEventQuery, IEnumerable<TeaEventDto>>
    {
        private readonly ITeaEventReadRepository _readRepo;
        private readonly IMapper _mapper;

        public GetAllTeaEventQueryHandler(ITeaEventReadRepository readRepo, IMapper mapper)
        {
            _readRepo = readRepo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TeaEventDto>> Handle(GetAllTeaEventQuery request, CancellationToken cancellationToken)
        {
            var entities = await _readRepo.GetAllAsync(cancellationToken);
            
            // Manual mapping as fallback if AutoMapper fails
            var dtos = entities.Select(e => new TeaEventDto
            {
                Id = e.Id,
                TitleTr = e.TitleTurkish.Value,
                TitleDe = e.TitleGerman.Value,
                IntroTr = e.Content.IntroTr,
                IntroDe = e.Content.IntroDe,
                HeritageTextTr = e.Content.HeritageTextTr,
                HeritageTextDe = e.Content.HeritageTextDe,
                ParticipationTextTr = e.Content.ParticipationTextTr,
                ParticipationTextDe = e.Content.ParticipationTextDe,
                ContactEmail = e.Content.ContactEmail,
                Date = e.Date,
                Time = e.Time,
                Location = e.Location.Value,
                ImageSource = e.GetImageSource(),  // ← ÖNEMLİ: Bu metodla hybrid image çözülüyor
                IsActive = e.IsActive
            }).ToList();
            
            return dtos;
        }
    }
}