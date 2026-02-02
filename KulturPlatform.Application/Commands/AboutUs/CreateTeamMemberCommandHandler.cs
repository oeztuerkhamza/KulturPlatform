using KulturPlatform.Application.Interfaces.AboutUs;
using KulturPlatform.Application.Services;
using KulturPlatform.Domain.Commons.Entities;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;
using MediatR;

namespace KulturPlatform.Application.Commands.AboutUs;

public class CreateTeamMemberCommandHandler : IRequestHandler<CreateTeamMemberCommand, Guid>
{
    private readonly ITeamMemberRepository _repository;
    private readonly IUnitOfWork _uow;
    private readonly ImageService _imageService;

    public CreateTeamMemberCommandHandler(
        ITeamMemberRepository repository, 
        IUnitOfWork uow,
        ImageService imageService)
    {
        _repository = repository;
        _uow = uow;
        _imageService = imageService;
    }

    public async Task<Guid> Handle(CreateTeamMemberCommand request, CancellationToken cancellationToken)
    {
        // Handle profile image upload
        string? imageUrl = request.ImageUrl;

        if (!string.IsNullOrWhiteSpace(request.ImageBase64) && !string.IsNullOrWhiteSpace(request.ImageFileName))
        {
            // Upload image to storage and get URL
            var image = await _imageService.ProcessAndUploadImageAsync(
                request.ImageBase64,
                request.ImageFileName,
                "team-members",
                maxWidth: 800,
                maxHeight: 800,
                quality: 85);

            imageUrl = image.ImageUrl?.Value ?? "";
        }

        var teamMember = TeamMember.Create(
            new Name(request.Name),
            new Title(request.TitleTr),
            new Title(request.TitleDe),
            request.DescriptionTr != null ? new Description(request.DescriptionTr) : null,
            request.DescriptionDe != null ? new Description(request.DescriptionDe) : null,
            imageUrl ?? "",
            request.Order
        );

        await _repository.AddAsync(teamMember, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);

        return teamMember.Id;
    }
}
