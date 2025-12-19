using KulturPlatform.Application.Dtos;
using KulturPlatform.Application.Interfaces.Activity;
using MediatR;

namespace KulturPlatform.Application.Queries.Activity
{
    public class GetAllActivitiesQueryHandler : IRequestHandler<GetAllActivitiesQuery, IEnumerable<ActivityDto>>
    {
        private readonly IActivityReadService _activityReadService;

        public GetAllActivitiesQueryHandler(IActivityReadService activityReadService)
        {
            _activityReadService = activityReadService;
        }

        public async Task<IEnumerable<ActivityDto>> Handle(GetAllActivitiesQuery request, CancellationToken cancellationToken)
        {
            return await _activityReadService.GetAllAsync();
        }
    }
}
