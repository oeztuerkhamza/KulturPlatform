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

        public string? DetailedContentTr { get; private set; }
        public string? DetailedContentDe { get; private set; }

        public ActivityDate DateTr { get; private set; }
        public ActivityDate DateDe { get; private set; }
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
            ActivityDate dateTr,
            ActivityDate dateDe,
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
                DateTr = dateTr,
                DateDe = dateDe,
                Address = address,
                Category = category,
                ImageUrl = imageUrl,
                GalleryImages = galleryImages ?? new MediaGallery(new List<string>()),
                VideoUrl = videoUrl,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };
        }

        public void Update(
            Title titleTr, Title titleDe,
            Description descriptionTr,
            Description descriptionDe,
            ActivityDate dateTr,
            ActivityDate dateDe,
            Address address,
            Category category,
            Url? imageUrl = null,
            MediaGallery? galleryImages = null,
            Url? videoUrl = null,
            bool isActive = true,
            string? detailedContentTr = null,
            string? detailedContentDe = null
        )
        {
            UpdateTitle(titleTr, titleDe);
            UpdateDescription(descriptionTr, descriptionDe);
            UpdateDate(dateTr, dateDe);
            UpdateLocation(address);
            UpdateCategory(category);
            ImageUrl = imageUrl;
            GalleryImages = galleryImages ?? new MediaGallery(new List<string>());
            VideoUrl = videoUrl;
            DetailedContentTr = detailedContentTr;
            DetailedContentDe = detailedContentDe;
            if (isActive)
                Activate();
            else
                Deactivate();
            SetUpdatedAt();
        }
        // Domain methods
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

        public void UpdateDate(ActivityDate dateTr, ActivityDate dateDe)
        {
            DateTr = dateTr;
            DateDe = dateDe;
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