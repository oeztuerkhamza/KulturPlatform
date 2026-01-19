namespace KulturPlatform.Domain.Commons.ValueObjects
{
    public record Subject
    {
        public string Value { get; init; }

        private Subject() { Value = string.Empty; }

        public Subject(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Subject cannot be empty.", nameof(value));
            if (value.Length < 3)
                throw new ArgumentException("Subject must be at least 3 characters long.", nameof(value));
            if (value.Length > 200)
                throw new ArgumentException("Subject cannot exceed 200 characters.", nameof(value));

            Value = value.Trim();
        }

        public override string ToString() => Value;
    }
}
