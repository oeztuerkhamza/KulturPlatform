using KulturPlatform.Domain.Commons.Entities;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;

namespace KulturPlatform.Domain.Commons.Aggregates
{
    public class TeaEvent : AuditableEntity, IAggregateRoot
    {
        public Title TitleTurkish { get; private set; }
        public Title TitleGerman { get; private set; }

        public TeaEventContent Content { get; private set; }

        public string Date { get; private set; }
        public string Time { get; private set; }
        public Location Location { get; private set; }
        public Url ImageUrl { get; private set; }

        public bool IsActive { get; private set; } = true;

        private TeaEvent(Guid id) : base(id)
        {
        }

        private TeaEvent(
            Guid id,
            Title titleTr,
            Title titleDe,
            TeaEventContent content,
            string date,
            string time,
            Location location,
            Url imageUrl)
            : base(id)
        {
            TitleTurkish = titleTr;
            TitleGerman = titleDe;
            Content = content;
            Date = date;
            Time = time;
            Location = location;
            ImageUrl = imageUrl;
            CreatedAt = DateTime.UtcNow;
        }

        public static TeaEvent CreateNew(
            Title titleTr,
            Title titleDe,
            TeaEventContent content,
            string date,
            string time,
            Location location,
            Url imageUrl)
        {
            return new TeaEvent(
                Guid.NewGuid(),
                titleTr,
                titleDe,
                content,
                date,
                time,
                location,
                imageUrl);
        }

        public void UpdateContent(TeaEventContent content)
        {
            Content = content;
            SetUpdatedAt();
        }

        public void UpdateTitle(Title titleTurkish, Title titleGerman)
        {
            TitleTurkish = titleTurkish;
            TitleGerman = titleGerman;
            SetUpdatedAt();
        }

        public void Reschedule(string date, string time)
        {
            Date = date;
            Time = time;
            SetUpdatedAt();
        }

        public void Deactivate()
        {
            IsActive = false;
            SetUpdatedAt();
        }

        public void Activate()
        {
            IsActive = true;
            SetUpdatedAt();
        }
    }
}