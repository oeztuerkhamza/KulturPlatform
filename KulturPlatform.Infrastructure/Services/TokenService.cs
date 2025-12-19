using KulturPlatform.Application.Interfaces.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace KulturPlatform.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<TokenService> _logger;

        public TokenService(IConfiguration configuration, ILogger<TokenService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public string GenerateToken(Guid userId, string userEmail, string role)
        {
            try
            {
                var jwtSettings = _configuration.GetSection("JwtSettings");
                var secretKey = jwtSettings["SecretKey"];
                var issuer = jwtSettings["Issuer"];
                var audience = jwtSettings["Audience"];
                var expirationHours = jwtSettings["ExpirationHours"];

                _logger.LogInformation("🔑 Generating JWT token for user: {UserId}, Email: {Email}, Role: {Role}", userId, userEmail, role);
                _logger.LogInformation("🔧 JWT Config - Issuer: {Issuer}, Audience: {Audience}, ExpirationHours: {ExpirationHours}", issuer, audience, expirationHours);

                if (string.IsNullOrEmpty(secretKey))
                {
                    _logger.LogError("❌ JWT SecretKey is null or empty!");
                    throw new InvalidOperationException("JWT SecretKey not configured");
                }

                if (string.IsNullOrEmpty(issuer))
                {
                    _logger.LogError("❌ JWT Issuer is null or empty!");
                    throw new InvalidOperationException("JWT Issuer not configured");
                }

                if (string.IsNullOrEmpty(audience))
                {
                    _logger.LogError("❌ JWT Audience is null or empty!");
                    throw new InvalidOperationException("JWT Audience not configured");
                }

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                    new Claim(ClaimTypes.Email, userEmail),
                    new Claim(ClaimTypes.Role, role),
                    new Claim("role", role),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, userId.ToString())
                };

                var token = new JwtSecurityToken(
                    issuer: issuer,
                    audience: audience,
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(Convert.ToDouble(expirationHours ?? "24")),
                    signingCredentials: credentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
                _logger.LogInformation("✅ JWT token generated successfully. Token length: {Length} characters", tokenString.Length);
                
                return tokenString;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Error generating JWT token for user: {UserId}", userId);
                throw;
            }
        }

        public Guid? ValidateToken(string token)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"] ?? throw new InvalidOperationException("JWT SecretKey not configured");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidateAudience = true,
                    ValidAudience = jwtSettings["Audience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                var principal = tokenHandler.ValidateToken(token, validationParameters, out _);
                var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier);

                if (Guid.TryParse(userIdClaim?.Value, out var userId))
                    return userId;

                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}
