using MediatR;

namespace KulturPlatform.Application.Commands.GuelenMovement
{
    public record DeleteGuelenMovementCommand(Guid Id) : IRequest<bool>;

}
