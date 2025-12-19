using AutoMapper;
using KulturPlatform.Application.Dtos.SatzungDto;
using KulturPlatform.Application.Interfaces.Satzung;
using MediatR;

namespace KulturPlatform.Application.Queries.Satzung
{
    public class GetAllSatzungHandler : IRequestHandler<GetAllSatzungQuery, IEnumerable<SatzungDto>>
    {
        private readonly ISatzungReadService _readService;
        private readonly IMapper _mapper;

        public GetAllSatzungHandler(ISatzungReadService readService, IMapper mapper)
        {
            _readService = readService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SatzungDto>> Handle(GetAllSatzungQuery request, CancellationToken cancellationToken)
        {
            var satzungen = await _readService.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<SatzungDto>>(satzungen);
        }
    }
}