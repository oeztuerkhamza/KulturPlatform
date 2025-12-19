using MediatR;

namespace KulturPlatform.Application.Commands.VolunteerSubmission
{
    public record CreateVolunteerSubmissionCommand(
        string FullName,
        string Email,
        string PhoneNumber,
        string Message
    ) : IRequest<Guid>;
}
