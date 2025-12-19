namespace KulturPlatform.Application.Dtos.Dashboard
{
    public class DashboardOverviewDto
    {
        public DashboardStatsDto Stats { get; set; } = new();
        public List<VolunteerSubmissionDto> RecentSubmissions { get; set; } = new();
    }
}
