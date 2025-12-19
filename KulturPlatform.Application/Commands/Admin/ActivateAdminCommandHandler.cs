using KulturPlatform.Application.Interfaces.Admin;
using MediatR;

namespace KulturPlatform.Application.Commands.Admin
{
    public class ActivateAdminCommandHandler : IRequestHandler<ActivateAdminCommand>
    {
        private readonly IAdminRepository _adminRepository;

        public ActivateAdminCommandHandler(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public async Task Handle(ActivateAdminCommand request, CancellationToken cancellationToken)
        {
            var admin = await _adminRepository.GetByIdAsync(request.AdminId, cancellationToken);
            
            if (admin == null)
                throw new KeyNotFoundException($"Admin with Id {request.AdminId} not found.");

            admin.Activate();

            _adminRepository.Update(admin, cancellationToken);

            await Task.CompletedTask;
        }
    }
}
