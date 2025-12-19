using KulturPlatform.Application.Dtos.Dashboard;
using KulturPlatform.Application.Interfaces.Dashboard;
using MediatR;

namespace KulturPlatform.Application.Queries.Dashboard
{
    public class GetDashboardStatsQueryHandler : IRequestHandler<GetDashboardStatsQuery, DashboardStatsDto>
    {
        private readonly IDashboardService _dashboardService;

        public GetDashboardStatsQueryHandler(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public async Task<DashboardStatsDto> Handle(GetDashboardStatsQuery request, CancellationToken cancellationToken)
        {
            return await _dashboardService.GetStatsAsync(cancellationToken);
        }
    }
}
