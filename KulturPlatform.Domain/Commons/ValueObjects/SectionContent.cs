using System.Text.Json.Serialization;

namespace KulturPlatform.Domain.Commons.ValueObjects
{
    public record SectionContent
    {
        public string Heading { get; init; }
        public string BodyTurkish { get; init; }
        public string BodyGerman { get; init; }

        // EF Core
        private SectionContent() { }
        [JsonConstructor]
        public SectionContent(string heading, string bodyTurkish, string bodyGerman)
        {
            Heading = heading?.Trim() ?? string.Empty;
            BodyTurkish = bodyTurkish?.Trim() ?? string.Empty;
            BodyGerman = bodyGerman?.Trim() ?? string.Empty;
        }

        public static SectionContent Create(
            string heading,
            string bodyTurkish,
            string bodyGerman)
        {
            if (string.IsNullOrWhiteSpace(heading))
                throw new ArgumentException("Heading cannot be empty.", nameof(heading));

            return new SectionContent(heading, bodyTurkish, bodyGerman);
        }
    }
}