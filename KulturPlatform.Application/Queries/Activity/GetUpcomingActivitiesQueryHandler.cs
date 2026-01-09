using KulturPlatform.Application.Dtos.Activity;
using KulturPlatform.Application.Interfaces.Activity;
using MediatR;

namespace KulturPlatform.Application.Queries.Activity
{
    public class GetUpcomingActivitiesQueryHandler : IRequestHandler<GetUpcomingActivitiesQuery, IEnumerable<ActivityDto>>
    {
        private readonly IActivityReadService _activityReadService;

        public GetUpcomingActivitiesQueryHandler(IActivityReadService activityReadService)
        {
            _activityReadService = activityReadService;
        }

        public async Task<IEnumerable<ActivityDto>> Handle(GetUpcomingActivitiesQuery request, CancellationToken cancellationToken)
        {
            return await _activityReadService.GetUpcomingAsync();
        }
    }
}
