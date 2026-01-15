using MediatR;

namespace KulturPlatform.Application.Commands.AboutUs;

public record UpdateAboutUsQuoteCommand(
    Guid Id,
    string QuoteTr,
    string QuoteDe,
    string QuoteAuthor
) : IRequest;
