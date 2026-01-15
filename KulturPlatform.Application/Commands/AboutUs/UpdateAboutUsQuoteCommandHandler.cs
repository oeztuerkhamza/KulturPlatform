using KulturPlatform.Application.Interfaces.AboutUs;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;
using MediatR;

namespace KulturPlatform.Application.Commands.AboutUs;

public class UpdateAboutUsQuoteCommandHandler : IRequestHandler<UpdateAboutUsQuoteCommand>
{
    private readonly IAboutUsQuoteRepository _repository;
    private readonly IUnitOfWork _uow;

    public UpdateAboutUsQuoteCommandHandler(IAboutUsQuoteRepository repository, IUnitOfWork uow)
    {
        _repository = repository;
        _uow = uow;
    }

    public async Task Handle(UpdateAboutUsQuoteCommand request, CancellationToken cancellationToken)
    {
        var quote = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (quote == null)
            throw new KeyNotFoundException($"AboutUsQuote with Id {request.Id} not found.");

        quote.Update(
            new Description(request.QuoteTr),
            new Description(request.QuoteDe),
            request.QuoteAuthor
        );

        await _repository.UpdateAsync(quote, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);
    }
}
