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

        public Url? ImageUrl { get; private set; }
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
            MediaGallery? galleryImages = null,
            Url? videoUrl = null
        )
        {
            return new Activity(Guid.NewGuid())
            {
                TitleTr = titleTr,
                TitleDe = titleDe,
                DescriptionTr = descriptionTr,
                DescriptionDe = descriptionDe,
                Date = date,
                Address = address,
                Category = category,
                ImageUrl = imageUrl,
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
            MediaGallery? galleryImages = null,
            Url? videoUrl = null,
            bool isActive = true,
            LocalizedContent? detailedContentTr = null,
            LocalizedContent? detailedContentDe = null
        )
        {
            UpdateTitle(titleTr, titleDe);
            UpdateDescription(descriptionTr, descriptionDe);
            UpdateDate(date);
            UpdateLocation(address);
            UpdateCategory(category);
            ImageUrl = imageUrl;
            GalleryImages = galleryImages ?? MediaGallery.Empty();
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