using AutoMapper;
using KulturPlatform.Application.Dtos;
using KulturPlatform.Application.Interfaces.AboutUs;
using MediatR;

namespace KulturPlatform.Application.Queries.AboutUs;

public class GetAboutUsTeamQueryHandler : IRequestHandler<GetAboutUsTeamQuery, List<TeamMemberDto>>
{
    private readonly ITeamMemberRepository _teamMemberRepository;
    private readonly IMapper _mapper;

    public GetAboutUsTeamQueryHandler(ITeamMemberRepository teamMemberRepository, IMapper mapper)
    {
        _teamMemberRepository = teamMemberRepository;
        _mapper = mapper;
    }

    public async Task<List<TeamMemberDto>> Handle(GetAboutUsTeamQuery request, CancellationToken cancellationToken)
    {
        var teamMembers = await _teamMemberRepository.GetAllAsync(cancellationToken);
        return _mapper.Map<List<TeamMemberDto>>(teamMembers);
    }
}
