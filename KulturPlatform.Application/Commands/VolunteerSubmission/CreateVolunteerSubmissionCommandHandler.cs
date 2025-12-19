using KulturPlatform.Application.Interfaces.VolunteerSubmission;
using KulturPlatform.Domain.Commons.ValueObjects;
using MediatR;

namespace KulturPlatform.Application.Commands.VolunteerSubmission
{
    public class CreateVolunteerSubmissionCommandHandler : IRequestHandler<CreateVolunteerSubmissionCommand, Guid>
    {
        private readonly IVolunteerSubmissionRepository _volunteerSubmissionRepository;

        public CreateVolunteerSubmissionCommandHandler(IVolunteerSubmissionRepository volunteerSubmissionRepository)
        {
            _volunteerSubmissionRepository = volunteerSubmissionRepository;
        }

        public async Task<Guid> Handle(CreateVolunteerSubmissionCommand request, CancellationToken cancellationToken)
        {
            var fullName = new Name(request.FullName);
            var email = new Email(request.Email);
            var phoneNumber = new PhoneNumber(request.PhoneNumber);
            var message = new SubmissionMessage(request.Message);

            var submission = Domain.Commons.Aggregates.VolunteerSubmission.CreateNew(
                fullName, email, phoneNumber, message);

            await _volunteerSubmissionRepository.AddAsync(submission, cancellationToken);

            return submission.Id;
        }
    }
}
