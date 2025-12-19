using KulturPlatform.Application.Dtos;
using MediatR;

namespace KulturPlatform.Application.Queries.VolunteerSubmission
{
    public record GetAllVolunteerSubmissionsQuery() : IRequest<IEnumerable<VolunteerSubmissionDto>>;
}
