using KulturPlatform.Domain.Commons.Entities;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;

namespace KulturPlatform.Domain.Commons.Aggregates
{
    public class Course : AuditableEntity, IAggregateRoot
    {
        public Title TitleTr { get; private set; }
        public Title TitleDe { get; private set; }

        public Description DescriptionTr { get; private set; }
        public Description DescriptionDe { get; private set; }

        public CourseDetails? DetailsTr { get; private set; }
        public CourseDetails? DetailsDe { get; private set; }

        public CourseSchedule? ScheduleTr { get; private set; }
        public CourseSchedule? ScheduleDe { get; private set; }
        public string? Icon { get; private set; }
        public InstructorName? Instructor { get; private set; }

        public DateTime? Date { get; private set; }
        public Address? CourseLocation { get; private set; }
        public Category? CourseCategory { get; private set; }

        public bool IsActive { get; private set; } = true;

        private Course(Guid id) : base(id)
        {
        }

        public static Course Create(
            Title titleTr,
            Title titleDe,
            Description descriptionTr,
            Description descriptionDe,
            CourseDetails? detailsTr = null,
            CourseDetails? detailsDe = null,
            CourseSchedule? scheduleTr = null,
            CourseSchedule? scheduleDe = null,
            string? icon = null,
            InstructorName? instructor = null,
            DateTime? date = null,
            Address? location = null,
            Category? category = null
        )
        {
            return new Course(Guid.NewGuid())
            {
                TitleTr = titleTr,
                TitleDe = titleDe,
                DescriptionTr = descriptionTr,
                DescriptionDe = descriptionDe,
                DetailsTr = detailsTr,
                DetailsDe = detailsDe,
                ScheduleTr = scheduleTr,
                ScheduleDe = scheduleDe,
                Icon = icon,
                Instructor = instructor,
                Date = date,
                CourseLocation = location,
                CourseCategory = category,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };
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

        public void UpdateDetails(CourseDetails? detailsTr, CourseDetails? detailsDe)
        {
            DetailsTr = detailsTr;
            DetailsDe = detailsDe;
            SetUpdatedAt();
        }

        public void UpdateSchedule(CourseSchedule? scheduleTr, CourseSchedule? scheduleDe)
        {
            ScheduleTr = scheduleTr;
            ScheduleDe = scheduleDe;
            SetUpdatedAt();
        }

        public void UpdateInstructor(InstructorName? instructor)
        {
            Instructor = instructor;
            SetUpdatedAt();
        }

        public void UpdateIcon(string? icon)
        {
            Icon = icon?.Trim();
            SetUpdatedAt();
        }

        public void UpdateDate(DateTime? date)
        {
            Date = date;
            SetUpdatedAt();
        }

        public void UpdateLocation(Address? location)
        {
            CourseLocation = location;
            SetUpdatedAt();
        }

        public void UpdateCategory(Category? category)
        {
            CourseCategory = category;
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
