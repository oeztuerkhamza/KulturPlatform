using AutoMapper;
using KulturPlatform.Application.Dtos.Activity;
using KulturPlatform.Application.Interfaces.Activity;
using MediatR;

namespace KulturPlatform.Application.Queries.Activity
{
    public class GetActivityByIdQueryHandler : IRequestHandler<GetActivityByIdQuery, ActivityDto?>
    {
        private readonly IActivityRepository _activityRepository;
        private readonly IMapper _mapper;

        public GetActivityByIdQueryHandler(IActivityRepository activityRepository, IMapper mapper)
        {
            _activityRepository = activityRepository;
            _mapper = mapper;
        }

        public async Task<ActivityDto?> Handle(GetActivityByIdQuery request, CancellationToken cancellationToken)
        {
            var activity = await _activityRepository.GetByIdAsync(request.Id, cancellationToken);
            if (activity == null) return null;

            return _mapper.Map<ActivityDto>(activity);
        }
    }

}
