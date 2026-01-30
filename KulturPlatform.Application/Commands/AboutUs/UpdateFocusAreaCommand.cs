using MediatR;

namespace KulturPlatform.Application.Commands.AboutUs;

public record UpdateFocusAreaCommand(
    Guid Id,
    string TitleTr,
    string TitleDe,
    string DescriptionTr,
    string DescriptionDe,
    int Order,
    string? IconUrl = null,
    string? IconBase64 = null,
    string? IconFileName = null
) : IRequest;
