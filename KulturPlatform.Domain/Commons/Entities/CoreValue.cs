using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;

namespace KulturPlatform.Domain.Commons.Entities
{
    public class CoreValue : AuditableEntity, IAggregateRoot
    {
        public Title TitleTr { get; private set; }
        public Title TitleDe { get; private set; }
        public Description DescriptionTr { get; private set; }
        public Description DescriptionDe { get; private set; }
        public int Order { get; private set; }

        protected CoreValue() { }

        private CoreValue(Guid id, Title titleTr, Title titleDe, Description descriptionTr, Description descriptionDe, int order)
            : base(id)
        {
            TitleTr = titleTr;
            TitleDe = titleDe;
            DescriptionTr = descriptionTr;
            DescriptionDe = descriptionDe;
            Order = order;
        }

        public static CoreValue Create(Title titleTr, Title titleDe, Description descriptionTr, Description descriptionDe, int order)
        {
            if (order < 0) throw new ArgumentOutOfRangeException(nameof(order));
            return new CoreValue(Guid.NewGuid(), titleTr, titleDe, descriptionTr, descriptionDe, order);
        }

        public void Update(Title titleTr, Title titleDe, Description descriptionTr, Description descriptionDe, int order)
        {
            TitleTr = titleTr;
            TitleDe = titleDe;
            DescriptionTr = descriptionTr;
            DescriptionDe = descriptionDe;
            Order = order;
            SetUpdatedAt();
        }
    }
}
