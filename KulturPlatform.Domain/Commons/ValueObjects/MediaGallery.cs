using System.Collections.Immutable;
using System.Text;

namespace KulturPlatform.Domain.Commons.ValueObjects
{
    public record MediaGallery
    {
        public IReadOnlyList<Url> Images { get; init; }

        // Private parameterless constructor for EF Core
        private MediaGallery() { Images = new List<Url>(); }

        // Constructor from Url VO list
        public MediaGallery(IReadOnlyList<Url> images)
        {
            if (images == null)
                throw new("Images cannot be null.");

            if (images.Count < 1)
                throw new("Gallery must contain at least 1 image.");

            if (images.Count > 10)
                throw new("Gallery cannot contain more than 10 images.");

            // Immutable copy
            Images = images.ToImmutableList();
        }

        // Constructor from raw string list
        public MediaGallery(IEnumerable<string> urls)
        {
            if (urls == null)
                throw new("URLs cannot be null.");

            var list = urls.Select(u => Url.Create(u)).ToList();


            if (list.Count < 1)
                throw new("Gallery must contain at least 1 image.");

            if (list.Count > 10)
                throw new("Gallery cannot contain more than 10 images.");

            Images = list.ToImmutableList();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("MediaGallery: [");
            for (int i = 0; i < Images.Count; i++)
            {
                sb.Append(Images[i].ToString());
                if (i < Images.Count - 1)
                    sb.Append(", ");
            }

            sb.Append("]");
            return sb.ToString();
        }
    }
}
