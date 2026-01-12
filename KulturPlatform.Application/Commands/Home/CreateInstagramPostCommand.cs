using MediatR;
using System;

namespace KulturPlatform.Application.Commands.Home
{
    public record CreateInstagramPostCommand(
        string ImageUrl,
        string? Link = null
    ) : IRequest<Guid>;
}
