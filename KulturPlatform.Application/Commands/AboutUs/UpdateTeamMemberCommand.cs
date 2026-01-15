using MediatR;

namespace KulturPlatform.Application.Commands.AboutUs;

public record UpdateTeamMemberCommand(
    Guid Id,
    string Name,
    string TitleTr,
    string TitleDe,
    string? DescriptionTr,
    string? DescriptionDe,
    string ImageUrl,
    int Order
) : IRequest;
