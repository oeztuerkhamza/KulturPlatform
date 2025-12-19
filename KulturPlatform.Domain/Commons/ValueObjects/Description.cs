namespace KulturPlatform.Domain.Commons.ValueObjects
{
    public record Description
    {
        public string Value { get; init; }

        // Private parameterless constructor for EF Core
        private Description() { Value = string.Empty; }

        public Description(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Description cannot be empty.", nameof(value));

            Value = value.Trim();
        }

        public override string ToString() => Value;
    }
}
