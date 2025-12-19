namespace KulturPlatform.Domain.Commons.ValueObjects
{
    public record SectionKey
    {
        public string Value { get; init; }

        // Private parameterless constructor for EF Core
        private SectionKey() { Value = string.Empty; }

        public SectionKey(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("SectionKey cannot be empty.");

            if (value.Length > 100)
                throw new ArgumentException("SectionKey cannot exceed 100 characters.");

            Value = value.Trim();
        }

        public override string ToString() => Value;
    }

}
