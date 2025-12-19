using AutoMapper;
using KulturPlatform.Application.Dtos;
using KulturPlatform.Application.Interfaces.VolunteerSubmission;
using MediatR;

namespace KulturPlatform.Application.Queries.VolunteerSubmission
{
    public class GetVolunteerSubmissionByIdQueryHandler : IRequestHandler<GetVolunteerSubmissionByIdQuery, VolunteerSubmissionDto?>
    {
        private readonly IVolunteerSubmissionRepository _volunteerSubmissionRepository;
        private readonly IMapper _mapper;

        public GetVolunteerSubmissionByIdQueryHandler(IVolunteerSubmissionRepository volunteerSubmissionRepository, IMapper mapper)
        {
            _volunteerSubmissionRepository = volunteerSubmissionRepository;
            _mapper = mapper;
        }

        public async Task<VolunteerSubmissionDto?> Handle(GetVolunteerSubmissionByIdQuery request, CancellationToken cancellationToken)
        {
            var submission = await _volunteerSubmissionRepository.GetByIdAsync(request.Id, cancellationToken);
            if (submission == null) return null;

            return _mapper.Map<VolunteerSubmissionDto>(submission);
        }
    }
}
