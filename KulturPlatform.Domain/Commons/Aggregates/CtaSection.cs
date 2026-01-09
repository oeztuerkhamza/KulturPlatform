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
            Title primaryButtonTr,
            Title primaryButtonDe,
            Title secondaryButtonTr,
            Title secondaryButtonDe,
            Title donateButtonTr,
            Title donateButtonDe
        )
        {
            return new CtaSection(Guid.NewGuid())
            {
                TitleTr = titleTr,
                TitleDe = titleDe,
                DescriptionTr = descriptionTr,
                DescriptionDe = descriptionDe,
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
            Title primaryButtonTr,
            Title primaryButtonDe,
            Title secondaryButtonTr,
            Title secondaryButtonDe,
            Title donateButtonTr,
            Title donateButtonDe
        )
        {
            TitleTr = titleTr;
            TitleDe = titleDe;
            DescriptionTr = descriptionTr;
            DescriptionDe = descriptionDe;
            PrimaryButtonTr = primaryButtonTr;
            PrimaryButtonDe = primaryButtonDe;
            SecondaryButtonTr = secondaryButtonTr;
            SecondaryButtonDe = secondaryButtonDe;
            DonateButtonTr = donateButtonTr;
            DonateButtonDe = donateButtonDe;

            SetUpdatedAt(); // AuditableEntity’den geliyor
        }
    }
}
