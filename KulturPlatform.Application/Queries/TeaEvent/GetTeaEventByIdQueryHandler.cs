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

            return _mapper.Map<TeaEventDto>(entity);
        }
    }
}