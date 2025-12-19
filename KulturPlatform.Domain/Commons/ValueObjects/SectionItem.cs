namespace KulturPlatform.Domain.Commons.ValueObjects
{
    public record SectionItem
    {
        public Title TitleTr { get; private set; }
        public Title TitleDe { get; private set; }

        public string Icon { get; private set; }

        public SectionItem() { }

        public SectionItem(Title titleTr, Title titleDe, string icon)
        {
            TitleTr = titleTr;
            TitleDe = titleDe;
            Icon = icon;
        }
    }
}
