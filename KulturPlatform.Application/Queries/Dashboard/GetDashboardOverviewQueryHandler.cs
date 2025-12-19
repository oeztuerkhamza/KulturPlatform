using KulturPlatform.Application.Dtos.Dashboard;
using KulturPlatform.Application.Interfaces.Dashboard;
using MediatR;

namespace KulturPlatform.Application.Queries.Dashboard
{
    public class GetDashboardOverviewQueryHandler : IRequestHandler<GetDashboardOverviewQuery, DashboardOverviewDto>
    {
        private readonly IDashboardService _dashboardService;

        public GetDashboardOverviewQueryHandler(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public async Task<DashboardOverviewDto> Handle(GetDashboardOverviewQuery request, CancellationToken cancellationToken)
        {
            return await _dashboardService.GetOverviewAsync(request.RecentSubmissionsCount, cancellationToken);
        }
    }
}
