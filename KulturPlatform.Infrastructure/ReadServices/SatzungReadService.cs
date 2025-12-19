using KulturPlatform.Application.Dtos.SatzungDto;
using KulturPlatform.Application.Interfaces.Satzung;
using Microsoft.EntityFrameworkCore;

namespace KulturPlatform.Infrastructure.ReadServices
{
    public class SatzungReadService : ISatzungReadService
    {
        private readonly AppDbContext _context;

        public SatzungReadService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SatzungDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Satzungen
                .AsNoTracking()
                .OrderBy(a => a.TitleGerman.Value)
                .Select(a => new SatzungDto
                {
                    Id = a.Id,
                    Key = a.Key,
                    TitleTurkish = a.TitleTurkish.Value,
                    TitleGerman = a.TitleGerman.Value,

                    NameAndSeatTurkish = a.NameAndSeatTurkish,
                    NameAndSeatGerman = a.NameAndSeatGerman,
                    NameDescTurkish = a.NameDescTurkish,
                    NameDescGerman = a.NameDescGerman,
                    SeatTurkish = a.SeatTurkish,
                    SeatGerman = a.SeatGerman,
                    SeatDescTurkish = a.SeatDescTurkish,
                    SeatDescGerman = a.SeatDescGerman,
                    FiscalYearTurkish = a.FiscalYearTurkish,
                    FiscalYearGerman = a.FiscalYearGerman,
                    FiscalYearDescTurkish = a.FiscalYearDescTurkish,
                    FiscalYearDescGerman = a.FiscalYearDescGerman,
                    PurposeOfAssociationTurkish = a.PurposeOfAssociationTurkish,
                    PurposeOfAssociationGerman = a.PurposeOfAssociationGerman,

                    Purposes = a.Purposes, // domain VO direkt kullanılıyor

                    GemeinnuetzigkeitTurkish = a.GemeinnuetzigkeitTurkish,
                    GemeinnuetzigkeitGerman = a.GemeinnuetzigkeitGerman,
                    PoliticalNeutralityTurkish = a.PoliticalNeutralityTurkish,
                    PoliticalNeutralityGerman = a.PoliticalNeutralityGerman,
                    Memberships = a.Memberships // domain VO direkt kullanılıyor

                })
                .ToListAsync(cancellationToken);
        }

        public async Task<SatzungDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Satzungen
                .AsNoTracking()
                .Where(a => a.Id == id)
                .Select(a => new SatzungDto
                {
                    Id = a.Id,
                    Key = a.Key,
                    TitleTurkish = a.TitleTurkish.Value,
                    TitleGerman = a.TitleGerman.Value,

                    NameAndSeatTurkish = a.NameAndSeatTurkish,
                    NameAndSeatGerman = a.NameAndSeatGerman,
                    NameDescTurkish = a.NameDescTurkish,
                    NameDescGerman = a.NameDescGerman,
                    SeatTurkish = a.SeatTurkish,
                    SeatGerman = a.SeatGerman,
                    SeatDescTurkish = a.SeatDescTurkish,
                    SeatDescGerman = a.SeatDescGerman,
                    FiscalYearTurkish = a.FiscalYearTurkish,
                    FiscalYearGerman = a.FiscalYearGerman,
                    FiscalYearDescTurkish = a.FiscalYearDescTurkish,
                    FiscalYearDescGerman = a.FiscalYearDescGerman,
                    PurposeOfAssociationTurkish = a.PurposeOfAssociationTurkish,
                    PurposeOfAssociationGerman = a.PurposeOfAssociationGerman,

                    Purposes = a.Purposes,

                    GemeinnuetzigkeitTurkish = a.GemeinnuetzigkeitTurkish,
                    GemeinnuetzigkeitGerman = a.GemeinnuetzigkeitGerman,
                    PoliticalNeutralityTurkish = a.PoliticalNeutralityTurkish,
                    PoliticalNeutralityGerman = a.PoliticalNeutralityGerman,

                    Memberships = a.Memberships
                })
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<SatzungDto?> GetByKeyAsync(string key, CancellationToken cancellationToken)
        {
            return await _context.Satzungen
                .AsNoTracking()
                .Where(a => a.Key == key)
                .Select(a => new SatzungDto
                {
                    Id = a.Id,
                    Key = a.Key,
                    TitleTurkish = a.TitleTurkish.Value,
                    TitleGerman = a.TitleGerman.Value,

                    NameAndSeatTurkish = a.NameAndSeatTurkish,
                    NameAndSeatGerman = a.NameAndSeatGerman,
                    NameDescTurkish = a.NameDescTurkish,
                    NameDescGerman = a.NameDescGerman,
                    SeatTurkish = a.SeatTurkish,
                    SeatGerman = a.SeatGerman,
                    SeatDescTurkish = a.SeatDescTurkish,
                    SeatDescGerman = a.SeatDescGerman,
                    FiscalYearTurkish = a.FiscalYearTurkish,
                    FiscalYearGerman = a.FiscalYearGerman,
                    FiscalYearDescTurkish = a.FiscalYearDescTurkish,
                    FiscalYearDescGerman = a.FiscalYearDescGerman,
                    PurposeOfAssociationTurkish = a.PurposeOfAssociationTurkish,
                    PurposeOfAssociationGerman = a.PurposeOfAssociationGerman,

                    Purposes = a.Purposes,

                    GemeinnuetzigkeitTurkish = a.GemeinnuetzigkeitTurkish,
                    GemeinnuetzigkeitGerman = a.GemeinnuetzigkeitGerman,
                    PoliticalNeutralityTurkish = a.PoliticalNeutralityTurkish,
                    PoliticalNeutralityGerman = a.PoliticalNeutralityGerman,

                    Memberships = a.Memberships
                })
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
