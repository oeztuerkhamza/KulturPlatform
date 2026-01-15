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

        public async Task<DonatePageDto?> GetDonatePageAsync(CancellationToken cancellationToken = default)
        {
            return await _context.DonatePages
                .AsNoTracking()
                .Select(a => new DonatePageDto
                {
                    Id = a.Id,
                    // Hero Section
                    HeroTitleTr = a.HeroTitleTurkish.Value,
                    HeroTitleDe = a.HeroTitleGerman.Value,
                    HeroSubtitleTr = a.HeroSubtitleTurkish.Value,
                    HeroSubtitleDe = a.HeroSubtitleGerman.Value,
                    HeroImageUrl = a.HeroImageUrl.Value,
                    // Feature Highlights
                    Feature1TitleTr = a.Feature1TitleTurkish.Value,
                    Feature1TitleDe = a.Feature1TitleGerman.Value,
                    Feature2TitleTr = a.Feature2TitleTurkish.Value,
                    Feature2TitleDe = a.Feature2TitleGerman.Value,
                    Feature3TitleTr = a.Feature3TitleTurkish.Value,
                    Feature3TitleDe = a.Feature3TitleGerman.Value,
                    // Why Donate Section
                    WhyDonateTitleTr = a.WhyDonateTitleTurkish.Value,
                    WhyDonateTitleDe = a.WhyDonateTitleGerman.Value,
                    WhyDonateDescriptionTr = a.WhyDonateDescriptionTurkish.Value,
                    WhyDonateDescriptionDe = a.WhyDonateDescriptionGerman.Value,
                    // Where Section
                    WhereTitleTr = a.WhereTitleTurkish.Value,
                    WhereTitleDe = a.WhereTitleGerman.Value,
                    WhereDescriptionTr = a.WhereDescriptionTurkish.Value,
                    WhereDescriptionDe = a.WhereDescriptionGerman.Value,
                    TaxInfoTr = a.TaxInfoTurkish.Value,
                    TaxInfoDe = a.TaxInfoGerman.Value,
                    // Bank Account Details
                    AccountHolder = a.AccountHolder,
                    Iban = a.Iban,
                    BicSwift = a.BicSwift,
                    BankName = a.BankName,
                    // PayPal Details
                    PayPalUrl = a.PayPalUrl.Value,
                    PayPalHandle = a.PayPalHandle,
                    // Legacy Content
                    ContentTr = a.ContentTurkish,
                    ContentDe = a.ContentGerman
                })
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}