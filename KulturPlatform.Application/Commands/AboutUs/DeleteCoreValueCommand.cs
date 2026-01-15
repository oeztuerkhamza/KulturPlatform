using MediatR;

namespace KulturPlatform.Application.Commands.AboutUs;

public record DeleteCoreValueCommand(Guid Id) : IRequest;
