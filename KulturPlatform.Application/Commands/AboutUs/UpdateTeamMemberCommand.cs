using MediatR;

namespace KulturPlatform.Application.Commands.AboutUs;

public record UpdateTeamMemberCommand(
    Guid Id,
    string Name,
    string TitleTr,
    string TitleDe,
    string? DescriptionTr,
    string? DescriptionDe,
    string? ImageUrl,
    string? ImageBase64,      // For uploading new image
    string? ImageFileName,    // For uploading new image
    int Order
) : IRequest;
