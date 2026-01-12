using AutoMapper;
using KulturPlatform.Application.Dtos.Activity;
using KulturPlatform.Application.Interfaces.Activity;
using MediatR;

namespace KulturPlatform.Application.Queries.Activity
{
    public class GetActivityByIdQueryHandler : IRequestHandler<GetActivityByIdQuery, ActivityDto?>
    {
        private readonly IActivityReadService _activityReadService;
        private readonly IMapper _mapper;

        public GetActivityByIdQueryHandler(IActivityReadService activityReadService, IMapper mapper)
        {
            _activityReadService = activityReadService;
            _mapper = mapper;
        }

        public async Task<ActivityDto?> Handle(GetActivityByIdQuery request, CancellationToken cancellationToken)
        {
            var activity = await _activityReadService.GetByIdAsync(request.Id);
            if (activity == null) return null;

            return _mapper.Map<ActivityDto>(activity);
        }
    }

}
