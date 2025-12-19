using KulturPlatform.Application.Dtos;
using KulturPlatform.Application.Interfaces.VolunteerSubmission;
using MediatR;

namespace KulturPlatform.Application.Queries.VolunteerSubmission
{
    public class GetAllVolunteerSubmissionsQueryHandler : IRequestHandler<GetAllVolunteerSubmissionsQuery, IEnumerable<VolunteerSubmissionDto>>
    {
        private readonly IVolunteerSubmissionReadService _volunteerSubmissionReadService;

        public GetAllVolunteerSubmissionsQueryHandler(IVolunteerSubmissionReadService volunteerSubmissionReadService)
        {
            _volunteerSubmissionReadService = volunteerSubmissionReadService;
        }

        public async Task<IEnumerable<VolunteerSubmissionDto>> Handle(GetAllVolunteerSubmissionsQuery request, CancellationToken cancellationToken)
        {
            return await _volunteerSubmissionReadService.GetAllAsync();
        }
    }
}
