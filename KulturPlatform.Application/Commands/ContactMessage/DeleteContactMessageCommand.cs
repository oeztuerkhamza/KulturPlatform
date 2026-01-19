using MediatR;

namespace KulturPlatform.Application.Commands.ContactMessages
{
    public record DeleteContactMessageCommand(Guid Id) : IRequest;
}
