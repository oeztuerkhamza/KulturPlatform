using MediatR;

namespace KulturPlatform.Application.Commands.AboutUs;

public record UpdateCoreValueCommand(
    Guid Id,
    string TitleTr,
    string TitleDe,
    string DescriptionTr,
    string DescriptionDe,
    int Order
) : IRequest;
