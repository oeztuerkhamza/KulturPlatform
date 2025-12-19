namespace KulturPlatform.Domain.Commons.ValueObjects
{
    public record InstructorName
    {
        public string Value { get; init; }

        // Private parameterless constructor for EF Core
        private InstructorName() { Value = string.Empty; }

        public InstructorName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Instructor name cannot be null or empty.", nameof(value));
            }
            if (value.Length > 100)
            {
                throw new ArgumentException("Instructor name cannot be longer than 100 characters.", nameof(value));
            }
            if (value.Length < 2)
            {
                throw new ArgumentException("Instructor name cannot be shorter than 2 characters.", nameof(value));
            }
            Value = value.Trim();
        }
    }
}
