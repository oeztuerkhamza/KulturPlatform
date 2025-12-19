using KulturPlatform.Application.Dtos.AuthDto;
using KulturPlatform.Application.Interfaces.Admin;
using KulturPlatform.Application.Interfaces.Auth;
using MediatR;

namespace KulturPlatform.Application.Commands.Auth
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponseDto>
    {
        private readonly IAdminRepository _adminRepository;
        private readonly ITokenService _tokenService;

        public LoginCommandHandler(IAdminRepository adminRepository, ITokenService tokenService)
        {
            _adminRepository = adminRepository;
            _tokenService = tokenService;
        }

        public async Task<LoginResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var admin = await _adminRepository.GetByEmailAsync(request.Email, cancellationToken);
            
            if (admin == null)
                throw new UnauthorizedAccessException("Invalid email or password.");

            if (!admin.IsActive)
                throw new UnauthorizedAccessException("Account is deactivated.");

            if (!admin.Password.Verify(request.Password))
                throw new UnauthorizedAccessException("Invalid email or password.");

            admin.UpdateLastLogin();

            var token = _tokenService.GenerateToken(admin.Id, admin.Email.Value, admin.Role);

            return new LoginResponseDto
            {
                Token = token,
                Email = admin.Email.Value,
                Name = admin.Name?.Value,
                Role = admin.Role,
                ExpiresAt = DateTime.UtcNow.AddHours(24)
            };
        }
    }
}
