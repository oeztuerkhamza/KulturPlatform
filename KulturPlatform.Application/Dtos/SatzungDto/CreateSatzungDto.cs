namespace KulturPlatform.Application.Dtos.SatzungDto
{
    public class CreateSatzungDto
    {
        public string Key { get; set; }

        public TitleDto TitleTurkish { get; set; }
        public TitleDto TitleGerman { get; set; }

        public SectionContentDto NameAndSeatTurkish { get; set; }
        public SectionContentDto NameAndSeatGerman { get; set; }
        public SectionContentDto NameDescTurkish { get; set; }
        public SectionContentDto NameDescGerman { get; set; }
        public SectionContentDto SeatTurkish { get; set; }
        public SectionContentDto SeatGerman { get; set; }
        public SectionContentDto SeatDescTurkish { get; set; }
        public SectionContentDto SeatDescGerman { get; set; }

        public SectionContentDto FiscalYearTurkish { get; set; }
        public SectionContentDto FiscalYearGerman { get; set; }
        public SectionContentDto FiscalYearDescTurkish { get; set; }
        public SectionContentDto FiscalYearDescGerman { get; set; }

        public SectionContentDto PurposeOfAssociationTurkish { get; set; }
        public SectionContentDto PurposeOfAssociationGerman { get; set; }

        public List<PurposeDto> Purposes { get; set; } = new();

        public SectionContentDto GemeinnuetzigkeitTurkish { get; set; }
        public SectionContentDto GemeinnuetzigkeitGerman { get; set; }

        public SectionContentDto PoliticalNeutralityTurkish { get; set; }
        public SectionContentDto PoliticalNeutralityGerman { get; set; }

        public List<MembershipDto> Memberships { get; set; } = new();
    }
}