using KulturPlatform.Application.Interfaces.ContactMessages;
using KulturPlatform.Domain.Interfaces;
using MediatR;

namespace KulturPlatform.Application.Commands.ContactMessages
{
    public class DeleteContactMessageCommandHandler : IRequestHandler<DeleteContactMessageCommand>
    {
        private readonly IContactMessageRepository _contactMessageRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteContactMessageCommandHandler(
            IContactMessageRepository contactMessageRepository,
            IUnitOfWork unitOfWork)
        {
            _contactMessageRepository = contactMessageRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteContactMessageCommand request, CancellationToken cancellationToken)
        {
            var message = await _contactMessageRepository.GetByIdAsync(request.Id, cancellationToken);

            if (message == null)
                throw new KeyNotFoundException($"Contact message with ID {request.Id} not found.");

            _contactMessageRepository.Delete(message);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
