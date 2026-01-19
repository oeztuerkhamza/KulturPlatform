namespace KulturPlatform.Domain.Commons.ValueObjects
{
    public record MessageText
    {
        public string Value { get; init; }

        private MessageText() { Value = string.Empty; }

        public MessageText(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Message cannot be empty.", nameof(value));
            if (value.Length < 10)
                throw new ArgumentException("Message must be at least 10 characters long.", nameof(value));
            if (value.Length > 5000)
                throw new ArgumentException("Message cannot exceed 5000 characters.", nameof(value));

            Value = value.Trim();
        }

        public override string ToString() => Value;
    }
}
