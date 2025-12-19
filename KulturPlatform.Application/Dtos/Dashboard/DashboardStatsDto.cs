namespace KulturPlatform.Application.Dtos.Dashboard
{
    public class DashboardStatsDto
    {
        public int TotalActivities { get; set; }
        public int ActiveActivities { get; set; }
        public int UpcomingActivities { get; set; }
        
        public int TotalCourses { get; set; }
        public int ActiveCourses { get; set; }
        
        public int TotalPartners { get; set; }
        public int ActivePartners { get; set; }
        
        public int TotalTeamMembers { get; set; }
        public int ActiveTeamMembers { get; set; }
        
        public int TotalValueItems { get; set; }
        public int ActiveValueItems { get; set; }
        
        public int TotalVolunteerSubmissions { get; set; }
        public int TotalAdmins { get; set; }
        public int ActiveAdmins { get; set; }
        
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
    }
}
