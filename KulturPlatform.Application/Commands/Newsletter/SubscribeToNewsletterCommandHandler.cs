using KulturPlatform.Application.Interfaces.Newsletter;
using MediatR;

namespace KulturPlatform.Application.Commands.Newsletter
{
    public class SubscribeToNewsletterCommandHandler : IRequestHandler<SubscribeToNewsletterCommand, bool>
    {
        private readonly INewsletterService _newsletterService;

        public SubscribeToNewsletterCommandHandler(INewsletterService newsletterService)
        {
            _newsletterService = newsletterService;
        }

        public async Task<bool> Handle(SubscribeToNewsletterCommand request, CancellationToken cancellationToken)
        {
            return await _newsletterService.SubscribeAsync(request.Email, request.FullName, cancellationToken);
        }
    }
}
