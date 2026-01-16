using KulturPlatform.Application.Dtos;
using KulturPlatform.Application.Interfaces.GuelenMovement;
using Microsoft.EntityFrameworkCore;

namespace KulturPlatform.Infrastructure.ReadServices
{
    public class GuelenMovementReadService : IGuelenMovementReadService
    {
        private readonly AppDbContext _context;

        public GuelenMovementReadService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GuelenMovementDto>> GetAllAsync()
        {
            return await _context.GuelenMovements
                .AsNoTracking()
                .Select(a => new GuelenMovementDto
                {
                    Id = a.Id,
                    TitleTr = a.TitleTurkish.Value,
                    TitleDe = a.TitleGerman.Value,
                    IntroductionTr = a.IntroductionTurkish.Value,
                    IntroductionDe = a.IntroductionGerman.Value,
                    ImageUrl = a.ImageUrl.Value,
                    PhilosophyTitleTr = a.PhilosophyTitleTurkish.Value,
                    PhilosophyTitleDe = a.PhilosophyTitleGerman.Value,
                    PhilosophyContentTr = a.PhilosophyContentTurkish.Value,
                    PhilosophyContentDe = a.PhilosophyContentGerman.Value,
                    DialogTitleTr = a.DialogTitleTurkish.Value,
                    DialogTitleDe = a.DialogTitleGerman.Value,
                    DialogContentTr = a.DialogContentTurkish.Value,
                    DialogContentDe = a.DialogContentGerman.Value,
                    NetworkTitleTr = a.NetworkTitleTurkish.Value,
                    NetworkTitleDe = a.NetworkTitleGerman.Value,
                    NetworkContentTr = a.NetworkContentTurkish.Value,
                    NetworkContentDe = a.NetworkContentGerman.Value,
                    SpiritualTitleTr = a.SpiritualTitleTurkish.Value,
                    SpiritualTitleDe = a.SpiritualTitleGerman.Value,
                    SpiritualContentTr = a.SpiritualContentTurkish.Value,
                    SpiritualContentDe = a.SpiritualContentGerman.Value,
                    VisionTitleTr = a.VisionTitleTurkish.Value,
                    VisionTitleDe = a.VisionTitleGerman.Value,
                    VisionContentTr = a.VisionContentTurkish.Value,
                    VisionContentDe = a.VisionContentGerman.Value
                })
                .ToListAsync();
        }

        public async Task<GuelenMovementDto?> GetByIdAsync(Guid id)
        {
            return await _context.GuelenMovements
                .AsNoTracking()
                .Where(a => a.Id == id)
                .Select(a => new GuelenMovementDto
                {
                    Id = a.Id,
                    TitleTr = a.TitleTurkish.Value,
                    TitleDe = a.TitleGerman.Value,
                    IntroductionTr = a.IntroductionTurkish.Value,
                    IntroductionDe = a.IntroductionGerman.Value,
                    ImageUrl = a.ImageUrl.Value,
                    PhilosophyTitleTr = a.PhilosophyTitleTurkish.Value,
                    PhilosophyTitleDe = a.PhilosophyTitleGerman.Value,
                    PhilosophyContentTr = a.PhilosophyContentTurkish.Value,
                    PhilosophyContentDe = a.PhilosophyContentGerman.Value,
                    DialogTitleTr = a.DialogTitleTurkish.Value,
                    DialogTitleDe = a.DialogTitleGerman.Value,
                    DialogContentTr = a.DialogContentTurkish.Value,
                    DialogContentDe = a.DialogContentGerman.Value,
                    NetworkTitleTr = a.NetworkTitleTurkish.Value,
                    NetworkTitleDe = a.NetworkTitleGerman.Value,
                    NetworkContentTr = a.NetworkContentTurkish.Value,
                    NetworkContentDe = a.NetworkContentGerman.Value,
                    SpiritualTitleTr = a.SpiritualTitleTurkish.Value,
                    SpiritualTitleDe = a.SpiritualTitleGerman.Value,
                    SpiritualContentTr = a.SpiritualContentTurkish.Value,
                    SpiritualContentDe = a.SpiritualContentGerman.Value,
                    VisionTitleTr = a.VisionTitleTurkish.Value,
                    VisionTitleDe = a.VisionTitleGerman.Value,
                    VisionContentTr = a.VisionContentTurkish.Value,
                    VisionContentDe = a.VisionContentGerman.Value
                })
                .FirstOrDefaultAsync();
        }
    }
}