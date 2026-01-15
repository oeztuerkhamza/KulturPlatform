using MediatR;

namespace KulturPlatform.Application.Commands.AboutUs;

public record CreateAboutUsGoalsCommand(
    string GoalsTr,
    string GoalsDe
) : IRequest<Guid>;
