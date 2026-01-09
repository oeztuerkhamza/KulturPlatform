using KulturPlatform.Application.Dtos.Activity;
using MediatR;

namespace KulturPlatform.Application.Queries.Activity
{
    public record GetActivityByIdQuery(Guid Id) : IRequest<ActivityDto?>;
}
