using KulturPlatform.Application.Interfaces;
using KulturPlatform.Application.Interfaces.ContactMessages;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;
using MediatR;

namespace KulturPlatform.Application.Commands.ContactMessages
{
    public class CreateContactMessageCommandHandler : IRequestHandler<CreateContactMessageCommand, Guid>
    {
        private readonly IContactMessageRepository _contactMessageRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;

        public CreateContactMessageCommandHandler(
            IContactMessageRepository contactMessageRepository,
            IUnitOfWork unitOfWork,
            IEmailService emailService)
        {
            _contactMessageRepository = contactMessageRepository;
            _unitOfWork = unitOfWork;
            _emailService = emailService;
        }

        public async Task<Guid> Handle(CreateContactMessageCommand request, CancellationToken cancellationToken)
        {
            var anrede = !string.IsNullOrWhiteSpace(request.Anrede)
                ? new Anrede(request.Anrede)
                : null;

            var senderName = new Name(request.SenderName);
            var email = new Email(request.Email);

            var phone = !string.IsNullOrWhiteSpace(request.Phone)
                ? new PhoneNumber(request.Phone)
                : null;

            var subject = new Subject(request.Subject);
            var message = new MessageText(request.Message);

            var contactMessage = Domain.Commons.Aggregates.ContactMessage.CreateNew(
                anrede,
                senderName,
                email,
                phone,
                subject,
                message);

            await _contactMessageRepository.AddAsync(contactMessage, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Send email notification
            await _emailService.SendContactMessageNotificationAsync(
                request.SenderName,
                request.Email,
                request.Phone,
                request.Subject,
                request.Message,
                cancellationToken);

            return contactMessage.Id;
        }
    }
}
