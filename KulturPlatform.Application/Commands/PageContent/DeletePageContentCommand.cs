using MediatR;

namespace KulturPlatform.Application.Commands.PageContent
{
    public record DeletePageContentCommand(Guid Id) : IRequest;
}
