using KulturPlatform.Domain.Commons.Entities;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;

namespace KulturPlatform.Domain.Commons.AggregateRoot
{
    public class Feature : AuditableEntity, IAggregateRoot
    {
        public Title TitleTr { get; private set; }
        public Title TitleDe { get; private set; }

        public Description DescriptionTr { get; private set; }
        public Description DescriptionDe { get; private set; }

        // Renk, ikon veya kategori bilgisi
        public string Color { get; private set; }

        private Feature(Guid id) : base(id) { }

        public static Feature Create(
            Title titleTr,
            Title titleDe,
            Description descriptionTr,
            Description descriptionDe,
            string color
        )
        {
            return new Feature(Guid.NewGuid())
            {
                TitleTr = titleTr,
                TitleDe = titleDe,
                DescriptionTr = descriptionTr,
                DescriptionDe = descriptionDe,
                Color = color,
                CreatedAt = DateTime.UtcNow
            };
        }

        public void Update(
            Title titleTr,
            Title titleDe,
            Description descriptionTr,
            Description descriptionDe,
            string color
        )
        {
            TitleTr = titleTr;
            TitleDe = titleDe;
            DescriptionTr = descriptionTr;
            DescriptionDe = descriptionDe;
            Color = color;
            SetUpdatedAt();
        }
    }
}
