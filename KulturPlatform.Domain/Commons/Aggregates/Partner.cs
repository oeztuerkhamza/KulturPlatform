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
        
        // ✅ Hybrid logo storage: either URL or Database (flat structure)
        public Url? LogoUrl { get; private set; }
        public ImageData? LogoData { get; private set; }
        
        // Website URL stays as Url (it's an actual web address, not an image)
        public Url? WebsiteUrl { get; private set; }
        
        public DisplayOrder DisplayOrder { get; private set; }

        public bool IsActive { get; private set; } = true;

        private Partner(Guid id) : base(id)
        {
        }

        private Partner(
            Guid id, 
            PartnerName name, 
            Description descriptionTr, 
            Description descriptionDe, 
            DisplayOrder order, 
            Url? logoUrl,
            ImageData? logoData,
            Url? websiteUrl)
            : base(id)
        {
            // Validate that only one image source is provided
            if (logoUrl != null && logoData != null)
                throw new ArgumentException("Cannot specify both LogoUrl and LogoData. Choose one image source.");

            Name = name;
            DescriptionTr = descriptionTr;
            DescriptionDe = descriptionDe;
            DisplayOrder = order;
            LogoUrl = logoUrl;
            LogoData = logoData;
            WebsiteUrl = websiteUrl;
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
        }

        public static Partner CreateNew(
            PartnerName name, 
            Description descriptionTr, 
            Description descriptionDe, 
            DisplayOrder order, 
            Url? logoUrl = null,
            ImageData? logoData = null,
            Url? websiteUrl = null)
        {
            return new Partner(Guid.NewGuid(), name, descriptionTr, descriptionDe, order, logoUrl, logoData, websiteUrl);
        }

        public void Update(
            PartnerName name, 
            Description descriptionTr, 
            Description descriptionDe, 
            DisplayOrder order, 
            Url? logoUrl,
            ImageData? logoData,
            Url? websiteUrl)
        {
            // Validate that only one image source is provided
            if (logoUrl != null && logoData != null)
                throw new ArgumentException("Cannot specify both LogoUrl and LogoData. Choose one image source.");

            Name = name;
            DescriptionTr = descriptionTr;
            DescriptionDe = descriptionDe;
            DisplayOrder = order;
            
            // Clear both first
            LogoUrl = null;
            LogoData = null;
            
            // Then set the appropriate one
            if (logoData != null)
            {
                LogoData = logoData;
            }
            else if (logoUrl != null)
            {
                LogoUrl = logoUrl;
            }
            
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

        /// <summary>
        /// Gets the logo source for frontend
        /// Returns ImageData's data URI if available, otherwise LogoUrl
        /// </summary>
        public string? GetLogoSource()
        {
            if (LogoData != null)
                return LogoData.GetDataUri();
            
            return LogoUrl?.Value;
        }

        /// <summary>
        /// Checks if logo exists
        /// </summary>
        public bool HasLogo() => LogoUrl != null || LogoData != null;
    }
}
