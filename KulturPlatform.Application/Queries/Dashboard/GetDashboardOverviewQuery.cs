using KulturPlatform.Application.Dtos.Dashboard;
using MediatR;

namespace KulturPlatform.Application.Queries.Dashboard
{
    public record GetDashboardOverviewQuery(int RecentSubmissionsCount = 10) : IRequest<DashboardOverviewDto>;
}
