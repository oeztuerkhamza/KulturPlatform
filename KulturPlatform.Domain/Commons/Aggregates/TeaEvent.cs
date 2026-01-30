using KulturPlatform.Domain.Commons.Entities;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;

namespace KulturPlatform.Domain.Commons.Aggregates
{
    public class TeaEvent : AuditableEntity, IAggregateRoot
    {
        public Title TitleTurkish { get; private set; }
        public Title TitleGerman { get; private set; }

        public TeaEventContent Content { get; private set; }

        public string Date { get; private set; }
        public string Time { get; private set; }
        public Location Location { get; private set; }
        
        // ✅ Hybrid image storage
        public Url? ImageUrl { get; private set; }
        public ImageData? ImageData { get; private set; }

        public bool IsActive { get; private set; } = true;

        private TeaEvent(Guid id) : base(id)
        {
        }

        private TeaEvent(
            Guid id,
            Title titleTr,
            Title titleDe,
            TeaEventContent content,
            string date,
            string time,
            Location location,
            Url? imageUrl,
            ImageData? imageData)
            : base(id)
        {
            // Validate that only one image source is provided
            if (imageUrl != null && imageData != null)
                throw new ArgumentException("Cannot specify both ImageUrl and ImageData. Choose one image source.");

            TitleTurkish = titleTr;
            TitleGerman = titleDe;
            Content = content;
            Date = date;
            Time = time;
            Location = location;
            ImageUrl = imageUrl;
            ImageData = imageData;
            CreatedAt = DateTime.UtcNow;
        }

        public static TeaEvent CreateNew(
            Title titleTr,
            Title titleDe,
            TeaEventContent content,
            string date,
            string time,
            Location location,
            Url? imageUrl = null,
            ImageData? imageData = null)
        {
            return new TeaEvent(
                Guid.NewGuid(),
                titleTr,
                titleDe,
                content,
                date,
                time,
                location,
                imageUrl,
                imageData);
        }

        public void UpdateContent(TeaEventContent content)
        {
            Content = content;
            SetUpdatedAt();
        }

        public void UpdateTitle(Title titleTurkish, Title titleGerman)
        {
            TitleTurkish = titleTurkish;
            TitleGerman = titleGerman;
            SetUpdatedAt();
        }

        public void UpdateImage(Url? imageUrl, ImageData? imageData)
        {
            // Validate that only one image source is provided
            if (imageUrl != null && imageData != null)
                throw new ArgumentException("Cannot specify both ImageUrl and ImageData. Choose one image source.");

            // Clear both first
            ImageUrl = null;
            ImageData = null;

            // Then set the appropriate one
            if (imageData != null)
            {
                ImageData = imageData;
            }
            else if (imageUrl != null)
            {
                ImageUrl = imageUrl;
            }

            SetUpdatedAt();
        }

        public void UpdateLocation(Location location)
        {
            Location = location;
            SetUpdatedAt();
        }

        public void Reschedule(string date, string time)
        {
            Date = date;
            Time = time;
            SetUpdatedAt();
        }

        public void Deactivate()
        {
            IsActive = false;
            SetUpdatedAt();
        }

        public void Activate()
        {
            IsActive = true;
            SetUpdatedAt();
        }

        /// <summary>
        /// Gets the image source for frontend
        /// </summary>
        public string? GetImageSource()
        {
            if (ImageData != null)
                return ImageData.GetDataUri();
            
            return ImageUrl?.Value;
        }

        /// <summary>
        /// Checks if image exists
        /// </summary>
        public bool HasImage() => ImageUrl != null || ImageData != null;
    }
}