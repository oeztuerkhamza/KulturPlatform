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
        }
        public override string ToString() => Value;
    }
}
