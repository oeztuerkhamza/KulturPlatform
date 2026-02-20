using KulturPlatform.Application.Dtos.AboutUs;
using MediatR;

namespace KulturPlatform.Application.Queries.AboutUs;

/// <summary>
/// Query to get About Us values (Vision, Mission, CoreValues)
/// </summary>
public record GetAboutUsValuesQuery : IRequest<AboutUsValuesDto>;
