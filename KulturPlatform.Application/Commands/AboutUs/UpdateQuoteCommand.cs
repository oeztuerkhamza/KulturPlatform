using KulturPlatform.Application.Dtos;
using MediatR;

namespace KulturPlatform.Application.Commands.AboutUs
{
    public record UpdateQuoteCommand(
        DescriptionDto QuoteTr,
        DescriptionDto QuoteDe,
        string QuoteAuthor
    ) : IRequest;

    public record CreateQuoteCommand(
        DescriptionDto QuoteTr,
        DescriptionDto QuoteDe,
        string QuoteAuthor
    ) : IRequest;
}
