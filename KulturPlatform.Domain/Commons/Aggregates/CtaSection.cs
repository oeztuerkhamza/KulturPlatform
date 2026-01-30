using KulturPlatform.Domain.Commons.Entities;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;

namespace KulturPlatform.Domain.Commons.AggregateRoot
{
    public class CtaSection : AuditableEntity, IAggregateRoot
    {
        public Title TitleTr { get; private set; }
        public Title TitleDe { get; private set; }

        public Description DescriptionTr { get; private set; }
        public Description DescriptionDe { get; private set; }

        // ✅ Hybrid background image storage
        public Url? BackgroundImageUrl { get; private set; }
        public ImageData? BackgroundImageData { get; private set; }

        public Title PrimaryButtonTr { get; private set; }
        public Title PrimaryButtonDe { get; private set; }

        public Title SecondaryButtonTr { get; private set; }
        public Title SecondaryButtonDe { get; private set; }

        public Title DonateButtonTr { get; private set; }
        public Title DonateButtonDe { get; private set; }

        private CtaSection(Guid id) : base(id) { }

        public static CtaSection Create(
            Title titleTr,
            Title titleDe,
            Description descriptionTr,
            Description descriptionDe,
            Url? backgroundImageUrl,
            ImageData? backgroundImageData,
            Title primaryButtonTr,
            Title primaryButtonDe,
            Title secondaryButtonTr,
            Title secondaryButtonDe,
            Title donateButtonTr,
            Title donateButtonDe
        )
        {
            // Validate that only one image source is provided
            if (backgroundImageUrl != null && backgroundImageData != null)
                throw new ArgumentException("Cannot specify both ImageUrl and ImageData. Choose one image source.");

            return new CtaSection(Guid.NewGuid())
            {
                TitleTr = titleTr,
                TitleDe = titleDe,
                DescriptionTr = descriptionTr,
                DescriptionDe = descriptionDe,
                BackgroundImageUrl = backgroundImageUrl,
                BackgroundImageData = backgroundImageData,
                PrimaryButtonTr = primaryButtonTr,
                PrimaryButtonDe = primaryButtonDe,
                SecondaryButtonTr = secondaryButtonTr,
                SecondaryButtonDe = secondaryButtonDe,
                DonateButtonTr = donateButtonTr,
                DonateButtonDe = donateButtonDe,
                CreatedAt = DateTime.UtcNow
            };
        }

        // =========================
        // Update method
        // =========================
        public void Update(
            Title titleTr,
            Title titleDe,
            Description descriptionTr,
            Description descriptionDe,
            Url? backgroundImageUrl,
            ImageData? backgroundImageData,
            Title primaryButtonTr,
            Title primaryButtonDe,
            Title secondaryButtonTr,
            Title secondaryButtonDe,
            Title donateButtonTr,
            Title donateButtonDe
        )
        {
            // Validate that only one image source is provided
            if (backgroundImageUrl != null && backgroundImageData != null)
                throw new ArgumentException("Cannot specify both ImageUrl and ImageData. Choose one image source.");

            TitleTr = titleTr;
            TitleDe = titleDe;
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
            
            PrimaryButtonTr = primaryButtonTr;
            PrimaryButtonDe = primaryButtonDe;
            SecondaryButtonTr = secondaryButtonTr;
            SecondaryButtonDe = secondaryButtonDe;
            DonateButtonTr = donateButtonTr;
            DonateButtonDe = donateButtonDe;

            SetUpdatedAt(); // AuditableEntity’den geliyor
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
