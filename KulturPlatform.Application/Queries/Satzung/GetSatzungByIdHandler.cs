using AutoMapper;
using KulturPlatform.Application.Dtos.SatzungDto;
using KulturPlatform.Application.Interfaces.Satzung;
using MediatR;

namespace KulturPlatform.Application.Queries.Satzung
{
    public class GetSatzungByIdHandler : IRequestHandler<GetSatzungByIdQuery, SatzungDto?>
    {
        private readonly ISatzungReadService _readService;
        private readonly IMapper _mapper;

        public GetSatzungByIdHandler(ISatzungReadService readService, IMapper mapper)
        {
            _readService = readService;
            _mapper = mapper;
        }

        public async Task<SatzungDto?> Handle(GetSatzungByIdQuery request, CancellationToken cancellationToken)
        {
            // Guid Id üzerinden read service kullanımı
            var satzung = await _readService.GetByIdAsync(request.Id, cancellationToken);

            if (satzung == null)
                return null;

            // AutoMapper ile DTO mapping
            return _mapper.Map<SatzungDto>(satzung);
        }
    }
}