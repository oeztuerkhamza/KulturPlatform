using KulturPlatform.Application.Dtos;
using MediatR;

namespace KulturPlatform.Application.Commands.ContactInfo
{
    public record UpdateContactInfoCommand(Guid Id, SaveContactInfoDto Dto) : IRequest;
}
