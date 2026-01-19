namespace KulturPlatform.Domain.Commons.ValueObjects
{
    public record Anrede
    {
        public string Value { get; init; }

        private Anrede() { Value = string.Empty; }

        public Anrede(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Anrede cannot be empty.", nameof(value));

            var validValues = new[] { "Herr", "Frau", "Divers", "Bay", "Bayan", "Diğer" };
            if (!validValues.Contains(value, StringComparer.OrdinalIgnoreCase))
                throw new ArgumentException($"Invalid Anrede. Must be one of: {string.Join(", ", validValues)}", nameof(value));

            Value = value;
        }

        public override string ToString() => Value;
    }
}
