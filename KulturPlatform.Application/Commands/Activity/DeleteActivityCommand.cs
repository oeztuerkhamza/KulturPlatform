using MediatR;

namespace KulturPlatform.Application.Commands.Activity
{
    public record DeleteActivityCommand(Guid Id) : IRequest;
}
