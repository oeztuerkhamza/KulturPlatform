using KulturPlatform.Application.Interfaces;
using KulturPlatform.Domain.Commons.ValueObjects;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace KulturPlatform.Infrastructure.Services;

/// <summary>
/// Local file system storage implementation
/// Stores images in wwwroot/uploads folder
/// ?? For DEVELOPMENT only - use cloud storage in production
/// </summary>
public class LocalFileStorageService : IFileStorageService
{
    private readonly ILogger<LocalFileStorageService> _logger;
    private readonly string _baseUrl;
    private readonly string _uploadsFolder;
    private readonly string _webRootPath;

    public LocalFileStorageService(
        ILogger<LocalFileStorageService> logger,
        IConfiguration configuration)
    {
        _logger = logger;
        
        // Get base URL from configuration
        _baseUrl = configuration["FileStorage:BaseUrl"] ?? "http://localhost:5000";
        
        // Get web root path (default to current directory + wwwroot)
        _webRootPath = configuration["FileStorage:WebRootPath"] ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
        _uploadsFolder = Path.Combine(_webRootPath, "uploads");

        // Ensure uploads directory exists
        if (!Directory.Exists(_uploadsFolder))
        {
            Directory.CreateDirectory(_uploadsFolder);
        }
    }

    public async Task<string> UploadImageAsync(ImageData imageData, string containerName, string? fileName = null)
    {
        try
        {
            // Create container directory
            var containerPath = Path.Combine(_uploadsFolder, containerName);
            if (!Directory.Exists(containerPath))
            {
                Directory.CreateDirectory(containerPath);
            }

            // Generate unique filename
            var finalFileName = fileName ?? $"{Guid.NewGuid()}_{SanitizeFileName(imageData.FileName)}";
            var filePath = Path.Combine(containerPath, finalFileName);

            // Save file
            var imageBytes = imageData.GetBytes();
            await File.WriteAllBytesAsync(filePath, imageBytes);

            // Return public URL
            var publicUrl = $"{_baseUrl}/uploads/{containerName}/{finalFileName}";
            
            _logger.LogInformation("Image uploaded successfully: {Url}", publicUrl);
            return publicUrl;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to upload image: {FileName}", imageData.FileName);
            throw new InvalidOperationException($"Failed to upload image: {ex.Message}", ex);
        }
    }

    public async Task<string> UploadImageFromBase64Async(
        string base64Data, 
        string mimeType, 
        string containerName, 
        string? fileName = null)
    {
        try
        {
            // Convert base64 to bytes
            var imageBytes = Convert.FromBase64String(base64Data);
            
            // Generate filename with proper extension
            var extension = GetExtensionFromMimeType(mimeType);
            var finalFileName = fileName ?? $"{Guid.NewGuid()}{extension}";

            // Create container directory
            var containerPath = Path.Combine(_uploadsFolder, containerName);
            if (!Directory.Exists(containerPath))
            {
                Directory.CreateDirectory(containerPath);
            }

            var filePath = Path.Combine(containerPath, finalFileName);

            // Save file
            await File.WriteAllBytesAsync(filePath, imageBytes);

            // Return public URL
            var publicUrl = $"{_baseUrl}/uploads/{containerName}/{finalFileName}";
            
            _logger.LogInformation("Image uploaded successfully: {Url}", publicUrl);
            return publicUrl;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to upload image from base64");
            throw new InvalidOperationException($"Failed to upload image: {ex.Message}", ex);
        }
    }

    public Task<bool> DeleteImageAsync(string imageUrl)
    {
        try
        {
            var filePath = GetFilePathFromUrl(imageUrl);
            
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                _logger.LogInformation("Image deleted successfully: {Url}", imageUrl);
                return Task.FromResult(true);
            }

            _logger.LogWarning("Image not found for deletion: {Url}", imageUrl);
            return Task.FromResult(false);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to delete image: {Url}", imageUrl);
            return Task.FromResult(false);
        }
    }

    public async Task<int> DeleteImagesAsync(IEnumerable<string> imageUrls)
    {
        var deletedCount = 0;
        foreach (var url in imageUrls)
        {
            if (await DeleteImageAsync(url))
            {
                deletedCount++;
            }
        }
        return deletedCount;
    }

    public Task<bool> ImageExistsAsync(string imageUrl)
    {
        var filePath = GetFilePathFromUrl(imageUrl);
        return Task.FromResult(File.Exists(filePath));
    }

    public Task<string> GetPresignedUrlAsync(string imageUrl, int expiresInMinutes = 60)
    {
        // Local storage doesn't need pre-signed URLs
        return Task.FromResult(imageUrl);
    }

    private string GetFilePathFromUrl(string imageUrl)
    {
        // Extract relative path from URL
        var uri = new Uri(imageUrl);
        var relativePath = uri.LocalPath.TrimStart('/');
        return Path.Combine(_webRootPath, relativePath);
    }

    private string SanitizeFileName(string fileName)
    {
        var invalidChars = Path.GetInvalidFileNameChars();
        return string.Join("_", fileName.Split(invalidChars, StringSplitOptions.RemoveEmptyEntries));
    }

    private string GetExtensionFromMimeType(string mimeType)
    {
        return mimeType.ToLowerInvariant() switch
        {
            "image/jpeg" or "image/jpg" => ".jpg",
            "image/png" => ".png",
            "image/webp" => ".webp",
            "image/gif" => ".gif",
            _ => ".jpg"
        };
    }
}
