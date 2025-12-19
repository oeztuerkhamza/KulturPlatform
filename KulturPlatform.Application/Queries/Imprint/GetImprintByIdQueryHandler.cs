using AutoMapper;
using KulturPlatform.Application.Dtos.ImprintDto;
using KulturPlatform.Application.Interfaces.Imprint;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace KulturPlatform.Application.Queries.Imprint
{

    public class GetImprintByIdQueryHandler : IRequestHandler<GetImprintByIdQuery, ImprintDto>
    {
        private readonly IImprintReadService _readService;
        private readonly IMapper _mapper;
        public GetImprintByIdQueryHandler(IImprintReadService readService, IMapper mapper)
        {
            _readService = readService;
            _mapper = mapper;
        }
        public async Task<ImprintDto> Handle(GetImprintByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _readService.GetByIdAsync(request.Id, cancellationToken);
            if (entity == null)
                throw new SecurityTokenException("Imprint not found");
            return _mapper.Map<ImprintDto>(entity);
        }
    }
}
