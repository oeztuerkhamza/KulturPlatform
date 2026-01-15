using MediatR;

namespace KulturPlatform.Application.Commands.AboutUs;

public record DeleteTeamMemberCommand(Guid Id) : IRequest;
