using KulturPlatform.Application.Constants;
using KulturPlatform.Application.Interfaces;
using KulturPlatform.Application.Interfaces.AboutUs;
using KulturPlatform.Domain.Commons.Entities;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace KulturPlatform.Application.Commands.AboutUs;

public class CreateTeamMemberCommandHandler : IRequestHandler<CreateTeamMemberCommand, Guid>
{
    private readonly ITeamMemberRepository _repository;
    private readonly IUnitOfWork _uow;
    private readonly IImageService _imageService;
    private readonly ILogger<CreateTeamMemberCommandHandler> _logger;

    public CreateTeamMemberCommandHandler(
        ITeamMemberRepository repository,
        IUnitOfWork uow,
        IImageService imageService,
        ILogger<CreateTeamMemberCommandHandler> logger)
    {
        _repository = repository;
        _uow = uow;
        _imageService = imageService;
        _logger = logger;
    }

    public async Task<Guid> Handle(CreateTeamMemberCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating team member: {Name}", request.Name);

        string? uploadedImageUrl = null;

        try
        {
            // Handle profile image upload
            string? imageUrl = request.ImageUrl;

            if (!string.IsNullOrWhiteSpace(request.ImageBase64) && !string.IsNullOrWhiteSpace(request.ImageFileName))
            {
                _logger.LogDebug("Processing Base64 image for team member: {FileName}", request.ImageFileName);

                // Upload image to storage and get URL
                var image = await _imageService.ProcessAndUploadImageAsync(
                    request.ImageBase64,
                    request.ImageFileName,
                    ImageProcessingConstants.TeamMember.ContainerPath,
                    maxWidth: ImageProcessingConstants.TeamMember.MaxWidth,
                    maxHeight: ImageProcessingConstants.TeamMember.MaxHeight,
                    quality: ImageProcessingConstants.TeamMember.Quality,
                    cancellationToken);

                imageUrl = image.ImageUrl?.Value ?? "";
                uploadedImageUrl = imageUrl;
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

            _logger.LogInformation("Team member created successfully with ID: {TeamMemberId}", teamMember.Id);

            return teamMember.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create team member: {Name}", request.Name);

            // Compensation: Clean up uploaded image if DB save failed
            if (!string.IsNullOrWhiteSpace(uploadedImageUrl) && !string.IsNullOrWhiteSpace(request.ImageBase64))
            {
                try
                {
                    await _imageService.DeleteImageByUrlAsync(uploadedImageUrl, cancellationToken);
                    _logger.LogWarning("Cleaned up orphaned image: {ImageUrl}", uploadedImageUrl);
                }
                catch (Exception cleanupEx)
                {
                    _logger.LogError(cleanupEx, "Failed to cleanup orphaned image: {ImageUrl}", uploadedImageUrl);
                }
            }

            throw;
        }
    }
}
