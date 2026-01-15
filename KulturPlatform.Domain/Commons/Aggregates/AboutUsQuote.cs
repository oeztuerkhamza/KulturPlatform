using KulturPlatform.Domain.Commons.Entities;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;

namespace KulturPlatform.Domain.Commons.Aggregates;

public class AboutUsQuote : AuditableEntity, IAggregateRoot
{
    public Description QuoteTr { get; private set; }
    public Description QuoteDe { get; private set; }
    public string QuoteAuthor { get; private set; }

    protected AboutUsQuote() { }

    private AboutUsQuote(Guid id, Description quoteTr, Description quoteDe, string quoteAuthor) : base(id)
    {
        QuoteTr = quoteTr;
        QuoteDe = quoteDe;
        QuoteAuthor = quoteAuthor;
    }

    public static AboutUsQuote Create(Description quoteTr, Description quoteDe, string quoteAuthor)
    {
        if (string.IsNullOrWhiteSpace(quoteAuthor))
            throw new ArgumentException("Quote author cannot be empty", nameof(quoteAuthor));

        return new AboutUsQuote(Guid.NewGuid(), quoteTr, quoteDe, quoteAuthor);
    }

    public void Update(Description quoteTr, Description quoteDe, string quoteAuthor)
    {
        if (string.IsNullOrWhiteSpace(quoteAuthor))
            throw new ArgumentException("Quote author cannot be empty", nameof(quoteAuthor));

        QuoteTr = quoteTr;
        QuoteDe = quoteDe;
        QuoteAuthor = quoteAuthor;
        SetUpdatedAt();
    }
}
