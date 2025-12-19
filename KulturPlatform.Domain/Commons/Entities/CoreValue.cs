using KulturPlatform.Domain.Commons.ValueObjects;

namespace KulturPlatform.Domain.Commons.Entities
{
    public class CoreValue : AboutUsItem
    {
        protected CoreValue() : base() { }

        private CoreValue(Guid id, Title titleTr, Title titleDe, Description descriptionTr, Description descriptionDe, int order)
            : base(id, titleTr, titleDe, descriptionTr, descriptionDe, order)
        {
        }

        public static CoreValue Create(Title titleTr, Title titleDe, Description descriptionTr, Description descriptionDe, int order)
            => new CoreValue(Guid.NewGuid(), titleTr, titleDe, descriptionTr, descriptionDe, order);
    }
}
