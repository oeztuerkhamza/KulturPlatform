using System.Text.Json.Serialization;

namespace KulturPlatform.Domain.Commons.ValueObjects
{
    public record Title
    {
        public string Value { get; init; }

        private Title() { Value = string.Empty; }

        [JsonConstructor]
        public Title(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Title cannot be empty.", nameof(value));

            if (value.Length > 200)
                throw new ArgumentException("Title cannot exceed 200 characters.", nameof(value));

            Value = value.Trim();
        }

        public static Title Create(string value)
            => new(value);

        public override string ToString() => Value;
    }
}