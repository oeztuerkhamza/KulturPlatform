using AutoMapper;
using KulturPlatform.Application.Dtos.SatzungDto;
using KulturPlatform.Application.Interfaces.Satzung;
using MediatR;

namespace KulturPlatform.Application.Queries.Satzung
{
    public class GetSatzungByKeyHandler : IRequestHandler<GetSatzungByKeyQuery, SatzungDto?>
    {
        private readonly ISatzungReadService _readService;
        private readonly IMapper _mapper;

        public GetSatzungByKeyHandler(ISatzungReadService readService, IMapper mapper)
        {
            _readService = readService;
            _mapper = mapper;
        }

        public async Task<SatzungDto?> Handle(GetSatzungByKeyQuery request, CancellationToken cancellationToken)
        {
            var satzung = await _readService.GetByKeyAsync(request.Key, cancellationToken);
            return satzung != null ? _mapper.Map<SatzungDto>(satzung) : null;
        }
    }
}