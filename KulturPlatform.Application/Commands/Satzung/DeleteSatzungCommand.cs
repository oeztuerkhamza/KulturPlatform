using MediatR;

namespace KulturPlatform.Application.Commands.Satzung
{
    public record DeleteSatzungCommand(Guid Id) : IRequest;
}
