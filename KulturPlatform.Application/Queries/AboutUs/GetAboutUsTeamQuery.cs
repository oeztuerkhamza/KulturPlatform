using KulturPlatform.Application.Dtos;
using MediatR;

namespace KulturPlatform.Application.Queries.AboutUs;

/// <summary>
/// Query to get About Us team members
/// </summary>
public record GetAboutUsTeamQuery : IRequest<List<TeamMemberDto>>;
