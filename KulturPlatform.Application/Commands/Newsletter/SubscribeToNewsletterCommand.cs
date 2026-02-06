using MediatR;

namespace KulturPlatform.Application.Commands.Newsletter
{
    public class SubscribeToNewsletterCommand : IRequest<bool>
    {
        public string Email { get; set; } = string.Empty;
        public string? FullName { get; set; }
    }
}
