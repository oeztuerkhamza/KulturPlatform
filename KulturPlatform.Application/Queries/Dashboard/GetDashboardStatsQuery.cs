using KulturPlatform.Application.Dtos.Dashboard;
using MediatR;

namespace KulturPlatform.Application.Queries.Dashboard
{
    public record GetDashboardStatsQuery() : IRequest<DashboardStatsDto>;
}
