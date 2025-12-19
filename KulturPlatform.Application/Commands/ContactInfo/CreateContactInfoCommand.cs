using KulturPlatform.Application.Dtos;
using MediatR;

namespace KulturPlatform.Application.Commands.ContactInfo
{
    public record CreateContactInfoCommand(SaveContactInfoDto Dto)
        : IRequest<Guid>;

}

