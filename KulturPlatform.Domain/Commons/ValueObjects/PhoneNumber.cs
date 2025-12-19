namespace KulturPlatform.Domain.Commons.ValueObjects
{
    public record PhoneNumber
    {
        public string Value { get; init; }

        // Private parameterless constructor for EF Core
        private PhoneNumber() { Value = string.Empty; }

        public PhoneNumber(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Phone number cannot be empty.", nameof(value));
            if (value.Length < 7)
                throw new ArgumentException("Phone number must be at least 7 characters long.", nameof(value));
            if (value.Length > 20)
                throw new ArgumentException("Phone number cannot exceed 15 characters.", nameof(value));
            Value = value.Trim();
        }
        public override string ToString() => Value;
    }
}
