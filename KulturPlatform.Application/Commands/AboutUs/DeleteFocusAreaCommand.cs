using MediatR;

namespace KulturPlatform.Application.Commands.AboutUs;

public record DeleteFocusAreaCommand(Guid Id) : IRequest;
