using KulturPlatform.Application.Interfaces.Admin;
using KulturPlatform.Domain.Commons.Constants;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;
using MediatR;

namespace KulturPlatform.Application.Commands.Admin
{
    public class CreateAdminCommandHandler : IRequestHandler<CreateAdminCommand, Guid>
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateAdminCommandHandler(IAdminRepository adminRepository, IUnitOfWork unitOfWork)
        {
            _adminRepository = adminRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateAdminCommand request, CancellationToken cancellationToken)
        {
            // Validate role
            if (!Roles.IsValid(request.Role))
                throw new ArgumentException($"Invalid role: {request.Role}");

            // Check if email already exists
            var existingAdmin = await _adminRepository.GetByEmailAsync(request.Email, cancellationToken);
            if (existingAdmin != null)
                throw new InvalidOperationException($"Admin with email {request.Email} already exists.");

            var admin = Domain.Commons.Aggregates.Admin.CreateNew(
                email: new Email(request.Email),
                password: Password.Create(request.Password),
                name: new Name(request.Name),
                role: request.Role
            );

            await _adminRepository.AddAsync(admin, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return admin.Id;
        }
    }
}
