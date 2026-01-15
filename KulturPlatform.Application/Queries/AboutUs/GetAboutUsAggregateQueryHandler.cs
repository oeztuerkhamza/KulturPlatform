using AutoMapper;
using KulturPlatform.Application.Dtos.AboutUs;
using KulturPlatform.Application.Interfaces.AboutUs;
using MediatR;

namespace KulturPlatform.Application.Queries.AboutUs;

public class GetAboutUsAggregateQueryHandler : IRequestHandler<GetAboutUsAggregateQuery, AboutUsAggregateDto>
{
    private readonly IAboutUsAggregateReadService _readService;
    private readonly IMapper _mapper;

    public GetAboutUsAggregateQueryHandler(IAboutUsAggregateReadService readService, IMapper mapper)
    {
        _readService = readService;
        _mapper = mapper;
    }

    public async Task<AboutUsAggregateDto> Handle(GetAboutUsAggregateQuery request, CancellationToken cancellationToken)
    {
        var aggregate = await _readService.GetAboutUsAggregateAsync(cancellationToken);
        return _mapper.Map<AboutUsAggregateDto>(aggregate);
    }
}
