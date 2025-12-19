using AutoMapper;
using KulturPlatform.Application.Dtos.ImprintDto;
using KulturPlatform.Application.Interfaces.Imprint;
using MediatR;
using System.Linq;

namespace KulturPlatform.Application.Queries.Imprint
{
    public class GetImprintHandler : IRequestHandler<GetImprintQuery, ImprintDto?>
    {
        private readonly IImprintReadService _readService;
        private readonly IMapper _mapper;

        public GetImprintHandler(IImprintReadService readService, IMapper mapper)
        {
            _readService = readService;
            _mapper = mapper;
        }

        public async Task<ImprintDto?> Handle(GetImprintQuery request, CancellationToken cancellationToken)
        {
            // ReadService returns entity, not DTO - ReadService should be fixed
            var imprints = await _readService.GetAllAsync(cancellationToken);
            var imprint = imprints.FirstOrDefault();
            
            return imprint != null ? _mapper.Map<ImprintDto>(imprint) : null;
        }
    }
}