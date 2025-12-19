using KulturPlatform.Application.Interfaces.VolunteerSubmission;
using MediatR;

namespace KulturPlatform.Application.Commands.VolunteerSubmission
{
    public class DeleteVolunteerSubmissionCommandHandler : IRequestHandler<DeleteVolunteerSubmissionCommand>
    {
        private readonly IVolunteerSubmissionRepository _volunteerSubmissionRepository;

        public DeleteVolunteerSubmissionCommandHandler(IVolunteerSubmissionRepository volunteerSubmissionRepository)
        {
            _volunteerSubmissionRepository = volunteerSubmissionRepository;
        }

        public async Task Handle(DeleteVolunteerSubmissionCommand request, CancellationToken cancellationToken)
        {
            var submission = await _volunteerSubmissionRepository.GetByIdAsync(request.Id, cancellationToken);
            if (submission == null)
                throw new KeyNotFoundException($"VolunteerSubmission with Id {request.Id} not found.");

            _volunteerSubmissionRepository.Delete(submission, cancellationToken);
        }
    }
}
