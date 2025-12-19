using System.Text.Json.Serialization;

namespace KulturPlatform.Domain.Commons.ValueObjects
{
    public record CourseDetails
    {
        public string Value { get; init; }

        // Private parameterless constructor for EF Core
        private CourseDetails() { Value = string.Empty; }
        [JsonConstructor]
        public CourseDetails(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Course details cannot be null or empty.", nameof(value));
            }
            if (value.Length > 1000)
            {
                throw new ArgumentException("Course details cannot be longer than 1000 characters.", nameof(value));
            }
            if (value.Length < 20)
            {
                throw new ArgumentException("Course details cannot be shorter than 20 characters.", nameof(value));
            }

            Value = value.Trim();
        }
    }
}
