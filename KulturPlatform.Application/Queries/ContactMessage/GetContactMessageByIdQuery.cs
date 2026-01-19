using KulturPlatform.Application.Dtos.ContactMessages;
using MediatR;

namespace KulturPlatform.Application.Queries.ContactMessages
{
    public record GetContactMessageByIdQuery(Guid Id) : IRequest<ContactMessageDto?>;
}
