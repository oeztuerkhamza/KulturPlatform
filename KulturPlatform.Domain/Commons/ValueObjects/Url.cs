namespace KulturPlatform.Domain.Commons.ValueObjects
{
    public sealed record Url
    {
        public string Value { get; init; }

        // EF Core için
        private Url()
        {
            Value = string.Empty;
        }

        private Url(string value)
        {
            Value = value;
        }

        public static Url? Create(string? url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return null;

            // Reject base64 data URIs - these should use ImageData instead
            if (url.StartsWith("data:", StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException("Base64 data URIs are not allowed for URL field. Use ImageData instead.");

            if (!Uri.TryCreate(url, UriKind.Absolute, out _))
                throw new ArgumentException("Invalid URL format.");

            return new Url(url.Trim());
        }

        public override string ToString() => Value;
    }
}