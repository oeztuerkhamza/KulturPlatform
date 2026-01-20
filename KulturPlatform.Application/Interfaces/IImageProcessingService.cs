using KulturPlatform.Domain.Commons.ValueObjects;

namespace KulturPlatform.Application.Interfaces
{
    /// <summary>
    /// Service for processing, validating and compressing images before storage
    /// </summary>
    public interface IImageProcessingService
    {
        /// <summary>
        /// Process an uploaded image file and convert it to ImageData
        /// Includes validation, resizing, and compression
        /// </summary>
        /// <param name="base64Data">Base64 encoded image with or without data URI prefix</param>
        /// <param name="fileName">Original filename</param>
        /// <param name="maxWidth">Maximum width in pixels (default: 1920)</param>
        /// <param name="maxHeight">Maximum height in pixels (default: 1080)</param>
        /// <param name="quality">JPEG/WebP compression quality 1-100 (default: 85)</param>
        /// <returns>Processed ImageData ready for database storage</returns>
        Task<ImageData> ProcessImageAsync(
            string base64Data,
            string fileName,
            int maxWidth = 1920,
            int maxHeight = 1080,
            int quality = 85);

        /// <summary>
        /// Validates image format and size without processing
        /// </summary>
        /// <param name="base64Data">Base64 encoded image</param>
        /// <param name="maxSizeBytes">Maximum allowed size in bytes (default: 5MB)</param>
        /// <returns>True if valid, otherwise throws exception with details</returns>
        Task<bool> ValidateImageAsync(string base64Data, int maxSizeBytes = 5242880);

        /// <summary>
        /// Creates a thumbnail version of the image
        /// </summary>
        /// <param name="imageData">Source image data</param>
        /// <param name="width">Thumbnail width</param>
        /// <param name="height">Thumbnail height</param>
        /// <returns>Thumbnail as ImageData</returns>
        Task<ImageData> CreateThumbnailAsync(ImageData imageData, int width, int height);
    }
}
