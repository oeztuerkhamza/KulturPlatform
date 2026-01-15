using KulturPlatform.Domain.Commons.Entities;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;

namespace KulturPlatform.Domain.Commons.Aggregates
{
    public class Partner : AuditableEntity, IAggregateRoot
    {
        public PartnerName Name { get; private set; }
        public Description DescriptionTr { get; private set; }
        public Description DescriptionDe { get; private set; }
        public Image? LogoUrl { get; private set; }
        public Image? WebsiteUrl { get; private set; }
        public DisplayOrder DisplayOrder { get; private set; }

        public bool IsActive { get; private set; } = true;

        private Partner(Guid id) : base(id)
        {
        }

        private Partner(Guid id, PartnerName name, Description descriptionTr, Description descriptionDe, DisplayOrder order, Image? logoUrl, Image? websiteUrl)
            : base(id)
        {
            Name = name;
            DescriptionTr = descriptionTr;
            DescriptionDe = descriptionDe;
            DisplayOrder = order;
            LogoUrl = logoUrl;
            WebsiteUrl = websiteUrl;
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
        }

        public static Partner CreateNew(PartnerName name, Description descriptionTr, Description descriptionDe, DisplayOrder order, Image? logoUrl = null, Image? websiteUrl = null)
        {
            return new Partner(Guid.NewGuid(), name, descriptionTr, descriptionDe, order, logoUrl, websiteUrl);
        }

        public void Update(PartnerName name, Description descriptionTr, Description descriptionDe, DisplayOrder order, Image? logoUrl, Image? websiteUrl)
        {
            Name = name;
            DescriptionTr = descriptionTr;
            DescriptionDe = descriptionDe;
            DisplayOrder = order;
            LogoUrl = logoUrl;
            WebsiteUrl = websiteUrl;
            SetUpdatedAt();
        }

        public void Activate()
        {
            if (!IsActive)
            {
                IsActive = true;
                SetUpdatedAt();
            }
        }

        public void Deactivate()
        {
            if (IsActive)
            {
                IsActive = false;
                SetUpdatedAt();
            }
        }
    }
}
