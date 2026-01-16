using KulturPlatform.Domain.Commons.Entities;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;

namespace KulturPlatform.Domain.Commons.Aggregates
{
    public class GuelenMovement : AuditableEntity, IAggregateRoot
    {
        // Main Content
        public Title TitleTurkish { get; private set; }
        public Title TitleGerman { get; private set; }
        public Description IntroductionTurkish { get; private set; }
        public Description IntroductionGerman { get; private set; }
        public Url ImageUrl { get; private set; }

        // 1. Philosophy Section - "Hizmet" Ideology
        public Title PhilosophyTitleTurkish { get; private set; }
        public Title PhilosophyTitleGerman { get; private set; }
        public Description PhilosophyContentTurkish { get; private set; }
        public Description PhilosophyContentGerman { get; private set; }

        // 2. Dialog Section
        public Title DialogTitleTurkish { get; private set; }
        public Title DialogTitleGerman { get; private set; }
        public Description DialogContentTurkish { get; private set; }
        public Description DialogContentGerman { get; private set; }

        // 3. Global Network Section
        public Title NetworkTitleTurkish { get; private set; }
        public Title NetworkTitleGerman { get; private set; }
        public Description NetworkContentTurkish { get; private set; }
        public Description NetworkContentGerman { get; private set; }

        // 4. Spiritual Roots Section
        public Title SpiritualTitleTurkish { get; private set; }
        public Title SpiritualTitleGerman { get; private set; }
        public Description SpiritualContentTurkish { get; private set; }
        public Description SpiritualContentGerman { get; private set; }

        // 5. Vision Section
        public Title VisionTitleTurkish { get; private set; }
        public Title VisionTitleGerman { get; private set; }
        public Description VisionContentTurkish { get; private set; }
        public Description VisionContentGerman { get; private set; }

        private GuelenMovement(Guid id) : base(id) { }

        private GuelenMovement(
            Guid id,
            Title titleTurkish,
            Title titleGerman,
            Description introductionTurkish,
            Description introductionGerman,
            Url imageUrl,
            Title philosophyTitleTurkish,
            Title philosophyTitleGerman,
            Description philosophyContentTurkish,
            Description philosophyContentGerman,
            Title dialogTitleTurkish,
            Title dialogTitleGerman,
            Description dialogContentTurkish,
            Description dialogContentGerman,
            Title networkTitleTurkish,
            Title networkTitleGerman,
            Description networkContentTurkish,
            Description networkContentGerman,
            Title spiritualTitleTurkish,
            Title spiritualTitleGerman,
            Description spiritualContentTurkish,
            Description spiritualContentGerman,
            Title visionTitleTurkish,
            Title visionTitleGerman,
            Description visionContentTurkish,
            Description visionContentGerman)
            : base(id)
        {
            TitleTurkish = titleTurkish;
            TitleGerman = titleGerman;
            IntroductionTurkish = introductionTurkish;
            IntroductionGerman = introductionGerman;
            ImageUrl = imageUrl;

            PhilosophyTitleTurkish = philosophyTitleTurkish;
            PhilosophyTitleGerman = philosophyTitleGerman;
            PhilosophyContentTurkish = philosophyContentTurkish;
            PhilosophyContentGerman = philosophyContentGerman;

            DialogTitleTurkish = dialogTitleTurkish;
            DialogTitleGerman = dialogTitleGerman;
            DialogContentTurkish = dialogContentTurkish;
            DialogContentGerman = dialogContentGerman;

            NetworkTitleTurkish = networkTitleTurkish;
            NetworkTitleGerman = networkTitleGerman;
            NetworkContentTurkish = networkContentTurkish;
            NetworkContentGerman = networkContentGerman;

            SpiritualTitleTurkish = spiritualTitleTurkish;
            SpiritualTitleGerman = spiritualTitleGerman;
            SpiritualContentTurkish = spiritualContentTurkish;
            SpiritualContentGerman = spiritualContentGerman;

            VisionTitleTurkish = visionTitleTurkish;
            VisionTitleGerman = visionTitleGerman;
            VisionContentTurkish = visionContentTurkish;
            VisionContentGerman = visionContentGerman;

            CreatedAt = DateTime.UtcNow;
        }

        public static GuelenMovement CreateNew(
            Title titleTurkish,
            Title titleGerman,
            Description introductionTurkish,
            Description introductionGerman,
            Url imageUrl,
            Title philosophyTitleTurkish,
            Title philosophyTitleGerman,
            Description philosophyContentTurkish,
            Description philosophyContentGerman,
            Title dialogTitleTurkish,
            Title dialogTitleGerman,
            Description dialogContentTurkish,
            Description dialogContentGerman,
            Title networkTitleTurkish,
            Title networkTitleGerman,
            Description networkContentTurkish,
            Description networkContentGerman,
            Title spiritualTitleTurkish,
            Title spiritualTitleGerman,
            Description spiritualContentTurkish,
            Description spiritualContentGerman,
            Title visionTitleTurkish,
            Title visionTitleGerman,
            Description visionContentTurkish,
            Description visionContentGerman)
        {
            return new GuelenMovement(
                Guid.NewGuid(),
                titleTurkish,
                titleGerman,
                introductionTurkish,
                introductionGerman,
                imageUrl,
                philosophyTitleTurkish,
                philosophyTitleGerman,
                philosophyContentTurkish,
                philosophyContentGerman,
                dialogTitleTurkish,
                dialogTitleGerman,
                dialogContentTurkish,
                dialogContentGerman,
                networkTitleTurkish,
                networkTitleGerman,
                networkContentTurkish,
                networkContentGerman,
                spiritualTitleTurkish,
                spiritualTitleGerman,
                spiritualContentTurkish,
                spiritualContentGerman,
                visionTitleTurkish,
                visionTitleGerman,
                visionContentTurkish,
                visionContentGerman);
        }

        public void Update(
            Title titleTurkish,
            Title titleGerman,
            Description introductionTurkish,
            Description introductionGerman,
            Url imageUrl,
            Title philosophyTitleTurkish,
            Title philosophyTitleGerman,
            Description philosophyContentTurkish,
            Description philosophyContentGerman,
            Title dialogTitleTurkish,
            Title dialogTitleGerman,
            Description dialogContentTurkish,
            Description dialogContentGerman,
            Title networkTitleTurkish,
            Title networkTitleGerman,
            Description networkContentTurkish,
            Description networkContentGerman,
            Title spiritualTitleTurkish,
            Title spiritualTitleGerman,
            Description spiritualContentTurkish,
            Description spiritualContentGerman,
            Title visionTitleTurkish,
            Title visionTitleGerman,
            Description visionContentTurkish,
            Description visionContentGerman)
        {
            UpdateMainContent(titleTurkish, titleGerman, introductionTurkish, introductionGerman, imageUrl);
            UpdatePhilosophy(philosophyTitleTurkish, philosophyTitleGerman, philosophyContentTurkish, philosophyContentGerman);
            UpdateDialog(dialogTitleTurkish, dialogTitleGerman, dialogContentTurkish, dialogContentGerman);
            UpdateNetwork(networkTitleTurkish, networkTitleGerman, networkContentTurkish, networkContentGerman);
            UpdateSpiritual(spiritualTitleTurkish, spiritualTitleGerman, spiritualContentTurkish, spiritualContentGerman);
            UpdateVision(visionTitleTurkish, visionTitleGerman, visionContentTurkish, visionContentGerman);
            
            SetUpdatedAt();
        }

        private void UpdateMainContent(Title titleTr, Title titleDe, Description introTr, Description introDe, Url image)
        {
            TitleTurkish = titleTr;
            TitleGerman = titleDe;
            IntroductionTurkish = introTr;
            IntroductionGerman = introDe;
            ImageUrl = image;
        }

        private void UpdatePhilosophy(Title titleTr, Title titleDe, Description contentTr, Description contentDe)
        {
            PhilosophyTitleTurkish = titleTr;
            PhilosophyTitleGerman = titleDe;
            PhilosophyContentTurkish = contentTr;
            PhilosophyContentGerman = contentDe;
        }

        private void UpdateDialog(Title titleTr, Title titleDe, Description contentTr, Description contentDe)
        {
            DialogTitleTurkish = titleTr;
            DialogTitleGerman = titleDe;
            DialogContentTurkish = contentTr;
            DialogContentGerman = contentDe;
        }

        private void UpdateNetwork(Title titleTr, Title titleDe, Description contentTr, Description contentDe)
        {
            NetworkTitleTurkish = titleTr;
            NetworkTitleGerman = titleDe;
            NetworkContentTurkish = contentTr;
            NetworkContentGerman = contentDe;
        }

        private void UpdateSpiritual(Title titleTr, Title titleDe, Description contentTr, Description contentDe)
        {
            SpiritualTitleTurkish = titleTr;
            SpiritualTitleGerman = titleDe;
            SpiritualContentTurkish = contentTr;
            SpiritualContentGerman = contentDe;
        }

        private void UpdateVision(Title titleTr, Title titleDe, Description contentTr, Description contentDe)
        {
            VisionTitleTurkish = titleTr;
            VisionTitleGerman = titleDe;
            VisionContentTurkish = contentTr;
            VisionContentGerman = contentDe;
        }
    }
}
