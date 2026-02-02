using KulturPlatform.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KulturPlatform.API.Controllers;

/// <summary>
/// Example controller demonstrating image upload handling
/// Shows how to use ImageService for complete image workflows
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ImagesController : ControllerBase
{
    private readonly ImageService _imageService;
    private readonly ILogger<ImagesController> _logger;

    public ImagesController(
        ImageService imageService,
        ILogger<ImagesController> logger)
    {
        _imageService = imageService;
        _logger = logger;
    }

    /// <summary>
    /// Upload an image (base64 format)
    /// </summary>
    /// <param name="request">Image upload request</param>
    /// <returns>URL of uploaded image</returns>
    [HttpPost("upload")]
    [Authorize] // Protect this endpoint
    public async Task<IActionResult> UploadImage([FromBody] ImageUploadRequest request)
    {
        try
        {
            var hybridImage = await _imageService.ProcessAndUploadImageAsync(
                request.Base64Data,
                request.FileName,
                request.ContainerName,
                request.MaxWidth ?? 1920,
                request.MaxHeight ?? 1080,
                request.Quality ?? 85);

            return Ok(new ImageUploadResponse
            {
                ImageUrl = hybridImage.GetImageSource(),
                Success = true,
                Message = "Image uploaded successfully"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to upload image");
            return BadRequest(new ImageUploadResponse
            {
                Success = false,
                Message = ex.Message
            });
        }
    }

    /// <summary>
    /// Upload multiple images
    /// </summary>
    [HttpPost("upload-multiple")]
    [Authorize]
    public async Task<IActionResult> UploadMultipleImages([FromBody] MultipleImageUploadRequest request)
    {
        try
        {
            var uploadedUrls = new List<string>();

            foreach (var image in request.Images)
            {
                var hybridImage = await _imageService.ProcessAndUploadImageAsync(
                    image.Base64Data,
                    image.FileName,
                    request.ContainerName,
                    image.MaxWidth ?? 1920,
                    image.MaxHeight ?? 1080,
                    image.Quality ?? 85);

                uploadedUrls.Add(hybridImage.GetImageSource()!);
            }

            return Ok(new
            {
                Success = true,
                ImageUrls = uploadedUrls,
                Count = uploadedUrls.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to upload multiple images");
            return BadRequest(new { Success = false, Message = ex.Message });
        }
    }

    /// <summary>
    /// Create thumbnail for an existing image
    /// </summary>
    [HttpPost("create-thumbnail")]
    [Authorize]
    public async Task<IActionResult> CreateThumbnail([FromBody] ThumbnailRequest request)
    {
        try
        {
            // You would need to fetch the existing image first
            // This is just an example structure
            
            return Ok(new
            {
                Success = true,
                Message = "Thumbnail created successfully"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create thumbnail");
            return BadRequest(new { Success = false, Message = ex.Message });
        }
    }
}

#region Request/Response DTOs

public class ImageUploadRequest
{
    public string Base64Data { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public string ContainerName { get; set; } = "general";
    public int? MaxWidth { get; set; }
    public int? MaxHeight { get; set; }
    public int? Quality { get; set; }
}

public class MultipleImageUploadRequest
{
    public List<ImageUploadRequest> Images { get; set; } = new();
    public string ContainerName { get; set; } = "general";
}

public class ImageUploadResponse
{
    public bool Success { get; set; }
    public string? ImageUrl { get; set; }
    public string? Message { get; set; }
}

public class ThumbnailRequest
{
    public string SourceImageUrl { get; set; } = string.Empty;
    public int Width { get; set; } = 300;
    public int Height { get; set; } = 200;
    public string ContainerName { get; set; } = "thumbnails";
}

#endregion
