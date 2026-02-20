using KulturPlatform.Application.Interfaces.Admin;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace KulturPlatform.Application.Commands.Auth
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand>
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ChangePasswordCommandHandler> _logger;

        public ChangePasswordCommandHandler(IAdminRepository adminRepository, IUnitOfWork unitOfWork, ILogger<ChangePasswordCommandHandler> logger)
        {
            _adminRepository = adminRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var admin = await _adminRepository.GetByIdAsync(request.AdminId, cancellationToken);

            if (admin == null)
                throw new KeyNotFoundException($"Admin with Id {request.AdminId} not found.");

            if (!admin.Password.Verify(request.CurrentPassword))
            {
                throw new UnauthorizedAccessException("Current password is incorrect.");
            }


            var newPassword = Password.Create(request.NewPassword);
            admin.UpdatePassword(newPassword);

            _adminRepository.Update(admin, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
