using System.Collections.Immutable;

namespace KulturPlatform.Domain.Commons.ValueObjects
{
    public record MediaGallery
    {
        public IReadOnlyList<Url> Images { get; init; }

        private MediaGallery() { Images = new List<Url>(); }

        // Constructor from Url VO list
        public MediaGallery(IReadOnlyList<Url> images)
        {
            if (images == null)
                throw new ArgumentException("Images cannot be null.");

            if (images.Count > 10)
                throw new ArgumentException("Gallery cannot contain more than 10 images.");

            Images = images.ToImmutableList();
        }

        // Constructor from raw string list
        public MediaGallery(IEnumerable<string> urls)
        {
            if (urls == null)
                throw new ArgumentException("URLs cannot be null.");

            var list = urls.Select(u => Url.Create(u)).ToList();


            if (list.Count > 10)
                throw new ArgumentException("Gallery cannot contain more than 10 images.");

            Images = list.ToImmutableList();
        }

        public static MediaGallery Empty() => new MediaGallery(new List<Url>());

        public bool IsEmpty => Images.Count == 0;
        public override string ToString() => $"MediaGallery: [{string.Join(", ", Images)}]";
    }

}
