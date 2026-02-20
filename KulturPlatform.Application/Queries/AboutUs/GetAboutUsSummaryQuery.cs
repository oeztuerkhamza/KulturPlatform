using KulturPlatform.Application.Dtos.AboutUs;
using MediatR;

namespace KulturPlatform.Application.Queries.AboutUs;

/// <summary>
/// Query to get About Us summary (Quote, WhoWeAre, Goals)
/// </summary>
public record GetAboutUsSummaryQuery : IRequest<AboutUsSummaryDto>;
