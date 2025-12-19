namespace KulturPlatform.Domain.Commons.ValueObjects
{
    public record PageName
    {
        public string Value { get; init; }

        // Private parameterless constructor for EF Core
        private PageName() { Value = string.Empty; }

        public PageName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Page name cannot be empty.");

            if (value.Length > 100)
                throw new ArgumentException("Page name cannot exceed 100 characters.");

            Value = value.Trim();
        }

        public override string ToString() => Value;
    }

}
