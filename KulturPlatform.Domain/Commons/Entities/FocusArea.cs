using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;

namespace KulturPlatform.Domain.Commons.Entities
{
    public class FocusArea : AuditableEntity, IAggregateRoot
    {
        public Title TitleTr { get; private set; }
        public Title TitleDe { get; private set; }
        public Description DescriptionTr { get; private set; }
        public Description DescriptionDe { get; private set; }
        
        // ? Hybrid icon/image storage
        public Url? IconUrl { get; private set; }
        public ImageData? IconData { get; private set; }
        
        public int Order { get; private set; }

        protected FocusArea() { }

        private FocusArea(
            Guid id, 
            Title titleTr, 
            Title titleDe, 
            Description descriptionTr, 
            Description descriptionDe,
            Url? iconUrl,
            ImageData? iconData,
            int order)
            : base(id)
        {
            // Validate that only one image source is provided
            if (iconUrl != null && iconData != null)
                throw new ArgumentException("Cannot specify both IconUrl and IconData. Choose one image source.");

            TitleTr = titleTr;
            TitleDe = titleDe;
            DescriptionTr = descriptionTr;
            DescriptionDe = descriptionDe;
            IconUrl = iconUrl;
            IconData = iconData;
            Order = order;
        }

        public static FocusArea Create(
            Title titleTr, 
            Title titleDe, 
            Description descriptionTr, 
            Description descriptionDe,
            Url? iconUrl,
            ImageData? iconData,
            int order)
        {
            if (order < 0) throw new ArgumentOutOfRangeException(nameof(order));
            return new FocusArea(Guid.NewGuid(), titleTr, titleDe, descriptionTr, descriptionDe, iconUrl, iconData, order);
        }

        public void Update(
            Title titleTr, 
            Title titleDe, 
            Description descriptionTr, 
            Description descriptionDe,
            Url? iconUrl,
            ImageData? iconData,
            int order)
        {
            // Validate that only one image source is provided
            if (iconUrl != null && iconData != null)
                throw new ArgumentException("Cannot specify both IconUrl and IconData. Choose one image source.");

            TitleTr = titleTr;
            TitleDe = titleDe;
            DescriptionTr = descriptionTr;
            DescriptionDe = descriptionDe;
            
            // Clear both first
            IconUrl = null;
            IconData = null;
            
            // Then set the appropriate one
            if (iconData != null)
            {
                IconData = iconData;
            }
            else if (iconUrl != null)
            {
                IconUrl = iconUrl;
            }
            
            Order = order;
            SetUpdatedAt();
        }

        /// <summary>
        /// Gets the icon source for frontend
        /// </summary>
        public string? GetIconSource()
        {
            if (IconData != null)
                return IconData.GetDataUri();
            
            return IconUrl?.Value;
        }

        /// <summary>
        /// Checks if icon exists
        /// </summary>
        public bool HasIcon() => IconUrl != null || IconData != null;
    }
}
