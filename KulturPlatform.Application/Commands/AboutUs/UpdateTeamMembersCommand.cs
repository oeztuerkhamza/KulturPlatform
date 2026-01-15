using KulturPlatform.Application.Dtos;
using MediatR;

namespace KulturPlatform.Application.Commands.AboutUs
{
    public record UpdateTeamMembersCommand(
        List<TeamMemberDto> TeamMembers
    ) : IRequest;
}
