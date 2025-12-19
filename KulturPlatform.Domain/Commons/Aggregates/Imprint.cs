using KulturPlatform.Domain.Commons.Entities;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;

namespace KulturPlatform.Domain.Commons.Aggregates
{
    /// <summary>
    /// Künye (Imprint/Impressum) - Legal information about the organization
    /// </summary>
    public class Imprint : AuditableEntity, IAggregateRoot
    {
        // Organization Info
        public Title OrganizationName { get; private set; }
        public string OrganizationType { get; private set; } // e.g., "Kay?tl? Dernek (e.V.)"

        // Street
        public Address Address { get; private set; }

        // Contact
        public Email Email { get; private set; }
        public PhoneNumber Phone { get; private set; }

        // Responsible Persons
        public Name President { get; private set; }
        public Name VicePresident { get; private set; }

        // Legal Structure
        public string LegalStructureTurkish { get; private set; }
        public string LegalStructureGerman { get; private set; }
        public string PurposeTurkish { get; private set; }
        public string PurposeGerman { get; private set; }
        public string TaxExemptionTurkish { get; private set; }
        public string TaxExemptionGerman { get; private set; }

        // Content Responsibility
        public string ContentResponsibilityTurkish { get; private set; }
        public string ContentResponsibilityGerman { get; private set; }

        // Links Responsibility
        public string LinksResponsibilityTurkish { get; private set; }
        public string LinksResponsibilityGerman { get; private set; }

        // Copyright
        public string CopyrightTurkish { get; private set; }
        public string CopyrightGerman { get; private set; }

        private Imprint(Guid id) : base(id) { }

        private Imprint(
            Guid id,
            Title organizationName,
            string organizationType,
            Address address,
            Email email,
            PhoneNumber phone,
            Name president,
            Name vicePresident,
            string legalStructureTurkish,
            string legalStructureGerman,
            string purposeTurkish,
            string purposeGerman,
            string taxExemptionTurkish,
            string taxExemptionGerman,
            string contentResponsibilityTurkish,
            string contentResponsibilityGerman,
            string linksResponsibilityTurkish,
            string linksResponsibilityGerman,
            string copyrightTurkish,
            string copyrightGerman
        ) : base(id)
        {
            OrganizationName = organizationName;
            OrganizationType = organizationType;
            Address = address;
            Email = email;
            Phone = phone;
            President = president;
            VicePresident = vicePresident;
            LegalStructureTurkish = legalStructureTurkish;
            LegalStructureGerman = legalStructureGerman;
            PurposeTurkish = purposeTurkish;
            PurposeGerman = purposeGerman;
            TaxExemptionTurkish = taxExemptionTurkish;
            TaxExemptionGerman = taxExemptionGerman;
            ContentResponsibilityTurkish = contentResponsibilityTurkish;
            ContentResponsibilityGerman = contentResponsibilityGerman;
            LinksResponsibilityTurkish = linksResponsibilityTurkish;
            LinksResponsibilityGerman = linksResponsibilityGerman;
            CopyrightTurkish = copyrightTurkish;
            CopyrightGerman = copyrightGerman;
            CreatedAt = DateTime.UtcNow;
        }

        public static Imprint CreateNew(
            Title organizationName,
            string organizationType,
            Address address,
            Email email,
            PhoneNumber phone,
            Name president,
            Name vicePresident,
            string legalStructureTurkish,
            string legalStructureGerman,
            string purposeTurkish,
            string purposeGerman,
            string taxExemptionTurkish,
            string taxExemptionGerman,
            string contentResponsibilityTurkish,
            string contentResponsibilityGerman,
            string linksResponsibilityTurkish,
            string linksResponsibilityGerman,
            string copyrightTurkish,
            string copyrightGerman
        )
        {
            return new Imprint(
                Guid.NewGuid(),
                organizationName,
                organizationType,
                address,
                email,
                phone,
                president,
                vicePresident,
                legalStructureTurkish,
                legalStructureGerman,
                purposeTurkish,
                purposeGerman,
                taxExemptionTurkish,
                taxExemptionGerman,
                contentResponsibilityTurkish,
                contentResponsibilityGerman,
                linksResponsibilityTurkish,
                linksResponsibilityGerman,
                copyrightTurkish,
                copyrightGerman
            );
        }

        public void Update(
            Title organizationName,
            string organizationType,
            Address address,
            Email email,
            PhoneNumber phone,
            Name president,
            Name vicePresident,
            string legalStructureTurkish,
            string legalStructureGerman,
            string purposeTurkish,
            string purposeGerman,
            string taxExemptionTurkish,
            string taxExemptionGerman,
            string contentResponsibilityTurkish,
            string contentResponsibilityGerman,
            string linksResponsibilityTurkish,
            string linksResponsibilityGerman,
            string copyrightTurkish,
            string copyrightGerman
        )
        {
            OrganizationName = organizationName;
            OrganizationType = organizationType;
            Address = address;
            Email = email;
            Phone = phone;
            President = president;
            VicePresident = vicePresident;
            LegalStructureTurkish = legalStructureTurkish;
            LegalStructureGerman = legalStructureGerman;
            PurposeTurkish = purposeTurkish;
            PurposeGerman = purposeGerman;
            TaxExemptionTurkish = taxExemptionTurkish;
            TaxExemptionGerman = taxExemptionGerman;
            ContentResponsibilityTurkish = contentResponsibilityTurkish;
            ContentResponsibilityGerman = contentResponsibilityGerman;
            LinksResponsibilityTurkish = linksResponsibilityTurkish;
            LinksResponsibilityGerman = linksResponsibilityGerman;
            CopyrightTurkish = copyrightTurkish;
            CopyrightGerman = copyrightGerman;
            SetUpdatedAt();
        }
    }
}
