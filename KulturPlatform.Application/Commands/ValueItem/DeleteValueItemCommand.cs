using MediatR;

namespace KulturPlatform.Application.Commands.ValueItem
{
    public record DeleteValueItemCommand(Guid Id) : IRequest;
}
