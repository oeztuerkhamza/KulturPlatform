namespace KulturPlatform.Domain.Commons.ValueObjects
{
    public record Purpose
    {
        public string Letter { get; init; }
        public SectionContent Content { get; init; }

        public Purpose() { }

        private Purpose(string letter, SectionContent content)
        {
            Letter = letter;
            Content = content;
        }

        public static Purpose Create(string letter, SectionContent content)
        {
            return new Purpose(letter, content);
        }
    }

}
