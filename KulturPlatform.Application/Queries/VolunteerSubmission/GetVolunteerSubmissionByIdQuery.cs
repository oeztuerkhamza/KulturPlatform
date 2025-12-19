using KulturPlatform.Application.Dtos;
using MediatR;

namespace KulturPlatform.Application.Queries.VolunteerSubmission
{
    public record GetVolunteerSubmissionByIdQuery(Guid Id) : IRequest<VolunteerSubmissionDto?>;
}
