using KulturPlatform.Application.Dtos;
using MediatR;

namespace KulturPlatform.Application.Queries.Activity
{
    public record GetActivityByIdQuery(Guid Id) : IRequest<ActivityDto?>;
}
