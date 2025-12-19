namespace KulturPlatform.Domain.Commons.ValueObjects
{
    public record PartnerName
    {
        public string Value { get; init; }

        // Private parameterless constructor for EF Core
        private PartnerName() { Value = string.Empty; }

        public PartnerName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Partner name cannot be empty.");

            if (value.Length > 200)
                throw new ArgumentException("Partner name cannot exceed 200 characters.");

            Value = value.Trim();
        }

        public override string ToString() => Value;
    }

}
