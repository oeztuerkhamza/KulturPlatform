namespace KulturPlatform.Application.Dtos.LocalizationDto
{
    public class LocalizationResourceDto
    {
        public Guid Id { get; set; }
        public string Key { get; set; }
        public string Turkish { get; set; }
        public string German { get; set; }
        public string English { get; set; }
        public string Section { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
    }

    public class CreateLocalizationResourceDto
    {
        public string Key { get; set; }
        public string Turkish { get; set; }
        public string German { get; set; }
        public string English { get; set; }
        public string Section { get; set; }
        public string? Description { get; set; }
    }

    public class UpdateLocalizationResourceDto
    {
        public string Turkish { get; set; }
        public string German { get; set; }
        public string English { get; set; }
        public string? Description { get; set; }
    }
}
