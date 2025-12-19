using KulturPlatform.Application.Dtos;
using MediatR;

namespace KulturPlatform.Application.Queries.GuelenMovement
{
    public record GetAllGuelenMovementsQuery() : IRequest<IEnumerable<GuelenMovementDto>>;

}
