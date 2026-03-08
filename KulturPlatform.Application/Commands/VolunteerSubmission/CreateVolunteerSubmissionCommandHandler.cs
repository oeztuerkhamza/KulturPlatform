using KulturPlatform.Application.Interfaces;
using KulturPlatform.Application.Interfaces.VolunteerSubmission;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace KulturPlatform.Application.Commands.VolunteerSubmission
{
    public class CreateVolunteerSubmissionCommandHandler : IRequestHandler<CreateVolunteerSubmissionCommand, Guid>
    {
        private readonly IVolunteerSubmissionRepository _volunteerSubmissionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;
        private readonly ILogger<CreateVolunteerSubmissionCommandHandler> _logger;

        public CreateVolunteerSubmissionCommandHandler(
            IVolunteerSubmissionRepository volunteerSubmissionRepository,
            IUnitOfWork unitOfWork,
            IEmailService emailService,
            ILogger<CreateVolunteerSubmissionCommandHandler> logger)
        {
            _volunteerSubmissionRepository = volunteerSubmissionRepository;
            _unitOfWork = unitOfWork;
            _emailService = emailService;
            _logger = logger;
        }

        public async Task<Guid> Handle(CreateVolunteerSubmissionCommand request, CancellationToken cancellationToken)
        {
            var fullName = new Name(request.FullName);
            var email = new Email(request.Email);
            var phoneNumber = !string.IsNullOrWhiteSpace(request.PhoneNumber)
                ? new PhoneNumber(request.PhoneNumber)
                : null;
            var message = new SubmissionMessage(request.Message);

            var submission = Domain.Commons.Aggregates.VolunteerSubmission.CreateNew(
                fullName, email, phoneNumber, message);

            await _volunteerSubmissionRepository.AddAsync(submission, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Send email notification — failure must never block the user-facing response
            try
            {
                await _emailService.SendVolunteerSubmissionNotificationAsync(
                    request.FullName,
                    request.Email,
                    request.PhoneNumber,
                    request.Message,
                    cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Email notification failed for volunteer submission {Id}. Submission was saved successfully.",
                    submission.Id);
            }

            return submission.Id;
        }
    }
}
