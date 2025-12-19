using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace KulturPlatform.API.Authorization
{
    /// <summary>
    /// Authorization requirement that checks if user belongs to one of the specified groups/roles
    /// </summary>
    public class GroupRequirement : IAuthorizationRequirement
    {
        public string[] AllowedRoles { get; }

        public GroupRequirement(params string[] allowedRoles)
        {
            AllowedRoles = allowedRoles ?? Array.Empty<string>();
        }
    }

    /// <summary>
    /// Handler for GroupRequirement - checks user's role claims
    /// Best Practice: Checks multiple claim types for compatibility
    /// </summary>
    public class GroupRequirementHandler : AuthorizationHandler<GroupRequirement>
    {
        private readonly ILogger<GroupRequirementHandler> _logger;

        public GroupRequirementHandler(ILogger<GroupRequirementHandler> logger)
        {
            _logger = logger;
        }

        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            GroupRequirement requirement)
        {
            var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            // ?? DEBUG: Tüm claim'leri logla
            var allClaims = string.Join(", ", context.User.Claims.Select(c => $"{c.Type}={c.Value}"));
            _logger.LogWarning("?? User {UserId} claims: {Claims}", userId, allClaims);

            // Get user roles from various claim types (for compatibility)
            var userRoles = context.User.Claims
                .Where(c => c.Type == ClaimTypes.Role || 
                           c.Type == "role" ||
                           c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")
                .Select(c => c.Value)
                .Distinct()
                .ToList();

            if (userRoles.Count == 0)
            {
                _logger.LogWarning("? User {UserId} has NO roles assigned", userId);
                return Task.CompletedTask;
            }

            _logger.LogInformation("?? User {UserId} has roles: [{UserRoles}]", userId, string.Join(", ", userRoles));
            _logger.LogInformation("?? Required roles: [{RequiredRoles}]", string.Join(", ", requirement.AllowedRoles));

            // Check if user has any of the required roles
            var hasRequiredRole = requirement.AllowedRoles.Any(requiredRole =>
                userRoles.Contains(requiredRole, StringComparer.OrdinalIgnoreCase));

            if (hasRequiredRole)
            {
                _logger.LogInformation("? User {UserId} authorized with roles: {UserRoles}", 
                    userId,
                    string.Join(", ", userRoles));
                context.Succeed(requirement);
            }
            else
            {
                _logger.LogWarning("? User {UserId} with roles [{UserRoles}] does not have required roles [{RequiredRoles}]",
                    userId,
                    string.Join(", ", userRoles),
                    string.Join(", ", requirement.AllowedRoles));
            }

            return Task.CompletedTask;
        }
    }
}
