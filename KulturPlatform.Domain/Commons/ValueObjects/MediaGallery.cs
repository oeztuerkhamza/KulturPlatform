namespace KulturPlatform.Domain.Commons.ValueObjects
{
    public record MediaGallery
    {
        // ✅ Changed from IReadOnlyList to List for EF Core compatibility
        public List<GalleryImage> Images { get; set; }

        private MediaGallery() { Images = new List<GalleryImage>(); }

        // Constructor from GalleryImage list
        public MediaGallery(IEnumerable<GalleryImage> images)
        {
            if (images == null)
                throw new ArgumentException("Images cannot be null.");

            var list = images.ToList();

            if (list.Count > 10)
                throw new ArgumentException("Gallery cannot contain more than 10 images.");

            Images = list; // Direct assignment instead of ToImmutableList()
        }

        // Constructor from raw string list (URLs only - for backward compatibility)
        public MediaGallery(IEnumerable<string> urls)
        {
            if (urls == null)
                throw new ArgumentException("URLs cannot be null.");

            var list = urls
                .Where(u => !string.IsNullOrWhiteSpace(u))
                .Select(u => GalleryImage.FromUrl(u))
                .ToList();

            if (list.Count > 10)
                throw new ArgumentException("Gallery cannot contain more than 10 images.");

            Images = list; // Direct assignment instead of ToImmutableList()
        }

        public static MediaGallery Empty() => new MediaGallery(new List<GalleryImage>());

        public bool IsEmpty => Images.Count == 0;
        
        public IEnumerable<string> GetImageSources() => Images.Select(img => img.GetImageSource());
        
        public override string ToString() => $"MediaGallery: {Images.Count} images";
    }
}
