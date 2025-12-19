using MediatR;

namespace KulturPlatform.Application.Commands.VolunteerSubmission
{
    public record DeleteVolunteerSubmissionCommand(Guid Id) : IRequest;
}
