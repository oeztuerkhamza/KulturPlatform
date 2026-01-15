using KulturPlatform.Application.Interfaces.Admin;
using KulturPlatform.Domain.Interfaces;
using MediatR;

namespace KulturPlatform.Application.Commands.Admin
{
    public class ActivateAdminCommandHandler : IRequestHandler<ActivateAdminCommand>
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ActivateAdminCommandHandler(IAdminRepository adminRepository, IUnitOfWork unitOfWork)
        {
            _adminRepository = adminRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(ActivateAdminCommand request, CancellationToken cancellationToken)
        {
            var admin = await _adminRepository.GetByIdAsync(request.AdminId, cancellationToken);

            if (admin == null)
                throw new KeyNotFoundException($"Admin with Id {request.AdminId} not found.");

            admin.Activate();

            _adminRepository.Update(admin, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await Task.CompletedTask;
        }
    }
}
