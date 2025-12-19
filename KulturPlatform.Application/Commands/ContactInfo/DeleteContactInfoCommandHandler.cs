using KulturPlatform.Application.Interfaces.ContactInfo;
using KulturPlatform.Domain.Interfaces;
using MediatR;

namespace KulturPlatform.Application.Commands.ContactInfo
{
    public class DeleteContactInfoCommandHandler
        : IRequestHandler<DeleteContactInfoCommand, bool>
    {
        private readonly IContactInfoRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteContactInfoCommandHandler(
            IContactInfoRepository repository,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(
            DeleteContactInfoCommand request,
            CancellationToken cancellationToken)
        {
            var contactInfo = await _repository
                .GetByIdAsync(request.Id, cancellationToken);

            if (contactInfo is null)
                return false;

            contactInfo.Delete(); // ✅ domain behavior

            _repository.Update(contactInfo, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }

    }
}