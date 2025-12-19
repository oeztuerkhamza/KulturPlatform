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
            return _mapper.Map<IEnumerable<TeaEventDto>>(entities);
        }
    }
}