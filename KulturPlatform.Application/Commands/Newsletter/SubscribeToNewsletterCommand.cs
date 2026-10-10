using MediatR;

namespace KulturPlatform.Application.Commands.Newsletter
{
    public class SubscribeToNewsletterCommand : IRequest<bool>
    {
        public string Email { get; set; } = string.Empty;
        public string? FullName { get; set; }

        /// <summary>
        /// Honeypot. Im Formular per CSS versteckt und fuer Screenreader mit
        /// aria-hidden/tabindex=-1 ausgenommen, also fuellt es nur ein Bot aus.
        /// Ist der Wert nicht leer, wird die Anmeldung verworfen.
        /// Der harmlose Name ist Absicht: Bots fuellen bevorzugt Felder, die
        /// nach einem echten Formularfeld aussehen.
        /// </summary>
        public string? Website { get; set; }

        /// <summary>
        /// Token aus GET /api/newsletter/challenge und die dazu errechnete
        /// Loesung. Ohne gueltiges Paar wird die Anmeldung abgelehnt.
        /// </summary>
        public string? ChallengeToken { get; set; }
        public string? ChallengeNonce { get; set; }
    }
}
