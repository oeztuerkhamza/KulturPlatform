using System.ComponentModel.DataAnnotations;

namespace KulturPlatform.Application.Dtos.AuthDto
{
    public class LoginDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
