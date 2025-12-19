using KulturPlatform.Domain.Commons.ValueObjects;

namespace KulturPlatform.Domain.Commons.Entities
{
    public class ActivityArea : AboutUsItem
    {
        protected ActivityArea() : base() { }

        private ActivityArea(Guid id, Title titleTr, Title titleDe, Description descriptionTr, Description descriptionDe, int order)
            : base(id, titleTr, titleDe, descriptionTr, descriptionDe, order)
        {
        }

        public static ActivityArea Create(Title titleTr, Title titleDe, Description descriptionTr, Description descriptionDe, int order)
            => new ActivityArea(Guid.NewGuid(), titleTr, titleDe, descriptionTr, descriptionDe, order);
    }
}
