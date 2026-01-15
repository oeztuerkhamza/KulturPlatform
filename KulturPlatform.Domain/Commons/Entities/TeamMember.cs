using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;

namespace KulturPlatform.Domain.Commons.Entities
{
    public class TeamMember : AuditableEntity, IAggregateRoot
    {
        public Name Name { get; private set; }
        public Title TitleTr { get; private set; }
        public Title TitleDe { get; private set; }
        public Description? DescriptionTr { get; private set; }
        public Description? DescriptionDe { get; private set; }
        public string ImageUrl { get; private set; }
        public int Order { get; private set; }

        // EF Core için
        protected TeamMember() : base(Guid.NewGuid()) { }

        // Private constructor, sadece Create kullanılacak
        private TeamMember(Guid id, Name name, Title titleTr, Title titleDe, Description? descriptionTr, Description? descriptionDe, string imageUrl, int order)
            : base(id)
        {
            Name = name;
            TitleTr = titleTr;
            TitleDe = titleDe;
            DescriptionTr = descriptionTr;
            DescriptionDe = descriptionDe;
            ImageUrl = imageUrl;
            Order = order;
        }

        // Factory
        public static TeamMember Create(Name name, Title titleTr, Title titleDe, Description? descriptionTr, Description? descriptionDe, string imageUrl, int order)
        {
            if (name is null) throw new ArgumentNullException(nameof(name));
            if (titleTr is null) throw new ArgumentNullException(nameof(titleTr));
            if (titleDe is null) throw new ArgumentNullException(nameof(titleDe));
            if (order < 0) throw new ArgumentOutOfRangeException(nameof(order));

            return new TeamMember(Guid.NewGuid(), name, titleTr, titleDe, descriptionTr, descriptionDe, imageUrl, order);
        }

        // Update method
        public void Update(Name name, Title titleTr, Title titleDe, Description? descriptionTr, Description? descriptionDe, string imageUrl, int order)
        {
            Name = name ?? Name;
            TitleTr = titleTr ?? TitleTr;
            TitleDe = titleDe ?? TitleDe;
            DescriptionTr = descriptionTr;
            DescriptionDe = descriptionDe;
            ImageUrl = imageUrl ?? ImageUrl;
            Order = order;
            SetUpdatedAt();
        }
    }
}