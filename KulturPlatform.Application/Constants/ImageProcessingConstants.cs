namespace KulturPlatform.Application.Constants;

/// <summary>
/// Constants for image processing across different entities
/// Centralizes all magic numbers and container paths
/// </summary>
public static class ImageProcessingConstants
{
    /// <summary>
    /// Hero Section image settings
    /// </summary>
    public static class HeroSection
    {
        public const string ContainerPath = "hero-sections";
        public const int MaxWidth = 1920;
        public const int MaxHeight = 1080;
        public const int Quality = 90;
    }

    /// <summary>
    /// CTA Section image settings
    /// </summary>
    public static class CtaSection
    {
        public const string ContainerPath = "cta-sections";
        public const int MaxWidth = 1920;
        public const int MaxHeight = 1080;
        public const int Quality = 85;
    }

    /// <summary>
    /// Partner logo settings
    /// </summary>
    public static class Partner
    {
        public const string ContainerPath = "partners";
        public const int MaxWidth = 800;
        public const int MaxHeight = 800;
        public const int Quality = 90;
    }

    /// <summary>
    /// Team member profile image settings
    /// </summary>
    public static class TeamMember
    {
        public const string ContainerPath = "team-members";
        public const int MaxWidth = 800;
        public const int MaxHeight = 800;
        public const int Quality = 85;
    }

    /// <summary>
    /// Activity images settings
    /// </summary>
    public static class Activity
    {
        public const string ContainerPath = "activities";
        public const int MainImageMaxWidth = 1200;
        public const int MainImageMaxHeight = 800;
        public const int GalleryMaxWidth = 1920;
        public const int GalleryMaxHeight = 1080;
        public const int Quality = 85;
        public const int MaxGalleryImages = 10;
    }

    /// <summary>
    /// Tea Event images settings
    /// </summary>
    public static class TeaEvent
    {
        public const string ContainerPath = "tea-events";
        public const int MaxWidth = 1200;
        public const int MaxHeight = 800;
        public const int Quality = 85;
    }

    /// <summary>
    /// Course images settings
    /// </summary>
    public static class Course
    {
        public const string ContainerPath = "courses";
        public const int MaxWidth = 1200;
        public const int MaxHeight = 800;
        public const int Quality = 85;
    }

    /// <summary>
    /// Focus Area images settings
    /// </summary>
    public static class FocusArea
    {
        public const string ContainerPath = "focus-areas";
        public const int MaxWidth = 800;
        public const int MaxHeight = 600;
        public const int Quality = 85;
    }

    /// <summary>
    /// Feature icons settings
    /// </summary>
    public static class Feature
    {
        public const string ContainerPath = "features";
        public const int MaxWidth = 512;
        public const int MaxHeight = 512;
        public const int Quality = 90;
    }

    /// <summary>
    /// Instagram post images settings
    /// </summary>
    public static class InstagramPost
    {
        public const string ContainerPath = "instagram-posts";
        public const int MaxWidth = 1080;
        public const int MaxHeight = 1080;
        public const int Quality = 90;
    }

    /// <summary>
    /// Guelen Movement images settings
    /// </summary>
    public static class GuelenMovement
    {
        public const string ContainerPath = "guelen-movement";
        public const int MaxWidth = 1200;
        public const int MaxHeight = 800;
        public const int Quality = 85;
    }

    /// <summary>
    /// Thumbnail settings
    /// </summary>
    public static class Thumbnail
    {
        public const int Width = 300;
        public const int Height = 200;
        public const string Suffix = "-thumbnails";
    }

    /// <summary>
    /// Validation settings
    /// </summary>
    public static class Validation
    {
        /// <summary>
        /// Maximum file size in bytes (5 MB)
        /// </summary>
        public const int MaxFileSizeBytes = 5 * 1024 * 1024;

        /// <summary>
        /// Allowed MIME types
        /// </summary>
        public static readonly string[] AllowedMimeTypes = 
        {
            "image/jpeg",
            "image/png",
            "image/gif",
            "image/webp"
        };

        /// <summary>
        /// Allowed file extensions
        /// </summary>
        public static readonly string[] AllowedExtensions = 
        {
            ".jpg",
            ".jpeg",
            ".png",
            ".gif",
            ".webp"
        };
    }
}
