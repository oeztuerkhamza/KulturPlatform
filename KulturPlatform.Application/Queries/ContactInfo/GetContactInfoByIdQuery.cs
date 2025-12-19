using KulturPlatform.Application.Dtos;
using MediatR;

namespace KulturPlatform.Application.Queries.ContactInfo
{
    public record GetContactInfoByIdQuery(Guid Id) : IRequest<ContactInfoDto>;

}

