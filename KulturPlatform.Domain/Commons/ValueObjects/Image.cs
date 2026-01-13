namespace KulturPlatform.Domain.Commons.ValueObjects
{
    public sealed record Image
    {
        public string Value { get; init; }

        // EF Core için
        private Image()
        {
            Value = string.Empty;
        }

        private Image(string value)
        {
            Value = value;
        }

        public static Image Create(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentException("Image cannot be empty.");

            if (!Uri.TryCreate(url, UriKind.Absolute, out _))
                throw new ArgumentException("Invalid Image format.");

            return new Image(url.Trim());
        }

        public override string ToString() => Value;
    }
}
