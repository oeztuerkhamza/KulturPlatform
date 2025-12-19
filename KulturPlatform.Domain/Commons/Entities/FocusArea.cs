using KulturPlatform.Domain.Commons.ValueObjects;

namespace KulturPlatform.Domain.Commons.Entities
{
    public class FocusArea : AboutUsItem
    {
        protected FocusArea() : base() { }

        private FocusArea(Guid id, Title titleTr, Title titleDe, Description descriptionTr, Description descriptionDe, int order)
            : base(id, titleTr, titleDe, descriptionTr, descriptionDe, order)
        {
        }

        public static FocusArea Create(Title titleTr, Title titleDe, Description descriptionTr, Description descriptionDe, int order)
            => new FocusArea(Guid.NewGuid(), titleTr, titleDe, descriptionTr, descriptionDe, order);
    }
}
