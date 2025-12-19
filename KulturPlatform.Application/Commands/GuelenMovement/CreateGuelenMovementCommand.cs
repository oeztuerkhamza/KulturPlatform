using MediatR;

namespace KulturPlatform.Application.Commands.GuelenMovement
{
    public record CreateGuelenMovementCommand(
        string TitleTr,
        string TitleDe,
        string ContentTr,
        string ContentDe,
        string ImageUrl
    ) : IRequest<Guid>;

}
