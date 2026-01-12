using AutoMapper;
using KulturPlatform.Application.Dtos;
using KulturPlatform.Application.Dtos.Dashboard;
using KulturPlatform.Application.Interfaces.Dashboard;
using Microsoft.EntityFrameworkCore;

namespace KulturPlatform.Infrastructure.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public DashboardService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DashboardStatsDto> GetStatsAsync(CancellationToken cancellationToken = default)
        {
            var stats = new DashboardStatsDto
            {
                // Activities
                TotalActivities = await _context.Activities.CountAsync(cancellationToken),
                ActiveActivities = await _context.Activities.CountAsync(a => a.IsActive, cancellationToken),
                UpcomingActivities = await _context.Activities
                    .CountAsync(a => a.IsActive && a.Date.DateIso > DateTime.UtcNow, cancellationToken),

                // Courses
                TotalCourses = await _context.Courses.CountAsync(cancellationToken),
                ActiveCourses = await _context.Courses.CountAsync(c => c.IsActive, cancellationToken),

                // Partners
                TotalPartners = await _context.Partners.CountAsync(cancellationToken),
                ActivePartners = await _context.Partners.CountAsync(p => p.IsActive, cancellationToken),

                // Team Members
                //TotalTeamMembers = await _context.TeamMembers.CountAsync(cancellationToken),
                //ActiveTeamMembers = await _context.TeamMembers.CountAsync(t => t.IsActive, cancellationToken),

                // Value Items
                TotalValueItems = await _context.ValueItems.CountAsync(cancellationToken),
                ActiveValueItems = await _context.ValueItems.CountAsync(v => v.IsActive, cancellationToken),

                // Volunteer Submissions
                TotalVolunteerSubmissions = await _context.VolunteerSubmissions.CountAsync(cancellationToken),

                // Admins
                TotalAdmins = await _context.Admins.CountAsync(cancellationToken),
                ActiveAdmins = await _context.Admins.CountAsync(a => a.IsActive, cancellationToken),

                LastUpdated = DateTime.UtcNow
            };

            return stats;
        }

        public async Task<DashboardOverviewDto> GetOverviewAsync(int recentSubmissionsCount = 10, CancellationToken cancellationToken = default)
        {
            // Get statistics
            var stats = await GetStatsAsync(cancellationToken);

            // Get recent volunteer submissions
            var recentSubmissions = await _context.VolunteerSubmissions
                .AsNoTracking()
                .OrderByDescending(v => v.SubmittedAt)
                .Take(recentSubmissionsCount)
                .ToListAsync(cancellationToken);

            var overview = new DashboardOverviewDto
            {
                Stats = stats,
                RecentSubmissions = _mapper.Map<List<VolunteerSubmissionDto>>(recentSubmissions)
            };

            return overview;
        }
    }
}
