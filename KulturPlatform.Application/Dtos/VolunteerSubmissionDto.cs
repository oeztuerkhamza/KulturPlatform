namespace KulturPlatform.Application.Dtos
{
    public class VolunteerSubmissionDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime SubmittedAt { get; set; }
        public VolunteerSubmissionDto() { }
    }
}
