using KulturPlatform.Domain.Commons.ValueObjects;

namespace KulturPlatform.Application.Dtos.SatzungDto
{
    public class SatzungDto
    {
        public Guid Id { get; set; }
        public string Key { get; set; }
        public string TitleTurkish { get; set; }
        public string TitleGerman { get; set; }

        public SectionContent NameAndSeatTurkish { get; set; }
        public SectionContent NameAndSeatGerman { get; set; }
        public SectionContent NameDescTurkish { get; set; }
        public SectionContent NameDescGerman { get; set; }
        public SectionContent SeatTurkish { get; set; }
        public SectionContent SeatGerman { get; set; }
        public SectionContent SeatDescTurkish { get; set; }
        public SectionContent SeatDescGerman { get; set; }
        public SectionContent FiscalYearTurkish { get; set; }
        public SectionContent FiscalYearGerman { get; set; }
        public SectionContent FiscalYearDescTurkish { get; set; }
        public SectionContent FiscalYearDescGerman { get; set; }

        public SectionContent PurposeOfAssociationTurkish { get; set; }
        public SectionContent PurposeOfAssociationGerman { get; set; }
        public List<Purpose> Purposes { get; set; }

        public SectionContent GemeinnuetzigkeitTurkish { get; set; }
        public SectionContent GemeinnuetzigkeitGerman { get; set; }

        public SectionContent PoliticalNeutralityTurkish { get; set; }
        public SectionContent PoliticalNeutralityGerman { get; set; }

        public List<MembershipDetail> Memberships { get; set; }

        public DateTime UpdatedAt { get; set; }
    }


}
