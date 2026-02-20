using KulturPlatform.Application.Dtos.AboutUs;
using MediatR;

namespace KulturPlatform.Application.Queries.AboutUs;

/// <summary>
/// Query to get About Us focus areas
/// </summary>
public record GetAboutUsFocusAreasQuery : IRequest<List<FocusAreaDto>>;
