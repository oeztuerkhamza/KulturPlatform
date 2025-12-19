using MediatR;

namespace KulturPlatform.Application.Commands.GuelenMovement
{
    public record UpdateGuelenMovementCommand(
        Guid Id,
        string TitleTr,
        string TitleDe,
        string ContentTr,
        string ContentDe,
        string ImageUrl
    ) : IRequest<bool>;

}
