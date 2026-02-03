using KulturPlatform.Domain.Commons.ValueObjects;

namespace KulturPlatform.Application.Interfaces;

/// <summary>
/// Abstraction for image processing and storage orchestration
/// Combines image processing and file storage operations
/// </summary>
public interface IImageService
{
    /// <summary>
    /// Complete workflow: Process image and upload to storage
    /// Returns a HybridImage with URL from cloud storage
    /// </summary>
    /// <param name="base64Data">Base64 encoded image data</param>
    /// <param name="fileName">Original file name</param>
    /// <param name="containerName">Storage container/folder name</param>
    /// <param name="maxWidth">Maximum width for image resize</param>
    /// <param name="maxHeight">Maximum height for image resize</param>
    /// <param name="quality">Image quality (1-100)</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>HybridImage with URL from storage</returns>
    Task<HybridImage> ProcessAndUploadImageAsync(
        string base64Data,
        string fileName,
        string containerName,
        int maxWidth = 1920,
        int maxHeight = 1080,
        int quality = 85,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Process image and store in database (as ImageData)
    /// Use this when you want to keep images in DB (not recommended for production)
    /// </summary>
    Task<HybridImage> ProcessAndStoreInDatabaseAsync(
        string base64Data,
        string fileName,
        int maxWidth = 1920,
        int maxHeight = 1080,
        int quality = 85,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Update an existing image: delete old, upload new
    /// Ensures transactional consistency
    /// </summary>
    /// <param name="currentImage">Current image to be replaced (will be deleted)</param>
    /// <param name="newBase64Data">New image Base64 data</param>
    /// <param name="newFileName">New image file name</param>
    /// <param name="containerName">Storage container name</param>
    /// <param name="maxWidth">Maximum width</param>
    /// <param name="maxHeight">Maximum height</param>
    /// <param name="quality">Image quality</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>New HybridImage with URL</returns>
    Task<HybridImage> UpdateImageAsync(
        HybridImage? currentImage,
        string newBase64Data,
        string newFileName,
        string containerName,
        int maxWidth = 1920,
        int maxHeight = 1080,
        int quality = 85,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete image from storage
    /// </summary>
    /// <param name="image">Image to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if deleted successfully</returns>
    Task<bool> DeleteImageAsync(HybridImage image, CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete image by URL
    /// </summary>
    /// <param name="imageUrl">Image URL to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if deleted successfully</returns>
    Task<bool> DeleteImageByUrlAsync(string imageUrl, CancellationToken cancellationToken = default);

    /// <summary>
    /// Migrate image from database to cloud storage
    /// Useful for migrating existing data
    /// </summary>
    Task<HybridImage> MigrateToCloudStorageAsync(
        HybridImage currentImage,
        string containerName,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Create thumbnail and upload
    /// </summary>
    Task<HybridImage> CreateAndUploadThumbnailAsync(
        HybridImage sourceImage,
        string containerName,
        int width,
        int height,
        CancellationToken cancellationToken = default);
}
