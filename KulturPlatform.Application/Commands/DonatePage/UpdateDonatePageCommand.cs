using MediatR;

namespace KulturPlatform.Application.Commands.DonatePage
{
    public record UpdateDonatePageCommand(
        Guid Id,
        string HeroTitleTr,
        string HeroTitleDe,
        string HeroSubtitleTr,
        string HeroSubtitleDe,
        string HeroImageUrl,
        string AccountHolder,
        string Iban,
        string BankName,
        string ContentTr,
        string ContentDe
    ) : IRequest<bool>;
}
