using KulturPlatform.Application.Interfaces.Newsletter;
using MediatR;

namespace KulturPlatform.Application.Commands.Newsletter
{
    public class VerifyNewsletterSubscriptionCommand : IRequest<bool>
    {
        public string Token { get; set; } = string.Empty;
    }

    public class VerifyNewsletterSubscriptionCommandHandler : IRequestHandler<VerifyNewsletterSubscriptionCommand, bool>
    {
        private readonly INewsletterService _newsletterService;

        public VerifyNewsletterSubscriptionCommandHandler(INewsletterService newsletterService)
        {
            _newsletterService = newsletterService;
        }

        public async Task<bool> Handle(VerifyNewsletterSubscriptionCommand request, CancellationToken cancellationToken)
        {
            return await _newsletterService.VerifySubscriptionAsync(request.Token, cancellationToken);
        }
    }

    public class UnsubscribeFromNewsletterCommand : IRequest<bool>
    {
        public string Token { get; set; } = string.Empty;
    }

    public class UnsubscribeFromNewsletterCommandHandler : IRequestHandler<UnsubscribeFromNewsletterCommand, bool>
    {
        private readonly INewsletterService _newsletterService;

        public UnsubscribeFromNewsletterCommandHandler(INewsletterService newsletterService)
        {
            _newsletterService = newsletterService;
        }

        public async Task<bool> Handle(UnsubscribeFromNewsletterCommand request, CancellationToken cancellationToken)
        {
            return await _newsletterService.UnsubscribeAsync(request.Token, cancellationToken);
        }
    }
}
