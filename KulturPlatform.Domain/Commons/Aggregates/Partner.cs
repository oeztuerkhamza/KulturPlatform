using KulturPlatform.Domain.Commons.Entities;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;

namespace KulturPlatform.Domain.Commons.Aggregates
{
    public class Partner : AuditableEntity, IAggregateRoot
    {
        public PartnerName Name { get; private set; }
        public Url? LogoUrl { get; private set; }
        public Url? WebsiteUrl { get; private set; }
        public DisplayOrder DisplayOrder { get; private set; }

        public bool IsActive { get; private set; } = true;

        private Partner(Guid id) : base(id)
        {
        }

        private Partner(Guid id, PartnerName name, DisplayOrder order, Url? logoUrl, Url? websiteUrl)
            : base(id)
        {
            Name = name;
            DisplayOrder = order;
            LogoUrl = logoUrl;
            WebsiteUrl = websiteUrl;
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
        }

        public static Partner CreateNew(PartnerName name, DisplayOrder order, Url? logoUrl = null, Url? websiteUrl = null)
        {
            return new Partner(Guid.NewGuid(), name, order, logoUrl, websiteUrl);
        }

        public void Update(PartnerName name, DisplayOrder order, Url? logoUrl, Url? websiteUrl)
        {
            Name = name;
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
