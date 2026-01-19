using MediatR;

namespace KulturPlatform.Application.Commands.ContactMessages
{
    public record MarkContactMessageAsReadCommand(Guid Id) : IRequest;
}
