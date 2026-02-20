using AutoMapper;
using KulturPlatform.Application.Dtos.AboutUs;
using KulturPlatform.Application.Interfaces.AboutUs;
using MediatR;

namespace KulturPlatform.Application.Queries.AboutUs;

public class GetAboutUsFocusAreasQueryHandler : IRequestHandler<GetAboutUsFocusAreasQuery, List<FocusAreaDto>>
{
    private readonly IFocusAreaRepository _focusAreaRepository;
    private readonly IMapper _mapper;

    public GetAboutUsFocusAreasQueryHandler(IFocusAreaRepository focusAreaRepository, IMapper mapper)
    {
        _focusAreaRepository = focusAreaRepository;
        _mapper = mapper;
    }

    public async Task<List<FocusAreaDto>> Handle(GetAboutUsFocusAreasQuery request, CancellationToken cancellationToken)
    {
        var focusAreas = await _focusAreaRepository.GetAllAsync(cancellationToken);
        return _mapper.Map<List<FocusAreaDto>>(focusAreas);
    }
}
