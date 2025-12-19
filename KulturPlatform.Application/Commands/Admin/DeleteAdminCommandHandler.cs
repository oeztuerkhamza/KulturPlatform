using KulturPlatform.Application.Interfaces.Admin;
using MediatR;

namespace KulturPlatform.Application.Commands.Admin
{
    public class DeleteAdminCommandHandler : IRequestHandler<DeleteAdminCommand>
    {
        private readonly IAdminRepository _adminRepository;

        public DeleteAdminCommandHandler(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public async Task Handle(DeleteAdminCommand request, CancellationToken cancellationToken)
        {
            var admin = await _adminRepository.GetByIdAsync(request.AdminId, cancellationToken);
            
            if (admin == null)
                throw new KeyNotFoundException($"Admin with Id {request.AdminId} not found.");

            _adminRepository.Delete(admin, cancellationToken);

            await Task.CompletedTask;
        }
    }
}
