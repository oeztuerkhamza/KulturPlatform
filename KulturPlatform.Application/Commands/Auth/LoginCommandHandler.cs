using KulturPlatform.Application.Dtos.AuthDto;
using KulturPlatform.Application.Interfaces.Admin;
using KulturPlatform.Application.Interfaces.Auth;
using KulturPlatform.Domain.Interfaces;
using MediatR;

namespace KulturPlatform.Application.Commands.Auth
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponseDto>
    {
        private readonly IAdminRepository _adminRepository;
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;

        public LoginCommandHandler(IAdminRepository adminRepository, ITokenService tokenService, IUnitOfWork unitOfWork)
        {
            _adminRepository = adminRepository;
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
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
            _adminRepository.Update(admin, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

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
