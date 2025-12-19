using MediatR;

namespace KulturPlatform.Application.Commands.Partner
{
    public record DeletePartnerCommand(Guid Id) : IRequest;
}
