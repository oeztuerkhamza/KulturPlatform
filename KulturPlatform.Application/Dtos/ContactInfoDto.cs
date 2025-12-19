using KulturPlatform.Application.Dtos.LocalizationDto;

namespace KulturPlatform.Application.Dtos
{
    /// <summary>
    /// Contact Information DTO with bilingual support (Turkish/German)
    /// </summary>
    public class ContactInfoDto
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Email address
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Phone number (e.g., "+49 123 456 7890")
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Street in Turkish
        /// </summary>
        public AddressDto Address { get; set; }


        /// <summary>
        /// Social media links
        /// </summary>
        public SocialMediaLinksDto SocialMedia { get; set; }

        /// <summary>
        /// Office hours in Turkish
        /// </summary>
        public string OfficeHours { get; set; }


        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    /// <summary>
    /// Social Media Links DTO
    /// </summary>
    public class SocialMediaLinksDto
    {
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public string Twitter { get; set; }
    }

    /// <summary>
    /// DTO for creating/updating contact info
    /// </summary>
    public class SaveContactInfoDto
    {
        public string Email { get; set; }
        public string Phone { get; set; }
        public AddressDto Address { get; set; }   // ✅
        public SocialMediaLinksDto SocialMedia { get; set; }
        public string OfficeHours { get; set; }
    }


    /// <summary>
    /// DTO for contact page content (for frontend)
    /// </summary>
    public class ContactPageDto
    {
        public ContactInfoDto ContactInfo { get; set; }
        public ContactFormDto Form { get; set; }
        public MapDto Map { get; set; }
    }

    public class ContactFormDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public List<FormFieldDto> Fields { get; set; }
        public string SubmitButtonText { get; set; }
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class MapDto
    {
        public string Title { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string EmbedUrl { get; set; }
    }
}
