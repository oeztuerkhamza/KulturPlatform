using KulturPlatform.Application.Dtos.AboutUs;
using MediatR;

namespace KulturPlatform.Application.Queries.AboutUs;

/// <summary>
/// Query to get About Us activity areas
/// </summary>
public record GetAboutUsActivityAreasQuery : IRequest<List<ActivityAreaDto>>;
