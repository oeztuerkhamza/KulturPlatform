using System.Text.Json.Serialization;

namespace KulturPlatform.Domain.Commons.ValueObjects
{
    public record Category
    {
        public string Value { get; init; }

        // Private parameterless constructor for EF Core
        private Category() { Value = string.Empty; }
        [JsonConstructor]
        public Category(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Category cannot be empty.", nameof(value));

            Value = value.Trim();

            var allowedCategories = new[] { "Music", "Art", "Education", "Social" };
            if (!allowedCategories.Contains(value, StringComparer.OrdinalIgnoreCase))
                throw new ArgumentException($"Category '{value}' is not allowed.", nameof(value));

            Value = value;
        }
        public override string ToString() => Value;
    }
}
