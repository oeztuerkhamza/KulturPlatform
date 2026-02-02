using KulturPlatform.Application.Interfaces.AboutUs;
using KulturPlatform.Application.Services;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;
using MediatR;

namespace KulturPlatform.Application.Commands.AboutUs;

public class UpdateTeamMemberCommandHandler : IRequestHandler<UpdateTeamMemberCommand>
{
    private readonly ITeamMemberRepository _repository;
    private readonly IUnitOfWork _uow;
    private readonly ImageService _imageService;

    public UpdateTeamMemberCommandHandler(
        ITeamMemberRepository repository, 
        IUnitOfWork uow,
        ImageService imageService)
    {
        _repository = repository;
        _uow = uow;
        _imageService = imageService;
    }

    public async Task Handle(UpdateTeamMemberCommand request, CancellationToken cancellationToken)
    {
        var teamMember = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (teamMember == null)
            throw new KeyNotFoundException($"TeamMember with Id {request.Id} not found.");

        // Handle profile image upload
        string? imageUrl = request.ImageUrl;

        if (!string.IsNullOrWhiteSpace(request.ImageBase64) && !string.IsNullOrWhiteSpace(request.ImageFileName))
        {
            // Get current image for cleanup
            HybridImage? currentImage = null;
            if (!string.IsNullOrWhiteSpace(teamMember.ImageUrl))
            {
                currentImage = HybridImage.FromUrl(teamMember.ImageUrl);
            }

            // Upload new image and get URL
            var newImage = await _imageService.UpdateImageAsync(
                currentImage,
                request.ImageBase64,
                request.ImageFileName,
                "team-members",
                maxWidth: 800,
                maxHeight: 800,
                quality: 85);

            imageUrl = newImage.ImageUrl?.Value ?? "";
        }

        teamMember.Update(
            new Name(request.Name),
            new Title(request.TitleTr),
            new Title(request.TitleDe),
            request.DescriptionTr != null ? new Description(request.DescriptionTr) : null,
            request.DescriptionDe != null ? new Description(request.DescriptionDe) : null,
            imageUrl ?? teamMember.ImageUrl,
            request.Order
        );

        await _repository.UpdateAsync(teamMember, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);
    }
}
