using KulturPlatform.Application.Queries.Dashboard;
using KulturPlatform.Domain.Commons.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KulturPlatform.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // All dashboard endpoints require authentication
    public class DashboardController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DashboardController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get dashboard statistics
        /// </summary>
        /// <remarks>
        /// Returns summary statistics for all entities in the system.
        /// Accessible by UserAdmin and SystemAdmin roles.
        /// </remarks>
        [HttpGet("stats")]
        [Authorize(Policy = Policies.RequireUserAdmin)]
        public async Task<IActionResult> GetStats()
        {
            var query = new GetDashboardStatsQuery();
            var stats = await _mediator.Send(query);
            return Ok(stats);
        }

        /// <summary>
        /// Get dashboard overview with stats and recent submissions
        /// </summary>
        /// <param name="recentCount">Number of recent volunteer submissions to return (default: 10)</param>
        /// <remarks>
        /// Returns complete dashboard overview including:
        /// - All statistics
        /// - Recent volunteer submissions
        /// 
        /// Accessible by UserAdmin and SystemAdmin roles.
        /// </remarks>
        [HttpGet("overview")]
        [Authorize(Policy = Policies.RequireUserAdmin)]
        public async Task<IActionResult> GetOverview([FromQuery] int recentCount = 10)
        {
            var query = new GetDashboardOverviewQuery(recentCount);
            var overview = await _mediator.Send(query);
            return Ok(overview);
        }
    }
}
