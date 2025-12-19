namespace KulturPlatform.Domain.Commons.ValueObjects
{
    public class selectedName
    {
        public Guid Id { get; private set; }
        public string Key { get; private set; }
        public SectionContent Content { get; private set; }

        private selectedName() { }

        private selectedName(string key, SectionContent content)
        {
            Id = Guid.NewGuid();
            Key = key;
            Content = content;
        }

        public static selectedName Create(string key, SectionContent content) => new selectedName(key, content);

        public void Update(SectionContent content)
        {
            Content = content;
        }
    }
}
