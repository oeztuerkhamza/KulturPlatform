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

        public static Url Create(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentException("URL cannot be empty.");

            if (!Uri.TryCreate(url, UriKind.Absolute, out _))
                throw new ArgumentException("Invalid URL format.");

            return new Url(url.Trim());
        }

        public override string ToString() => Value;
    }
}