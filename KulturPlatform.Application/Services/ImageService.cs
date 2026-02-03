using KulturPlatform.Application.Interfaces;
using KulturPlatform.Domain.Commons.ValueObjects;
using Microsoft.Extensions.Logging;

namespace KulturPlatform.Application.Services;

/// <summary>
/// Orchestrates image processing and storage operations
/// Combines IImageProcessingService and IFileStorageService for complete image handling
/// </summary>
public class ImageService : IImageService
{
    private readonly IImageProcessingService _imageProcessing;
    private readonly IFileStorageService _fileStorage;
    private readonly ILogger<ImageService> _logger;

    public ImageService(
        IImageProcessingService imageProcessing,
        IFileStorageService fileStorage,
        ILogger<ImageService> logger)
    {
        _imageProcessing = imageProcessing;
        _fileStorage = fileStorage;
        _logger = logger;
    }

    /// <summary>
    /// Complete workflow: Process image and upload to storage
    /// Returns a HybridImage with URL from cloud storage
    /// </summary>
    public async Task<HybridImage> ProcessAndUploadImageAsync(
        string base64Data,
        string fileName,
        string containerName,
        int maxWidth = 1920,
        int maxHeight = 1080,
        int quality = 85,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        try
        {
            _logger.LogDebug("Processing and uploading image: {FileName} to {Container}", fileName, containerName);
            
            // Step 1: Process image (resize, compress, validate)
            var processedImage = await _imageProcessing.ProcessImageAsync(
                base64Data, 
                fileName, 
                maxWidth, 
                maxHeight, 
                quality);

            cancellationToken.ThrowIfCancellationRequested();

            // Step 2: Upload to storage and get URL
            var imageUrl = await _fileStorage.UploadImageAsync(
                processedImage, 
                containerName, 
                null); // Let storage service generate unique filename

            _logger.LogInformation("Image uploaded successfully: {FileName} -> {Url}", fileName, imageUrl);

            // Step 3: Return HybridImage with URL
            return HybridImage.FromUrl(imageUrl);
        }
        catch (OperationCanceledException)
        {
            _logger.LogWarning("Image processing cancelled: {FileName}", fileName);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to process and upload image: {FileName}", fileName);
            throw;
        }
    }

    /// <summary>
    /// Process image and store in database (as ImageData)
    /// Use this when you want to keep images in DB (not recommended for production)
    /// </summary>
    public async Task<HybridImage> ProcessAndStoreInDatabaseAsync(
        string base64Data,
        string fileName,
        int maxWidth = 1920,
        int maxHeight = 1080,
        int quality = 85,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        try
        {
            var processedImage = await _imageProcessing.ProcessImageAsync(
                base64Data, 
                fileName, 
                maxWidth, 
                maxHeight, 
                quality);

            return HybridImage.FromImageData(processedImage);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to process image for database storage: {FileName}", fileName);
            throw;
        }
    }

    /// <summary>
    /// Update an existing image: delete old, upload new
    /// Ensures transactional consistency
    /// </summary>
    public async Task<HybridImage> UpdateImageAsync(
        HybridImage? currentImage,
        string newBase64Data,
        string newFileName,
        string containerName,
        int maxWidth = 1920,
        int maxHeight = 1080,
        int quality = 85,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        try
        {
            // Process and upload new image first
            var newImage = await ProcessAndUploadImageAsync(
                newBase64Data, 
                newFileName, 
                containerName, 
                maxWidth, 
                maxHeight, 
                quality,
                cancellationToken);

            // Delete old image only after successful upload
            if (currentImage != null && currentImage.IsUrl())
            {
                await DeleteImageAsync(currentImage, cancellationToken);
            }

            return newImage;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to update image");
            throw;
        }
    }

    /// <summary>
    /// Delete image from storage
    /// </summary>
    public async Task<bool> DeleteImageAsync(HybridImage image, CancellationToken cancellationToken = default)
    {
        try
        {
            if (image.IsUrl() && image.ImageUrl != null)
            {
                return await _fileStorage.DeleteImageAsync(image.ImageUrl.Value);
            }

            // Database-stored images don't need separate deletion
            // They'll be deleted when the entity is deleted
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to delete image");
            return false;
        }
    }

    /// <summary>
    /// Delete image by URL
    /// </summary>
    public async Task<bool> DeleteImageByUrlAsync(string imageUrl, CancellationToken cancellationToken = default)
    {
        try
        {
            if (!string.IsNullOrWhiteSpace(imageUrl))
            {
                return await _fileStorage.DeleteImageAsync(imageUrl);
            }
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to delete image by URL: {Url}", imageUrl);
            return false;
        }
    }

    /// <summary>
    /// Migrate image from database to cloud storage
    /// Useful for migrating existing data
    /// </summary>
    public async Task<HybridImage> MigrateToCloudStorageAsync(
        HybridImage currentImage,
        string containerName,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        if (currentImage.IsDatabase() && currentImage.ImageData != null)
        {
            // Upload existing database image to cloud
            var imageUrl = await _fileStorage.UploadImageAsync(
                currentImage.ImageData, 
                containerName);

            return HybridImage.FromUrl(imageUrl);
        }

        // Already in cloud storage
        return currentImage;
    }

    /// <summary>
    /// Create thumbnail and upload
    /// </summary>
    public async Task<HybridImage> CreateAndUploadThumbnailAsync(
        HybridImage sourceImage,
        string containerName,
        int width,
        int height,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        try
        {
            ImageData sourceData;

            // Get source image data
            if (sourceImage.IsDatabase() && sourceImage.ImageData != null)
            {
                sourceData = sourceImage.ImageData;
            }
            else if (sourceImage.IsUrl() && sourceImage.ImageUrl != null)
            {
                // Download from URL, convert to ImageData (implement as needed)
                throw new NotImplementedException("Downloading from URL not yet implemented");
            }
            else
            {
                throw new InvalidOperationException("Source image has no data");
            }

            cancellationToken.ThrowIfCancellationRequested();

            // Create thumbnail
            var thumbnail = await _imageProcessing.CreateThumbnailAsync(sourceData, width, height);

            // Upload thumbnail
            var thumbnailUrl = await _fileStorage.UploadImageAsync(
                thumbnail, 
                $"{containerName}-thumbnails");

            return HybridImage.FromUrl(thumbnailUrl);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create and upload thumbnail");
            throw;
        }
    }
}
