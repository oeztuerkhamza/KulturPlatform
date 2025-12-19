using MediatR;

namespace KulturPlatform.Application.Commands.Imprint
{
    public record DeleteImprintCommand(Guid Id) : IRequest;
}
