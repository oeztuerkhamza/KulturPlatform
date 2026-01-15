namespace KulturPlatform.Application.Dtos
{
    public class PartnerDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string? LogoUrl { get; set; }
        public string? DescriptionTr { get; set; }
        public string? DescriptionDe { get; set; }
        public string? WebsiteUrl { get; set; }
        public int DisplayOrder { get; set; }

        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public PartnerDto()
        {
        }
    }

}
