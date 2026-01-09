using KulturPlatform.Application.Dtos.Activity;
using MediatR;

namespace KulturPlatform.Application.Queries.Activity
{
    public record GetUpcomingActivitiesQuery() : IRequest<IEnumerable<ActivityDto>>;
}
