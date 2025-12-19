namespace KulturPlatform.Domain.Commons.ValueObjects
{
    public record MembershipDetail
    {
        public SectionContent Type { get; init; }
        public SectionContent DescriptionTurkish { get; init; }
        public SectionContent DescriptionGerman { get; init; }

        public MembershipDetail() { }

        private MembershipDetail(SectionContent type, SectionContent descriptionTurkish, SectionContent descriptionGerman)
        {
            Type = type;
            DescriptionTurkish = descriptionTurkish;
            DescriptionGerman = descriptionGerman;
        }

        public static MembershipDetail Create(SectionContent type, SectionContent descriptionTurkish, SectionContent descriptionGerman)
            => new MembershipDetail(type, descriptionTurkish, descriptionGerman);
    }

}
