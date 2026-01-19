using MediatR;

namespace KulturPlatform.Application.Commands.ContactMessages
{
    public record CreateContactMessageCommand(
        string? Anrede,
        string SenderName,
        string Email,
        string? Phone,
        string Subject,
        string Message
    ) : IRequest<Guid>;
}
