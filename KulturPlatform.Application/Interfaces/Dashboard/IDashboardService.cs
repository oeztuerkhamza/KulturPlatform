using KulturPlatform.Application.Dtos.Dashboard;

namespace KulturPlatform.Application.Interfaces.Dashboard
{
    public interface IDashboardService
    {
        Task<DashboardStatsDto> GetStatsAsync(CancellationToken cancellationToken = default);
        Task<DashboardOverviewDto> GetOverviewAsync(int recentSubmissionsCount = 10, CancellationToken cancellationToken = default);
    }
}
