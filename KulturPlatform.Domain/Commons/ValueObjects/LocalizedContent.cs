namespace KulturPlatform.Domain.Commons.ValueObjects
{
    public record LocalizedContent
    {
        public string Value { get; init; }

        // Private parameterless constructor for EF Core
        private LocalizedContent() { Value = string.Empty; }

        public LocalizedContent(string value)
        {
            Value = value?.Trim() ?? string.Empty;
        }

        public override string ToString() => Value;
    }

}
