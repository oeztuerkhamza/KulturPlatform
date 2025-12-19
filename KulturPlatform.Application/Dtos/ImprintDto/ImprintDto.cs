using KulturPlatform.Application.Dtos.LocalizationDto;

namespace KulturPlatform.Application.Dtos.ImprintDto
{
    public class ImprintDto
    {
        public Guid Id { get; set; }
        public string OrganizationName { get; set; }
        public string OrganizationType { get; set; }

        // Street
        public AddressDto Address { get; set; }

        // Contact
        public string Email { get; set; }
        public string Phone { get; set; }

        // Responsible Persons
        public string President { get; set; }
        public string VicePresident { get; set; }

        // Legal Structure
        public string LegalStructureTurkish { get; set; }
        public string LegalStructureGerman { get; set; }
        public string PurposeTurkish { get; set; }
        public string PurposeGerman { get; set; }
        public string TaxExemptionTurkish { get; set; }
        public string TaxExemptionGerman { get; set; }

        // Content Responsibility
        public string ContentResponsibilityTurkish { get; set; }
        public string ContentResponsibilityGerman { get; set; }

        // Links Responsibility
        public string LinksResponsibilityTurkish { get; set; }
        public string LinksResponsibilityGerman { get; set; }

        // Copyright
        public string CopyrightTurkish { get; set; }
        public string CopyrightGerman { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class CreateImprintDto
    {
        public string OrganizationName { get; set; }
        public string OrganizationType { get; set; }
        public AddressDto Address { get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }

        public string President { get; set; }
        public string VicePresident { get; set; }

        public string LegalStructureTurkish { get; set; }
        public string LegalStructureGerman { get; set; }
        public string PurposeTurkish { get; set; }
        public string PurposeGerman { get; set; }
        public string TaxExemptionTurkish { get; set; }
        public string TaxExemptionGerman { get; set; }

        public string ContentResponsibilityTurkish { get; set; }
        public string ContentResponsibilityGerman { get; set; }

        public string LinksResponsibilityTurkish { get; set; }
        public string LinksResponsibilityGerman { get; set; }

        public string CopyrightTurkish { get; set; }
        public string CopyrightGerman { get; set; }
    }


    public class UpdateImprintDto
    {
        public string OrganizationName { get; set; }
        public string OrganizationType { get; set; }
        public AddressDto Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string President { get; set; }
        public string VicePresident { get; set; }
        public string LegalStructureTurkish { get; set; }
        public string LegalStructureGerman { get; set; }
        public string PurposeTurkish { get; set; }
        public string PurposeGerman { get; set; }
        public string TaxExemptionTurkish { get; set; }
        public string TaxExemptionGerman { get; set; }
        public string ContentResponsibilityTurkish { get; set; }
        public string ContentResponsibilityGerman { get; set; }
        public string LinksResponsibilityTurkish { get; set; }
        public string LinksResponsibilityGerman { get; set; }
        public string CopyrightTurkish { get; set; }
        public string CopyrightGerman { get; set; }
    }
}
