using KulturPlatform.Application.Dtos;
using MediatR;

namespace KulturPlatform.Application.Commands.AboutUs
{
    public record UpdateGoalsCommand(
        DescriptionDto GoalsTr,
        DescriptionDto GoalsDe
    ) : IRequest;

    public record CreateGoalsCommand(
        DescriptionDto GoalsTr,
        DescriptionDto GoalsDe
    ) : IRequest;
}
