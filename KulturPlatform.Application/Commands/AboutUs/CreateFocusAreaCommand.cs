using MediatR;

namespace KulturPlatform.Application.Commands.AboutUs;

public record CreateFocusAreaCommand(
    string TitleTr,
    string TitleDe,
    string DescriptionTr,
    string DescriptionDe,
    int Order
) : IRequest<Guid>;
