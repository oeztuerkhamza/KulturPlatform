using KulturPlatform.Application.Dtos;
using MediatR;

namespace KulturPlatform.Application.Queries.Activity
{
    public record GetAllActivitiesQuery() : IRequest<IEnumerable<ActivityDto>>;
}
