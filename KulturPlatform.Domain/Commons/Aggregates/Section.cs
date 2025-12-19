using KulturPlatform.Domain.Commons.Entities;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;

namespace KulturPlatform.Domain.Commons.Aggregates
{
    public class Section : AuditableEntity, IAggregateRoot
    {
        public Title HeadingTr { get; private set; }
        public Title HeadingDe { get; private set; }

        public Description BodyTr { get; private set; }
        public Description BodyDe { get; private set; }
        private readonly List<SectionItem> _items = new();
        public IReadOnlyCollection<SectionItem> Items => _items.AsReadOnly();

        private Section() { }

        public Section(Title headingTr, Title headingDe, Description bodyTr, Description bodyDe, IEnumerable<SectionItem> items)
        {
            HeadingTr = headingTr;
            HeadingDe = headingDe;
            BodyTr = bodyTr;
            BodyDe = bodyDe;
            _items.AddRange(items ?? new List<SectionItem>());
        }
    }
}
