using KulturPlatform.Domain.Commons.Entities;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;

namespace KulturPlatform.Domain.Commons.Aggregates
{
    public class Satzung : AuditableEntity, IAggregateRoot
    {
        // Header
        public Title TitleTurkish { get; private set; }
        public Title TitleGerman { get; private set; }

        // Name & Seat
        public SectionContent NameAndSeatTurkish { get; private set; }
        public SectionContent NameAndSeatGerman { get; private set; }
        public SectionContent NameDescTurkish { get; private set; }
        public SectionContent NameDescGerman { get; private set; }
        public SectionContent SeatTurkish { get; private set; }
        public SectionContent SeatGerman { get; private set; }
        public SectionContent SeatDescTurkish { get; private set; }
        public SectionContent SeatDescGerman { get; private set; }
        public SectionContent FiscalYearTurkish { get; private set; }
        public SectionContent FiscalYearGerman { get; private set; }
        public SectionContent FiscalYearDescTurkish { get; private set; }
        public SectionContent FiscalYearDescGerman { get; private set; }

        // Purpose
        public SectionContent PurposeOfAssociationTurkish { get; private set; }
        public SectionContent PurposeOfAssociationGerman { get; private set; }
        public List<Purpose> Purposes { get; private set; } = new();

        // Gemeinnützigkeit
        public SectionContent GemeinnuetzigkeitTurkish { get; private set; }
        public SectionContent GemeinnuetzigkeitGerman { get; private set; }

        // Politische Neutralität
        public SectionContent PoliticalNeutralityTurkish { get; private set; }
        public SectionContent PoliticalNeutralityGerman { get; private set; }

        // Mitgliedschaft
        public List<MembershipDetail> Memberships { get; private set; } = new();

        private Satzung(Guid id) : base(id) { }

        public static Satzung CreateNew(
            Title titleTurkish,
            Title titleGerman,
            SectionContent nameAndSeatTurkish,
            SectionContent nameAndSeatGerman,
            SectionContent nameDescTurkish,
            SectionContent nameDescGerman,
            SectionContent seatTurkish,
            SectionContent seatGerman,
            SectionContent seatDescTurkish,
            SectionContent seatDescGerman,
            SectionContent fiscalYearTurkish,
            SectionContent fiscalYearGerman,
            SectionContent fiscalYearDescTurkish,
            SectionContent fiscalYearDescGerman,
            SectionContent purposeOfAssociationTurkish,
            SectionContent purposeOfAssociationGerman,
            List<Purpose> purposes,
            SectionContent gemeinnuetzigkeitTurkish,
            SectionContent gemeinnuetzigkeitGerman,
            SectionContent politicalNeutralityTurkish,
            SectionContent politicalNeutralityGerman,
            List<MembershipDetail> memberships
        )
        {
            return new Satzung(Guid.NewGuid())
            {
                TitleTurkish = titleTurkish,
                TitleGerman = titleGerman,
                NameAndSeatTurkish = nameAndSeatTurkish,
                NameAndSeatGerman = nameAndSeatGerman,
                NameDescTurkish = nameDescTurkish,
                NameDescGerman = nameDescGerman,
                SeatTurkish = seatTurkish,
                SeatGerman = seatGerman,
                SeatDescTurkish = seatDescTurkish,
                SeatDescGerman = seatDescGerman,
                FiscalYearTurkish = fiscalYearTurkish,
                FiscalYearGerman = fiscalYearGerman,
                FiscalYearDescTurkish = fiscalYearDescTurkish,
                FiscalYearDescGerman = fiscalYearDescGerman,
                PurposeOfAssociationTurkish = purposeOfAssociationTurkish,
                PurposeOfAssociationGerman = purposeOfAssociationGerman,
                Purposes = purposes ?? new List<Purpose>(),
                GemeinnuetzigkeitTurkish = gemeinnuetzigkeitTurkish,
                GemeinnuetzigkeitGerman = gemeinnuetzigkeitGerman,
                PoliticalNeutralityTurkish = politicalNeutralityTurkish,
                PoliticalNeutralityGerman = politicalNeutralityGerman,
                Memberships = memberships ?? new List<MembershipDetail>(),
                CreatedAt = DateTime.UtcNow
            };
        }

        public void Update(
            Title titleTurkish,
            Title titleGerman,
            SectionContent nameAndSeatTurkish,
            SectionContent nameAndSeatGerman,
            SectionContent nameDescTurkish,
            SectionContent nameDescGerman,
            SectionContent seatTurkish,
            SectionContent seatGerman,
            SectionContent seatDescTurkish,
            SectionContent seatDescGerman,
            SectionContent fiscalYearTurkish,
            SectionContent fiscalYearGerman,
            SectionContent fiscalYearDescTurkish,
            SectionContent fiscalYearDescGerman,
            SectionContent purposeOfAssociationTurkish,
            SectionContent purposeOfAssociationGerman,
            List<Purpose> purposes,
            SectionContent gemeinnuetzigkeitTurkish,
            SectionContent gemeinnuetzigkeitGerman,
            SectionContent politicalNeutralityTurkish,
            SectionContent politicalNeutralityGerman,
            List<MembershipDetail> memberships
        )
        {
            TitleTurkish = titleTurkish;
            TitleGerman = titleGerman;
            NameAndSeatTurkish = nameAndSeatTurkish;
            NameAndSeatGerman = nameAndSeatGerman;
            NameDescTurkish = nameDescTurkish;
            NameDescGerman = nameDescGerman;
            SeatTurkish = seatTurkish;
            SeatGerman = seatGerman;
            SeatDescTurkish = seatDescTurkish;
            SeatDescGerman = seatDescGerman;
            FiscalYearTurkish = fiscalYearTurkish;
            FiscalYearGerman = fiscalYearGerman;
            FiscalYearDescTurkish = fiscalYearDescTurkish;
            FiscalYearDescGerman = fiscalYearDescGerman;
            PurposeOfAssociationTurkish = purposeOfAssociationTurkish;
            PurposeOfAssociationGerman = purposeOfAssociationGerman;
            Purposes = purposes ?? new List<Purpose>();
            GemeinnuetzigkeitTurkish = gemeinnuetzigkeitTurkish;
            GemeinnuetzigkeitGerman = gemeinnuetzigkeitGerman;
            PoliticalNeutralityTurkish = politicalNeutralityTurkish;
            PoliticalNeutralityGerman = politicalNeutralityGerman;
            Memberships = memberships ?? new List<MembershipDetail>();
            SetUpdatedAt();
        }
    }
}
