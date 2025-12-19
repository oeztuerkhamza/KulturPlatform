using KulturPlatform.Application.Interfaces.Admin;
using KulturPlatform.Domain.Commons.ValueObjects;
using MediatR;

namespace KulturPlatform.Application.Commands.Admin
{
    public class UpdateAdminCommandHandler : IRequestHandler<UpdateAdminCommand>
    {
        private readonly IAdminRepository _adminRepository;

        public UpdateAdminCommandHandler(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public async Task Handle(UpdateAdminCommand request, CancellationToken cancellationToken)
        {
            var admin = await _adminRepository.GetByIdAsync(request.Id, cancellationToken);
            
            if (admin == null)
                throw new KeyNotFoundException($"Admin with Id {request.Id} not found.");

            admin.UpdateEmail(new Email(request.Email));
            admin.UpdateName(new Name(request.Name));

            _adminRepository.Update(admin, cancellationToken);

            await Task.CompletedTask;
        }
    }
}
