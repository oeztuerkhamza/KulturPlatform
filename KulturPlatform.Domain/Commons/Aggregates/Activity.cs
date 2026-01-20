using KulturPlatform.Domain.Commons.Entities;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;

namespace KulturPlatform.Domain.Commons.AggregateRoot
{
    public class Activity : AuditableEntity, IAggregateRoot
    {
        public Title TitleTr { get; private set; }
        public Title TitleDe { get; private set; }

        public Description DescriptionTr { get; private set; }
        public Description DescriptionDe { get; private set; }

        public LocalizedContent? DetailedContentTr { get; private set; }
        public LocalizedContent? DetailedContentDe { get; private set; }
        public ActivityDate Date { get; private set; }
        public Address Address { get; private set; }
        public Category Category { get; private set; }

        // Hybrid image storage: either URL or Database-stored image
        public Url? ImageUrl { get; private set; }
        public ImageData? ImageData { get; private set; }
        
        public MediaGallery GalleryImages { get; private set; }
        public Url? VideoUrl { get; private set; }

        public bool IsActive { get; private set; } = true;

        private Activity(Guid id) : base(id)
        {
        }

        public static Activity Create(
            Title titleTr,
            Title titleDe,
            Description descriptionTr,
            Description descriptionDe,
            ActivityDate date,
            Address address,
            Category category,
            Url? imageUrl = null,
            ImageData? imageData = null,
            MediaGallery? galleryImages = null,
            Url? videoUrl = null
        )
        {
            // Validate that only one image source is provided
            if (imageUrl != null && imageData != null)
                throw new ArgumentException("Cannot specify both ImageUrl and ImageData. Choose one image source.");

            return new Activity(Guid.NewGuid())
            {
                TitleTr = titleTr,
                TitleDe = titleDe,
                DescriptionTr = descriptionTr,
                DescriptionDe = descriptionDe,
                Date = date,
                Address = address,
                Category = category,
                ImageUrl = imageData == null ? imageUrl : null, // Only set if no ImageData
                ImageData = imageData,
                GalleryImages = galleryImages ?? MediaGallery.Empty(),
                VideoUrl = videoUrl,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };
        }

        public void Update(
            Title titleTr, Title titleDe,
            Description descriptionTr,
            Description descriptionDe,
            ActivityDate date,
            Address address,
            Category category,
            Url? imageUrl = null,
            ImageData? imageData = null,
            MediaGallery? galleryImages = null,
            Url? videoUrl = null,
            bool isActive = true,
            LocalizedContent? detailedContentTr = null,
            LocalizedContent? detailedContentDe = null
        )
        {
            // Validate that only one image source is provided
            if (imageUrl != null && imageData != null)
                throw new ArgumentException("Cannot specify both ImageUrl and ImageData. Choose one image source.");

            UpdateTitle(titleTr, titleDe);
            UpdateDescription(descriptionTr, descriptionDe);
            UpdateDate(date);
            UpdateLocation(address);
            UpdateCategory(category);
            UpdateImage(imageUrl, imageData);
            
            // ✅ Only update GalleryImages if provided (not null)
            // When null, it means gallery is being handled separately (e.g., in repository)
            if (galleryImages != null)
            {
                GalleryImages = galleryImages;
            }
            
            VideoUrl = videoUrl;
            DetailedContentTr = detailedContentTr;
            DetailedContentDe = detailedContentDe;
            if (isActive)
                Activate();
            else
                Deactivate();
            SetUpdatedAt();
        }

        public void UpdateTitle(Title titleTr, Title titleDe)
        {
            TitleTr = titleTr;
            TitleDe = titleDe;
            SetUpdatedAt();
        }

        public void UpdateDescription(Description descriptionTr, Description descriptionDe)
        {
            DescriptionTr = descriptionTr;
            DescriptionDe = descriptionDe;
            SetUpdatedAt();
        }

        public void UpdateLocation(Address address)
        {
            Address = address;
            SetUpdatedAt();
        }

        public void UpdateCategory(Category category)
        {
            Category = category;
            SetUpdatedAt();
        }

        public void UpdateDate(ActivityDate date)
        {
            Date = date;
            SetUpdatedAt();
        }

        public void UpdateImage(Url? imageUrl, ImageData? imageData)
        {
            // Validate that only one image source is provided
            if (imageUrl != null && imageData != null)
                throw new ArgumentException("Cannot specify both ImageUrl and ImageData. Choose one image source.");

            // Clear both fields first
            ImageUrl = null;
            ImageData = null;

            // Then set the appropriate one
            if (imageData != null)
            {
                ImageData = imageData;
            }
            else if (imageUrl != null)
            {
                ImageUrl = imageUrl;
            }

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
        /// Gets the image source for frontend display
        /// Returns ImageData's data URI if available, otherwise ImageUrl
        /// </summary>
        public string? GetImageSource()
        {
            if (ImageData != null)
                return ImageData.GetDataUri();
            
            return ImageUrl?.Value;
        }

        /// <summary>
        /// Checks if the activity has any image
        /// </summary>
        public bool HasImage() => ImageUrl != null || ImageData != null;
    }
}