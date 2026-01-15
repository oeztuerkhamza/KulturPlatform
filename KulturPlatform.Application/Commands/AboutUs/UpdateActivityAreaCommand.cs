using MediatR;

namespace KulturPlatform.Application.Commands.AboutUs;

public record UpdateActivityAreaCommand(
    Guid Id,
    string TitleTr,
    string TitleDe,
    string DescriptionTr,
    string DescriptionDe,
    int Order
) : IRequest;
