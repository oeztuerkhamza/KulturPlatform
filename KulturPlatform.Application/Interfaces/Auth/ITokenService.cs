namespace KulturPlatform.Application.Interfaces.Auth
{
    public interface ITokenService
    {
        string GenerateToken(Guid userId, string userEmail, string role);
        Guid? ValidateToken(string token);
    }
}
