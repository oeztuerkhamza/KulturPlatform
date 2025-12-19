using MediatR;

namespace KulturPlatform.Application.Commands.ContactInfo
{
    public record DeleteContactInfoCommand(Guid Id) : IRequest<bool>;
}
