using KulturPlatform.Application.Dtos;
using MediatR;

namespace KulturPlatform.Application.Queries.GuelenMovement
{
    public record GetGuelenMovementByIdQuery(Guid Id) : IRequest<GuelenMovementDto>;

}
