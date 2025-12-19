using KulturPlatform.Application.Dtos;
using KulturPlatform.Application.Interfaces.DonatePage;
using Microsoft.EntityFrameworkCore;

namespace KulturPlatform.Infrastructure.ReadServices
{
    public class DonatePageReadService : IDonatePageReadService
    {
        private readonly AppDbContext _context;

        public DonatePageReadService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<DonatePageDto?> GetAsync()
        {
            return await _context.DonatePages
                .AsNoTracking()
                .Select(a => new DonatePageDto
                {
                    Id = a.Id,
                    HeroTitleTr = a.HeroTitleTurkish.Value,
                    HeroTitleDe = a.HeroTitleGerman.Value,
                    HeroSubtitleTr = a.HeroSubtitleTurkish.Value,
                    HeroSubtitleDe = a.HeroSubtitleGerman.Value,
                    HeroImageUrl = a.HeroImageUrl.Value,
                    AccountHolder = a.AccountHolder,
                    Iban = a.Iban,
                    BankName = a.BankName,
                    ContentTr = a.ContentTurkish,
                    ContentDe = a.ContentGerman
                })
                .FirstOrDefaultAsync();
        }
    }
}