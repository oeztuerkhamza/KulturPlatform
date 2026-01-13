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

        public Image BackgroundImageUrl { get; private set; }

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
            Image backgroundImageUrl,
            Title primaryButtonTextTr,
            Title primaryButtonTextDe,
            Title secondaryButtonTextTr,
            Title secondaryButtonTextDe
        )
        {
            return new HeroSection(Guid.NewGuid())
            {
                TitleTr = titleTr,
                TitleDe = titleDe,
                SubtitleTr = subtitleTr,
                SubtitleDe = subtitleDe,
                DescriptionTr = descriptionTr,
                DescriptionDe = descriptionDe,
                BackgroundImageUrl = backgroundImageUrl,
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
            Image backgroundImageUrl,
            Title primaryButtonTextTr,
            Title primaryButtonTextDe,
            Title secondaryButtonTextTr,
            Title secondaryButtonTextDe
        )
        {
            TitleTr = titleTr;
            TitleDe = titleDe;
            SubtitleTr = subtitleTr;
            SubtitleDe = subtitleDe;
            DescriptionTr = descriptionTr;
            DescriptionDe = descriptionDe;
            BackgroundImageUrl = backgroundImageUrl;
            PrimaryButtonTextTr = primaryButtonTextTr;
            PrimaryButtonTextDe = primaryButtonTextDe;
            SecondaryButtonTextTr = secondaryButtonTextTr;
            SecondaryButtonTextDe = secondaryButtonTextDe;

            SetUpdatedAt();
        }
    }
}
