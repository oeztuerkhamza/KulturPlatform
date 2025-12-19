using MediatR;

namespace KulturPlatform.Application.Commands.TeaEvent
{
    public record DeleteTeaEventCommand(Guid Id) : IRequest<bool>;
}
