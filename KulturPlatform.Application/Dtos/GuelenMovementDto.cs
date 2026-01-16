namespace KulturPlatform.Application.Dtos
{
    public class GuelenMovementDto
    {
        public Guid Id { get; set; }
        
        // Main Content
        public string TitleTr { get; set; }
        public string TitleDe { get; set; }
        public string IntroductionTr { get; set; }
        public string IntroductionDe { get; set; }
        public string ImageUrl { get; set; }

        // Philosophy Section
        public string PhilosophyTitleTr { get; set; }
        public string PhilosophyTitleDe { get; set; }
        public string PhilosophyContentTr { get; set; }
        public string PhilosophyContentDe { get; set; }

        // Dialog Section
        public string DialogTitleTr { get; set; }
        public string DialogTitleDe { get; set; }
        public string DialogContentTr { get; set; }
        public string DialogContentDe { get; set; }

        // Network Section
        public string NetworkTitleTr { get; set; }
        public string NetworkTitleDe { get; set; }
        public string NetworkContentTr { get; set; }
        public string NetworkContentDe { get; set; }

        // Spiritual Roots Section
        public string SpiritualTitleTr { get; set; }
        public string SpiritualTitleDe { get; set; }
        public string SpiritualContentTr { get; set; }
        public string SpiritualContentDe { get; set; }

        // Vision Section
        public string VisionTitleTr { get; set; }
        public string VisionTitleDe { get; set; }
        public string VisionContentTr { get; set; }
        public string VisionContentDe { get; set; }

        public GuelenMovementDto() { }
    }
}
