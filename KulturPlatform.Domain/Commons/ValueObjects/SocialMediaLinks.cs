namespace KulturPlatform.Domain.Commons.ValueObjects
{
    public record SocialMediaLinks
    {
        public string Facebook { get; private set; }
        public string Instagram { get; private set; }
        public string Twitter { get; private set; }

        private SocialMediaLinks() { }

        public SocialMediaLinks(string facebook, string instagram, string twitter)
        {
            Facebook = facebook;
            Instagram = instagram;
            Twitter = twitter;
        }

        public static SocialMediaLinks Empty => new(null, null, null);
    }



}
