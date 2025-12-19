using KulturPlatform.Domain.Commons.Entities;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;

namespace KulturPlatform.Domain.Commons.Aggregates
{
    public class ValueItem : AuditableEntity, IAggregateRoot
    {
        public Title TitleTr { get; private set; }
        public Title TitleDe { get; private set; }
        public Title SubtitleTr { get; private set; }
        public Title SubtitleDe { get; private set; }

        public Description DescriptionTr { get; private set; }
        public Description DescriptionDe { get; private set; }

        public DisplayOrder DisplayOrder { get; private set; }

        private readonly List<Section> _sections = new();
        public IReadOnlyCollection<Section> Sections => _sections.AsReadOnly();

        public bool IsActive { get; private set; } = true;

        private ValueItem(Guid id) : base(id) { }

        private ValueItem(
            Guid id,
            Title titleTr,
            Title titleDe,
            Title subtitleTr,
            Title subtitleDe,
            Description descriptionTr,
            Description descriptionDe,
            DisplayOrder displayOrder)
            : base(id)
        {
            TitleTr = titleTr;
            TitleDe = titleDe;
            SubtitleTr = subtitleTr;
            SubtitleDe = subtitleDe;
            DescriptionTr = descriptionTr;
            DescriptionDe = descriptionDe;
            DisplayOrder = displayOrder;
        }

        public static ValueItem CreateNew(
            Title titleTr,
            Title titleDe,
            Title subTitleTr,
            Title subTitleDe,
            Description descriptionTr,
            Description descriptionDe,
            DisplayOrder displayOrder)
        {
            return new ValueItem(
                Guid.NewGuid(),
                titleTr,
                titleDe,
                subTitleTr,
                subTitleDe,
                descriptionTr,
                descriptionDe,
                displayOrder);
        }

        /* =======================
           BEHAVIORS (IMPORTANT)
           ======================= */

        public void UpdateContent(
            Title titleTr,
            Title titleDe,
            Title subTitleTr,
            Title subTitleDe,
            Description descriptionTr,
            Description descriptionDe)
        {
            TitleTr = titleTr;
            TitleDe = titleDe;
            SubtitleTr = subTitleTr;
            SubtitleDe = subTitleDe;
            DescriptionTr = descriptionTr;
            DescriptionDe = descriptionDe;
            SetUpdatedAt();
        }

        public void UpdateDisplayOrder(DisplayOrder displayOrder)
        {
            DisplayOrder = displayOrder;
            SetUpdatedAt();
        }

        public void UpdateSections(IEnumerable<Section> sections)
        {
            _sections.Clear();
            _sections.AddRange(sections);
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
