using KulturPlatform.Application.Interfaces.Admin;
using MediatR;

namespace KulturPlatform.Application.Commands.Admin
{
    public class UpdateAdminRoleCommandHandler : IRequestHandler<UpdateAdminRoleCommand>
    {
        private readonly IAdminRepository _adminRepository;

        public UpdateAdminRoleCommandHandler(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public async Task Handle(UpdateAdminRoleCommand request, CancellationToken cancellationToken)
        {
            var admin = await _adminRepository.GetByIdAsync(request.AdminId, cancellationToken);
            
            if (admin == null)
                throw new KeyNotFoundException($"Admin with Id {request.AdminId} not found.");

            admin.UpdateRole(request.Role);

            _adminRepository.Update(admin, cancellationToken);

            await Task.CompletedTask;
        }
    }
}
