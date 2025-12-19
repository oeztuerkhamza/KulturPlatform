namespace KulturPlatform.Application.Dtos.AdminDto
{
    public class AdminDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string? Name { get; set; }
        public string Role { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public DateTime? LastLoginAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
