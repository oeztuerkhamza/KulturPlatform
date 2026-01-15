using MediatR;

namespace KulturPlatform.Application.Commands.AboutUs;

public record UpdateAboutUsGoalsCommand(
    Guid Id,
    string GoalsTr,
    string GoalsDe
) : IRequest;
