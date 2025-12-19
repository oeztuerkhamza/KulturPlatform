namespace KulturPlatform.Domain.Commons.ValueObjects
{
    public record SubmissionMessage
    {
        public string Value { get; init; }

        // Private parameterless constructor for EF Core
        private SubmissionMessage() { Value = string.Empty; }

        public SubmissionMessage(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Message cannot be empty.", nameof(value));
            if (value.Length < 10)
                throw new ArgumentException("Message must be at least 10 characters long.", nameof(value));
            if (value.Length > 2000)
                throw new ArgumentException("Message cannot exceed 2000 characters.", nameof(value));

            Value = value.Trim();
        }

        public override string ToString() => Value;
    }
}
