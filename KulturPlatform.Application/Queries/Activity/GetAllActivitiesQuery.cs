using KulturPlatform.Application.Dtos.Activity;
using KulturPlatform.Application.Dtos.Common;
using KulturPlatform.Application.Queries.Common;
using MediatR;

namespace KulturPlatform.Application.Queries.Activity
{
    public class GetAllActivitiesQuery : PagedQuery, IRequest<PagedResult<ActivityDto>>
    {
    }
}
