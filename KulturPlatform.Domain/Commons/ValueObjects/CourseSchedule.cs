namespace KulturPlatform.Domain.Commons.ValueObjects
{
    public record CourseSchedule
    {
        public string Value { get; init; }

        // Private parameterless constructor for EF Core
        private CourseSchedule() { Value = string.Empty; }

        public CourseSchedule(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Course schedule cannot be null or empty.", nameof(value));
            }
            if (value.Length > 500)
            {
                throw new ArgumentException("Course schedule cannot be longer than 500 characters.", nameof(value));
            }
            if (value.Length < 10)
            {
                throw new ArgumentException("Course schedule cannot be shorter than 10 characters.", nameof(value));
            }
            Value = value.Trim();
        }
    }
}
