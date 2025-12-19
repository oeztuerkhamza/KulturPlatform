using KulturPlatform.Domain.Commons.Entities;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;

namespace KulturPlatform.Domain.Commons.Aggregates
{
    public class GuelenMovement : AuditableEntity, IAggregateRoot
    {
        public Title TitleTurkish { get; private set; }
        public Title TitleGerman { get; private set; }
        public string ContentTurkish { get; private set; }
        public string ContentGerman { get; private set; }
        public Url ImageUrl { get; private set; }
        private GuelenMovement(Guid id) : base(id) { }

        private GuelenMovement(Guid id, Title titleTurkish, Title titleGerman, string contentTurkish, string contentGerman, Url imageUrl)
            : base(id)
        {
            TitleTurkish = titleTurkish;
            TitleGerman = titleGerman;
            ContentTurkish = contentTurkish;
            ContentGerman = contentGerman;
            ImageUrl = imageUrl;
            CreatedAt = DateTime.UtcNow;
        }
        public static GuelenMovement CreateNew(Title titleTurkish, Title titleGerman, string contentTurkish, string contentGerman, Url imageUrl)
        {
            return new GuelenMovement(Guid.NewGuid(), titleTurkish, titleGerman, contentTurkish, contentGerman, imageUrl);
        }
        public void Update(Title titleTurkish, Title titleGerman, string contentTurkish, string contentGerman, Url imageUrl)
        {
            UpdateTitle(titleTurkish, titleGerman);
            ContentTurkish = contentTurkish;
            ContentGerman = contentGerman;
            ImageUrl = imageUrl;
            SetUpdatedAt();
        }
        public void UpdateTitle(Title titleTr, Title titleDe)
        {
            TitleTurkish = titleTr;
            TitleGerman = titleDe;
            SetUpdatedAt();
        }

    }
}
