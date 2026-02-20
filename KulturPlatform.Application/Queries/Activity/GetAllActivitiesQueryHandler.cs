using KulturPlatform.Application.Dtos.Activity;
using KulturPlatform.Application.Dtos.Common;
using KulturPlatform.Application.Interfaces.Activity;
using MediatR;

namespace KulturPlatform.Application.Queries.Activity
{
    public class GetAllActivitiesQueryHandler : IRequestHandler<GetAllActivitiesQuery, PagedResult<ActivityDto>>
    {
        private readonly IActivityReadService _activityReadService;

        public GetAllActivitiesQueryHandler(IActivityReadService activityReadService)
        {
            _activityReadService = activityReadService;
        }

        public async Task<PagedResult<ActivityDto>> Handle(GetAllActivitiesQuery request, CancellationToken cancellationToken)
        {
            return await _activityReadService.GetAllPagedAsync(
                request.PageNumber,
                request.PageSize,
                cancellationToken);
        }
    }
}
