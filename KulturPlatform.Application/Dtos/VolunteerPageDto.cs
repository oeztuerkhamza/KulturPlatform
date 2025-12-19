namespace KulturPlatform.Application.Dtos
{
    /// <summary>
    /// DTO for Volunteer Page content (Hero, Features, FAQ, etc.)
    /// </summary>
    public class VolunteerPageDto
    {
        public HeroSectionDto Hero { get; set; }
        public List<BenefitDto> Benefits { get; set; }
        public VolunteerFormDto Form { get; set; }
        public TestimonialsDto Testimonials { get; set; }
    }

    public class HeroSectionDto
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Description { get; set; }
        public string BackgroundImage { get; set; }
        public string CtaButtonText { get; set; }
    }

    public class BenefitDto
    {
        public string Icon { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public class VolunteerFormDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public List<FormFieldDto> Fields { get; set; }
        public string SubmitButtonText { get; set; }
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class FormFieldDto
    {
        public string Name { get; set; }
        public string Label { get; set; }
        public string Type { get; set; } // text, email, tel, textarea
        public string Placeholder { get; set; }
        public bool Required { get; set; }
        public string ValidationMessage { get; set; }
    }

    public class TestimonialsDto
    {
        public string Title { get; set; }
        public List<TestimonialDto> Items { get; set; }
    }

    public class TestimonialDto
    {
        public string Name { get; set; }
        public string Quote { get; set; }
        public string Image { get; set; }
        public string Role { get; set; }
    }
}
