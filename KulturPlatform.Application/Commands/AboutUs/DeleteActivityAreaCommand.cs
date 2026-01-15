using MediatR;

namespace KulturPlatform.Application.Commands.AboutUs;

public record DeleteActivityAreaCommand(Guid Id) : IRequest;
