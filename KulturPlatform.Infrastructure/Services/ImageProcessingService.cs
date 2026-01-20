using KulturPlatform.Application.Interfaces;
using KulturPlatform.Domain.Commons.ValueObjects;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.Processing;
using SixLaborsImage = SixLabors.ImageSharp.Image;
using Size = SixLabors.ImageSharp.Size;

namespace KulturPlatform.Infrastructure.Services
{
    /// <summary>
    /// Image processing service using ImageSharp for compression, resizing and format conversion
    /// Optimized for web delivery and database storage
    /// </summary>
    public class ImageProcessingService : IImageProcessingService
    {
        private readonly ILogger<ImageProcessingService> _logger;
        private readonly int _maxFileSizeBytes;
        private readonly int _defaultMaxWidth;
        private readonly int _defaultMaxHeight;
        private readonly int _defaultQuality;

        private static readonly string[] AllowedMimeTypes = new[]
        {
            "image/jpeg",
            "image/jpg",
            "image/png",
            "image/webp"
        };

        public ImageProcessingService(
            ILogger<ImageProcessingService> logger,
            IConfiguration configuration)
        {
            _logger = logger;
            
            // Load configuration with defaults
            _maxFileSizeBytes = GetConfigValue(configuration, "ImageProcessing:MaxFileSizeBytes", 5242880); // 5MB default
            _defaultMaxWidth = GetConfigValue(configuration, "ImageProcessing:DefaultMaxWidth", 1920);
            _defaultMaxHeight = GetConfigValue(configuration, "ImageProcessing:DefaultMaxHeight", 1080);
            _defaultQuality = GetConfigValue(configuration, "ImageProcessing:DefaultQuality", 85);
        }

        private static int GetConfigValue(IConfiguration configuration, string key, int defaultValue)
        {
            var value = configuration[key];
            return int.TryParse(value, out var result) ? result : defaultValue;
        }

        public async Task<ImageData> ProcessImageAsync(
            string base64Data,
            string fileName,
            int maxWidth = 1920,
            int maxHeight = 1080,
            int quality = 85)
        {
            try
            {
                // Remove data URI prefix if present
                base64Data = RemoveDataUriPrefix(base64Data);

                // Decode base64 to bytes
                var imageBytes = Convert.FromBase64String(base64Data);

                // Validate size before processing
                if (imageBytes.Length > _maxFileSizeBytes)
                {
                    throw new ArgumentException(
                        $"Image size ({imageBytes.Length / 1024 / 1024}MB) exceeds maximum allowed size ({_maxFileSizeBytes / 1024 / 1024}MB).");
                }

                using var inputStream = new MemoryStream(imageBytes);
                using var image = await SixLaborsImage.LoadAsync(inputStream);

                // Get original format
                var originalFormat = image.Metadata.DecodedImageFormat ?? throw new InvalidOperationException("Unable to determine image format.");
                
                // Determine target format and MIME type
                var (mimeType, encoder) = GetEncoderAndMimeType(originalFormat, quality);

                // Resize if necessary
                if (image.Width > maxWidth || image.Height > maxHeight)
                {
                    image.Mutate(x => x.Resize(new ResizeOptions
                    {
                        Size = new Size(maxWidth, maxHeight),
                        Mode = ResizeMode.Max,
                        Sampler = KnownResamplers.Lanczos3
                    }));
                }

                // Encode to output format
                using var outputStream = new MemoryStream();
                await image.SaveAsync(outputStream, encoder);
                var processedBytes = outputStream.ToArray();

                // Convert to base64
                var processedBase64 = Convert.ToBase64String(processedBytes);

                _logger.LogInformation(
                    "Image processed: {FileName}, Original size: {OriginalSize}KB, Processed size: {ProcessedSize}KB, Dimensions: {Width}x{Height}",
                    fileName,
                    imageBytes.Length / 1024,
                    processedBytes.Length / 1024,
                    image.Width,
                    image.Height);

                return ImageData.Create(processedBase64, mimeType, fileName, processedBytes.Length);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing image: {FileName}", fileName);
                throw new InvalidOperationException($"Failed to process image: {ex.Message}", ex);
            }
        }

        public async Task<bool> ValidateImageAsync(string base64Data, int maxSizeBytes = 5242880)
        {
            try
            {
                // Remove data URI prefix if present
                base64Data = RemoveDataUriPrefix(base64Data);

                // Decode and validate
                var imageBytes = Convert.FromBase64String(base64Data);

                if (imageBytes.Length > maxSizeBytes)
                {
                    throw new ArgumentException(
                        $"Image size ({imageBytes.Length / 1024 / 1024}MB) exceeds maximum allowed size ({maxSizeBytes / 1024 / 1024}MB).");
                }

                // Try to load the image to validate format
                using var stream = new MemoryStream(imageBytes);
                using var image = await SixLaborsImage.LoadAsync(stream);

                var format = image.Metadata.DecodedImageFormat;
                if (format == null)
                {
                    throw new ArgumentException("Unable to determine image format.");
                }

                var mimeType = GetMimeTypeFromFormat(format);
                if (!AllowedMimeTypes.Contains(mimeType))
                {
                    throw new ArgumentException(
                        $"Image format '{mimeType}' is not supported. Allowed formats: {string.Join(", ", AllowedMimeTypes)}");
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Image validation failed");
                throw;
            }
        }

        public async Task<ImageData> CreateThumbnailAsync(ImageData imageData, int width, int height)
        {
            try
            {
                var imageBytes = imageData.GetBytes();

                using var inputStream = new MemoryStream(imageBytes);
                using var image = await SixLaborsImage.LoadAsync(inputStream);

                // Create thumbnail
                image.Mutate(x => x.Resize(new ResizeOptions
                {
                    Size = new Size(width, height),
                    Mode = ResizeMode.Crop,
                    Sampler = KnownResamplers.Lanczos3
                }));

                // Use same format as original
                var encoder = GetEncoderFromMimeType(imageData.MimeType);

                using var outputStream = new MemoryStream();
                await image.SaveAsync(outputStream, encoder);
                var thumbnailBytes = outputStream.ToArray();

                var thumbnailBase64 = Convert.ToBase64String(thumbnailBytes);
                var thumbnailFileName = $"thumb_{width}x{height}_{imageData.FileName}";

                return ImageData.Create(thumbnailBase64, imageData.MimeType, thumbnailFileName, thumbnailBytes.Length);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating thumbnail for: {FileName}", imageData.FileName);
                throw new InvalidOperationException($"Failed to create thumbnail: {ex.Message}", ex);
            }
        }

        private string RemoveDataUriPrefix(string base64Data)
        {
            // Remove "data:image/...;base64," prefix if present
            if (base64Data.StartsWith("data:"))
            {
                var commaIndex = base64Data.IndexOf(',');
                if (commaIndex != -1)
                {
                    return base64Data.Substring(commaIndex + 1);
                }
            }
            return base64Data;
        }

        private (string mimeType, IImageEncoder encoder) GetEncoderAndMimeType(IImageFormat format, int quality)
        {
            // Convert PNG to WebP for better compression, keep JPEG as is
            if (format.Name.Equals("PNG", StringComparison.OrdinalIgnoreCase))
            {
                return ("image/webp", new WebpEncoder { Quality = quality });
            }
            else if (format.Name.Equals("JPEG", StringComparison.OrdinalIgnoreCase) ||
                     format.Name.Equals("JPG", StringComparison.OrdinalIgnoreCase))
            {
                return ("image/jpeg", new JpegEncoder { Quality = quality });
            }
            else if (format.Name.Equals("WEBP", StringComparison.OrdinalIgnoreCase))
            {
                return ("image/webp", new WebpEncoder { Quality = quality });
            }

            // Default to JPEG for unknown formats
            return ("image/jpeg", new JpegEncoder { Quality = quality });
        }

        private string GetMimeTypeFromFormat(IImageFormat format)
        {
            return format.Name.ToLowerInvariant() switch
            {
                "png" => "image/png",
                "jpeg" or "jpg" => "image/jpeg",
                "webp" => "image/webp",
                _ => "image/jpeg"
            };
        }

        private IImageEncoder GetEncoderFromMimeType(string mimeType)
        {
            return mimeType.ToLowerInvariant() switch
            {
                "image/png" => new PngEncoder(),
                "image/jpeg" or "image/jpg" => new JpegEncoder { Quality = _defaultQuality },
                "image/webp" => new WebpEncoder { Quality = _defaultQuality },
                _ => new JpegEncoder { Quality = _defaultQuality }
            };
        }
    }
}
