namespace KulturPlatform.Domain.Commons.ValueObjects
{
    public record Name
    {
        public string Value { get; init; }

        // Private parameterless constructor for EF Core
        private Name() { Value = string.Empty; }

        public Name(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Name cannot be empty", nameof(value));
            if (value.Length > 50)
                throw new ArgumentException("Name cannot exceed 50 characters", nameof(value));
            if (value.Length < 3)
                throw new ArgumentException("Name must be at least 3 characters long", nameof(value));

            Value = value.Trim();
        }
        public override string ToString() => Value;
    }
}
