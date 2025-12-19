using KulturPlatform.Application.Dtos;
using MediatR;

namespace KulturPlatform.Application.Queries.ContactInfo
{
    public record GetContactInfoQuery : IRequest<ContactInfoDto?>;
}
