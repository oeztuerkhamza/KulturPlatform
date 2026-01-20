namespace KulturPlatform.Domain.Commons.ValueObjects
{
    /// <summary>
    /// Represents a gallery image that can be either a URL or database-stored image data
    /// </summary>
    public sealed record GalleryImage
    {
        public Url? ImageUrl { get; init; }
        public ImageData? ImageData { get; init; }

        // EF Core için
        private GalleryImage()
        {
        }

        private GalleryImage(Url? imageUrl, ImageData? imageData)
        {
            ImageUrl = imageUrl;
            ImageData = imageData;
        }

        public static GalleryImage FromUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentException("URL cannot be empty.");

            return new GalleryImage(Url.Create(url), null);
        }

        public static GalleryImage FromImageData(ImageData imageData)
        {
            if (imageData == null)
                throw new ArgumentNullException(nameof(imageData));

            return new GalleryImage(null, imageData);
        }

        /// <summary>
        /// Gets the image source for frontend display
        /// Returns ImageData's data URI if available, otherwise ImageUrl
        /// </summary>
        public string GetImageSource()
        {
            if (ImageData != null)
                return ImageData.GetDataUri();

            if (ImageUrl != null)
                return ImageUrl.Value;

            throw new InvalidOperationException("Gallery image must have either URL or ImageData.");
        }

        public bool HasUrl() => ImageUrl != null;
        public bool HasImageData() => ImageData != null;
    }
}
