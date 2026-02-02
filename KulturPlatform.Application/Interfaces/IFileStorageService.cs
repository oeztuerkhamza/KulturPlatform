using KulturPlatform.Domain.Commons.ValueObjects;

namespace KulturPlatform.Application.Interfaces;

/// <summary>
/// Abstraction for file storage operations
/// Supports local file system, Azure Blob Storage, AWS S3, etc.
/// </summary>
public interface IFileStorageService
{
    /// <summary>
    /// Upload an image file and return its URL
    /// </summary>
    /// <param name="imageData">Image data to upload</param>
    /// <param name="containerName">Storage container/bucket name (e.g., "profile-images", "gallery")</param>
    /// <param name="fileName">Optional custom filename (auto-generated if null)</param>
    /// <returns>Public URL of the uploaded image</returns>
    Task<string> UploadImageAsync(ImageData imageData, string containerName, string? fileName = null);

    /// <summary>
    /// Upload an image from base64 data
    /// </summary>
    /// <param name="base64Data">Base64 encoded image</param>
    /// <param name="mimeType">Image MIME type</param>
    /// <param name="containerName">Storage container name</param>
    /// <param name="fileName">Optional custom filename</param>
    /// <returns>Public URL of the uploaded image</returns>
    Task<string> UploadImageFromBase64Async(
        string base64Data, 
        string mimeType, 
        string containerName, 
        string? fileName = null);

    /// <summary>
    /// Delete an image by its URL
    /// </summary>
    /// <param name="imageUrl">Full URL of the image to delete</param>
    /// <returns>True if deleted successfully</returns>
    Task<bool> DeleteImageAsync(string imageUrl);

    /// <summary>
    /// Delete multiple images by their URLs
    /// </summary>
    /// <param name="imageUrls">List of image URLs to delete</param>
    /// <returns>Number of successfully deleted images</returns>
    Task<int> DeleteImagesAsync(IEnumerable<string> imageUrls);

    /// <summary>
    /// Check if an image exists at the given URL
    /// </summary>
    /// <param name="imageUrl">Image URL to check</param>
    /// <returns>True if image exists</returns>
    Task<bool> ImageExistsAsync(string imageUrl);

    /// <summary>
    /// Get a pre-signed URL for secure temporary access (useful for private images)
    /// </summary>
    /// <param name="imageUrl">Image URL</param>
    /// <param name="expiresInMinutes">Expiration time in minutes</param>
    /// <returns>Pre-signed URL</returns>
    Task<string> GetPresignedUrlAsync(string imageUrl, int expiresInMinutes = 60);
}
