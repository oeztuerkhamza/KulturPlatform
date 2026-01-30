namespace KulturPlatform.Domain.Commons.ValueObjects
{
    /// <summary>
    /// Represents an image that can be stored either as URL or in database
    /// Reusable value object for all entities needing hybrid image storage
    /// </summary>
    public sealed record HybridImage
    {
        public Url? ImageUrl { get; init; }
        public ImageData? ImageData { get; init; }

        // EF Core için
        private HybridImage()
        {
        }

        private HybridImage(Url? imageUrl, ImageData? imageData)
        {
            // Validate that only one source is provided
            if (imageUrl != null && imageData != null)
                throw new ArgumentException("Cannot specify both ImageUrl and ImageData. Choose one image source.");

            ImageUrl = imageUrl;
            ImageData = imageData;
        }

        /// <summary>
        /// Create from URL
        /// </summary>
        public static HybridImage FromUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentException("URL cannot be empty.");

            return new HybridImage(Url.Create(url), null);
        }

        /// <summary>
        /// Create from ImageData (database storage)
        /// </summary>
        public static HybridImage FromImageData(ImageData imageData)
        {
            if (imageData == null)
                throw new ArgumentNullException(nameof(imageData));

            return new HybridImage(null, imageData);
        }

        /// <summary>
        /// Create empty/null hybrid image
        /// </summary>
        public static HybridImage? Empty() => null;

        /// <summary>
        /// Gets the image source for frontend display
        /// Returns ImageData's data URI if available, otherwise ImageUrl
        /// </summary>
        public string? GetImageSource()
        {
            if (ImageData != null)
                return ImageData.GetDataUri();

            return ImageUrl?.Value;
        }

        /// <summary>
        /// Checks if image is from URL
        /// </summary>
        public bool IsUrl() => ImageUrl != null;

        /// <summary>
        /// Checks if image is from database
        /// </summary>
        public bool IsDatabase() => ImageData != null;

        /// <summary>
        /// Checks if hybrid image has any data
        /// </summary>
        public bool HasImage() => ImageUrl != null || ImageData != null;

        public override string ToString()
        {
            if (ImageData != null)
                return $"Database Image: {ImageData.FileName}";
            if (ImageUrl != null)
                return $"URL: {ImageUrl.Value}";
            return "No Image";
        }
    }
}
