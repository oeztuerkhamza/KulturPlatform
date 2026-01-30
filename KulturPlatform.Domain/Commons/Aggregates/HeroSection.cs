using KulturPlatform.Domain.Commons.Entities;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;

namespace KulturPlatform.Domain.Commons.AggregateRoot
{
    public class HeroSection : AuditableEntity, IAggregateRoot
    {
        public Title TitleTr { get; private set; }
        public Title TitleDe { get; private set; }

        public Title SubtitleTr { get; private set; }
        public Title SubtitleDe { get; private set; }

        public Description DescriptionTr { get; private set; }
        public Description DescriptionDe { get; private set; }

        // ? Hybrid image storage: either URL or Database (flat structure)
        public Url? BackgroundImageUrl { get; private set; }
        public ImageData? BackgroundImageData { get; private set; }

        public Title PrimaryButtonTextTr { get; private set; }
        public Title PrimaryButtonTextDe { get; private set; }

        public Title SecondaryButtonTextTr { get; private set; }
        public Title SecondaryButtonTextDe { get; private set; }

        private HeroSection(Guid id) : base(id) { }

        public static HeroSection Create(
            Title titleTr,
            Title titleDe,
            Title subtitleTr,
            Title subtitleDe,
            Description descriptionTr,
            Description descriptionDe,
            Url? backgroundImageUrl,
            ImageData? backgroundImageData,
            Title primaryButtonTextTr,
            Title primaryButtonTextDe,
            Title secondaryButtonTextTr,
            Title secondaryButtonTextDe
        )
        {
            // Validate that only one image source is provided
            if (backgroundImageUrl != null && backgroundImageData != null)
                throw new ArgumentException("Cannot specify both ImageUrl and ImageData. Choose one image source.");

            return new HeroSection(Guid.NewGuid())
            {
                TitleTr = titleTr,
                TitleDe = titleDe,
                SubtitleTr = subtitleTr,
                SubtitleDe = subtitleDe,
                DescriptionTr = descriptionTr,
                DescriptionDe = descriptionDe,
                BackgroundImageUrl = backgroundImageUrl,
                BackgroundImageData = backgroundImageData,
                PrimaryButtonTextTr = primaryButtonTextTr,
                PrimaryButtonTextDe = primaryButtonTextDe,
                SecondaryButtonTextTr = secondaryButtonTextTr,
                SecondaryButtonTextDe = secondaryButtonTextDe,
                CreatedAt = DateTime.UtcNow
            };
        }

        public void Update(
            Title titleTr,
            Title titleDe,
            Title subtitleTr,
            Title subtitleDe,
            Description descriptionTr,
            Description descriptionDe,
            Url? backgroundImageUrl,
            ImageData? backgroundImageData,
            Title primaryButtonTextTr,
            Title primaryButtonTextDe,
            Title secondaryButtonTextTr,
            Title secondaryButtonTextDe
        )
        {
            // Validate that only one image source is provided
            if (backgroundImageUrl != null && backgroundImageData != null)
                throw new ArgumentException("Cannot specify both ImageUrl and ImageData. Choose one image source.");

            TitleTr = titleTr;
            TitleDe = titleDe;
            SubtitleTr = subtitleTr;
            SubtitleDe = subtitleDe;
            DescriptionTr = descriptionTr;
            DescriptionDe = descriptionDe;
            
            // Clear both first
            BackgroundImageUrl = null;
            BackgroundImageData = null;
            
            // Then set the appropriate one
            if (backgroundImageData != null)
            {
                BackgroundImageData = backgroundImageData;
            }
            else if (backgroundImageUrl != null)
            {
                BackgroundImageUrl = backgroundImageUrl;
            }
            
            PrimaryButtonTextTr = primaryButtonTextTr;
            PrimaryButtonTextDe = primaryButtonTextDe;
            SecondaryButtonTextTr = secondaryButtonTextTr;
            SecondaryButtonTextDe = secondaryButtonTextDe;

            SetUpdatedAt();
        }

        /// <summary>
        /// Gets the background image source for frontend
        /// Returns ImageData's data URI if available, otherwise ImageUrl
        /// </summary>
        public string? GetBackgroundImageSource()
        {
            if (BackgroundImageData != null)
                return BackgroundImageData.GetDataUri();
            
            return BackgroundImageUrl?.Value;
        }

        /// <summary>
        /// Checks if background image exists
        /// </summary>
        public bool HasBackgroundImage() => BackgroundImageUrl != null || BackgroundImageData != null;
    }
}
