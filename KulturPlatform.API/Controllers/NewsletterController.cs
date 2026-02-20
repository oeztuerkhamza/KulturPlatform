using KulturPlatform.Application.Commands.Newsletter;
using KulturPlatform.Application.Dtos.Newsletter;
using KulturPlatform.Application.Queries.Newsletter;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KulturPlatform.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsletterController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<NewsletterController> _logger;

        public NewsletterController(IMediator mediator, ILogger<NewsletterController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Subscribe to newsletter (public endpoint)
        /// </summary>
        [HttpPost("subscribe")]
        [AllowAnonymous]
        public async Task<IActionResult> Subscribe([FromBody] SubscribeToNewsletterCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(new
            {
                success = true,
                message = result
                    ? "Verification email sent. Please check your inbox."
                    : "Email already subscribed and verified."
            });
        }

        /// <summary>
        /// Verify newsletter subscription (public endpoint)
        /// </summary>
        [HttpGet("verify")]
        [AllowAnonymous]
        public async Task<IActionResult> Verify([FromQuery] string token)
        {
            var command = new VerifyNewsletterSubscriptionCommand { Token = token };
            var result = await _mediator.Send(command);

            if (result)
            {
                return Ok(new
                {
                    success = true,
                    message = "Newsletter subscription verified successfully!"
                });
            }

            return NotFound(new
            {
                success = false,
                message = "Invalid verification link."
            });
        }

        /// <summary>
        /// Unsubscribe from newsletter (public endpoint)
        /// </summary>
        [HttpGet("unsubscribe")]
        [AllowAnonymous]
        public async Task<IActionResult> Unsubscribe([FromQuery] string token)
        {
            var command = new UnsubscribeFromNewsletterCommand { Token = token };
            var result = await _mediator.Send(command);

            if (result)
            {
                return Ok(new
                {
                    success = true,
                    message = "Successfully unsubscribed from newsletter."
                });
            }

            return NotFound(new
            {
                success = false,
                message = "Invalid unsubscribe link."
            });
        }

        /// <summary>
        /// Get all newsletter subscribers (admin only)
        /// </summary>
        [HttpGet("subscribers")]
        [Authorize] // System Admin or User Admin can access
        public async Task<IActionResult> GetSubscribers()
        {
            var query = new GetNewsletterSubscribersQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Get subscriber statistics (admin only)
        /// </summary>
        [HttpGet("subscribers/stats")]
        [Authorize] // System Admin or User Admin can access
        public async Task<IActionResult> GetSubscriberStats()
        {
            var query = new GetNewsletterStatsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Create new newsletter campaign (admin only)
        /// </summary>
        [HttpPost("campaigns")]
        [Authorize] // System Admin or User Admin can access
        public async Task<IActionResult> CreateCampaign([FromBody] CreateNewsletterCampaignCommand command)
        {
            var campaignId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetCampaign), new { id = campaignId }, new { id = campaignId });
        }

        /// <summary>
        /// Get all newsletter campaigns (admin only)
        /// </summary>
        [HttpGet("campaigns")]
        [Authorize] // System Admin or User Admin can access
        public async Task<IActionResult> GetCampaigns()
        {
            var query = new GetNewsletterCampaignsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Get newsletter campaign by ID (admin only)
        /// </summary>
        [HttpGet("campaigns/{id}")]
        [Authorize] // System Admin or User Admin can access
        public async Task<IActionResult> GetCampaign(Guid id)
        {
            var query = new GetNewsletterCampaignByIdQuery { Id = id };
            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Update newsletter campaign (admin only)
        /// </summary>
        [HttpPut("campaigns/{id}")]
        [Authorize] // System Admin or User Admin can access
        public async Task<IActionResult> UpdateCampaign(Guid id, [FromBody] UpdateNewsletterCampaignCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID mismatch");

            await _mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Send newsletter campaign (admin only)
        /// </summary>
        [HttpPost("campaigns/{id}/send")]
        [Authorize] // System Admin or User Admin can access
        public async Task<IActionResult> SendCampaign(Guid id)
        {
            try
            {
                var command = new SendNewsletterCampaignCommand { CampaignId = id };
                var result = await _mediator.Send(command);

                return Ok(new
                {
                    success = true,
                    message = "Campaign sent successfully",
                    successful = result.successful,
                    failed = result.failed,
                    total = result.successful + result.failed
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending newsletter campaign {CampaignId}", id);
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Send test newsletter email (admin only)
        /// </summary>
        [HttpPost("campaigns/{id}/test")]
        [Authorize] // System Admin or User Admin can access
        public async Task<IActionResult> SendTestEmail(Guid id, [FromBody] SendTestNewsletterCommand command)
        {
            if (id != command.CampaignId)
                return BadRequest("ID mismatch");

            await _mediator.Send(command);
            return Ok(new { success = true, message = "Test email sent successfully" });
        }

        /// <summary>
        /// Delete newsletter campaign (admin only)
        /// </summary>
        [HttpDelete("campaigns/{id}")]
        [Authorize] // System Admin or User Admin can access
        public async Task<IActionResult> DeleteCampaign(Guid id)
        {
            var command = new DeleteNewsletterCampaignCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
