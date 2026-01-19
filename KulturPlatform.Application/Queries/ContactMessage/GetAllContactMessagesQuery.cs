using KulturPlatform.Application.Dtos.ContactMessages;
using MediatR;

namespace KulturPlatform.Application.Queries.ContactMessages
{
    public record GetAllContactMessagesQuery : IRequest<IEnumerable<ContactMessageDto>>;
}
