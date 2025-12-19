using KulturPlatform.Application.Interfaces.Admin;
using MediatR;

namespace KulturPlatform.Application.Commands.Admin
{
    public class DeactivateAdminCommandHandler : IRequestHandler<DeactivateAdminCommand>
    {
        private readonly IAdminRepository _adminRepository;

        public DeactivateAdminCommandHandler(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public async Task Handle(DeactivateAdminCommand request, CancellationToken cancellationToken)
        {
            var admin = await _adminRepository.GetByIdAsync(request.AdminId, cancellationToken);
            
            if (admin == null)
                throw new KeyNotFoundException($"Admin with Id {request.AdminId} not found.");

            admin.Deactivate();

            _adminRepository.Update(admin, cancellationToken);

            await Task.CompletedTask;
        }
    }
}
