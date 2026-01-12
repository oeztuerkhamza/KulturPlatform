using MediatR;

namespace KulturPlatform.Application.Commands.Home
{
    public record UpdateInstagramPostCommand(
        Guid Id,
        string ImageUrl,
        string? Link = null
    ) : IRequest;
}
