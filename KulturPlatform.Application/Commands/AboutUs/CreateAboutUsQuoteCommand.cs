using MediatR;

namespace KulturPlatform.Application.Commands.AboutUs;

public record CreateAboutUsQuoteCommand(
    string QuoteTr,
    string QuoteDe,
    string QuoteAuthor
) : IRequest<Guid>;
