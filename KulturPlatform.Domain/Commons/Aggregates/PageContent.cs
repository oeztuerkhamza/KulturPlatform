using KulturPlatform.Domain.Commons.Entities;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;

namespace KulturPlatform.Domain.Commons.Aggregates
{
    public class PageContent : AuditableEntity, IAggregateRoot
    {
        public PageName PageName { get; private set; }
        public SectionKey SectionKey { get; private set; }

        public LocalizedContent ContentTr { get; private set; }
        public LocalizedContent ContentDe { get; private set; }

        public bool IsActive { get; private set; } = true;

        private PageContent(Guid id) : base(id) { }

        private PageContent(
            PageName pageName,
            SectionKey sectionKey,
            LocalizedContent contentTr,
            LocalizedContent contentDe
        ) : base(Guid.NewGuid())
        {
            PageName = pageName;
            SectionKey = sectionKey;
            ContentTr = contentTr;
            ContentDe = contentDe;
            CreatedAt = DateTime.UtcNow;
            IsActive = true;
        }

        public static PageContent Create(
            PageName pageName,
            SectionKey sectionKey,
            LocalizedContent contentTr,
            LocalizedContent contentDe
        )
        {
            return new PageContent(pageName, sectionKey, contentTr, contentDe);
        }

        // ----------- Domain Methods --------------

        public void UpdateContent(LocalizedContent contentTr, LocalizedContent contentDe)
        {
            ContentTr = contentTr;
            ContentDe = contentDe;
            SetUpdatedAt();
        }

        public void UpdateSectionKey(SectionKey sectionKey)
        {
            SectionKey = sectionKey;
            SetUpdatedAt();
        }

        public void UpdatePageName(PageName pageName)
        {
            PageName = pageName;
            SetUpdatedAt();
        }

        public void Activate()
        {
            IsActive = true;
            SetUpdatedAt();
        }

        public void Deactivate()
        {
            IsActive = false;
            SetUpdatedAt();
        }
    }
}
