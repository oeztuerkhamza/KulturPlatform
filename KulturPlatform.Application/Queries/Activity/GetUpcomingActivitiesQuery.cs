using KulturPlatform.Application.Dtos;
using MediatR;

namespace KulturPlatform.Application.Queries.Activity
{
    public record GetUpcomingActivitiesQuery() : IRequest<IEnumerable<ActivityDto>>;
}
