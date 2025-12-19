using KulturPlatform.Domain.Commons.ValueObjects;

namespace KulturPlatform.Domain.Commons.Entities
{
    public class AboutUsItem : Entity
    {
        public Title TitleTr { get; private set; }
        public Title TitleDe { get; private set; }
        public Description DescriptionTr { get; private set; }
        public Description DescriptionDe { get; private set; }
        public int Order { get; private set; }

        // EF Core için
        protected AboutUsItem() : base(Guid.NewGuid()) { }

        // Protected constructor so subclasses can call it
        protected AboutUsItem(Guid id, Title titleTr, Title titleDe, Description descriptionTr, Description descriptionDe, int order)
            : base(id)
        {
            TitleTr = titleTr;
            TitleDe = titleDe;
            DescriptionTr = descriptionTr;
            DescriptionDe = descriptionDe;
            Order = order;
        }

        // Factory
        public static AboutUsItem Create(Title titleTr, Title titleDe, Description descriptionTr, Description descriptionDe, int order)
        {
            if (titleTr is null) throw new ArgumentNullException(nameof(titleTr));
            if (titleDe is null) throw new ArgumentNullException(nameof(titleDe));
            if (descriptionTr is null) throw new ArgumentNullException(nameof(descriptionTr));
            if (descriptionDe is null) throw new ArgumentNullException(nameof(descriptionDe));
            if (order < 0) throw new ArgumentOutOfRangeException(nameof(order));

            return new AboutUsItem(Guid.NewGuid(), titleTr, titleDe, descriptionTr, descriptionDe, order);
        }

        // Update method
        public void Update(Title titleTr, Title titleDe, Description descriptionTr, Description descriptionDe, int order)
        {
            TitleTr = titleTr ?? TitleTr;
            TitleDe = titleDe ?? TitleDe;
            DescriptionTr = descriptionTr ?? DescriptionTr;
            DescriptionDe = descriptionDe ?? DescriptionDe;
            Order = order;
        }
    }
}