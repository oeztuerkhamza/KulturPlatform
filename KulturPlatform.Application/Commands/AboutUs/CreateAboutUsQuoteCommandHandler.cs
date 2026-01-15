using KulturPlatform.Application.Interfaces.AboutUs;
using KulturPlatform.Domain.Commons.Aggregates;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;
using MediatR;

namespace KulturPlatform.Application.Commands.AboutUs;

public class CreateAboutUsQuoteCommandHandler : IRequestHandler<CreateAboutUsQuoteCommand, Guid>
{
    private readonly IAboutUsQuoteRepository _repository;
    private readonly IUnitOfWork _uow;

    public CreateAboutUsQuoteCommandHandler(IAboutUsQuoteRepository repository, IUnitOfWork uow)
    {
        _repository = repository;
        _uow = uow;
    }

    public async Task<Guid> Handle(CreateAboutUsQuoteCommand request, CancellationToken cancellationToken)
    {
        var quote = AboutUsQuote.Create(
            new Description(request.QuoteTr),
            new Description(request.QuoteDe),
            request.QuoteAuthor
        );

        await _repository.AddAsync(quote, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);

        return quote.Id;
    }
}
