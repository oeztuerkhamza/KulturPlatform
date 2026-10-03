namespace KulturPlatform.API;

/// <summary>
/// Gemeinsame Konstanten und Helfer fuer die Drosselung des oeffentlichen
/// Newsletter-Anmeldeendpunkts.
/// </summary>
public static class NewsletterRateLimiting
{
    /// <summary>Name der Named Policy, siehe <c>Program.cs</c>.</summary>
    public const string SubscribePolicy = "newsletter-subscribe";

    /// <summary>Pfad, den der globale Limiter als Notbremse ueberwacht.</summary>
    public const string SubscribePath = "/api/newsletter/subscribe";

    /// <summary>
    /// Liefert die Client-IP als Partitionsschluessel.
    /// </summary>
    /// <remarks>
    /// nginx setzt <c>X-Real-IP</c> per <c>proxy_set_header X-Real-IP $remote_addr</c>
    /// und ueberschreibt dabei einen vom Client mitgeschickten Wert. Der
    /// API-Container veroeffentlicht keinen Port und ist ausschliesslich ueber
    /// nginx erreichbar, deshalb ist der Header hier nicht faelschbar.
    /// <c>X-Forwarded-For</c> wird bewusst nicht benutzt: nginx haengt dort per
    /// <c>$proxy_add_x_forwarded_for</c> an einen vorhandenen Wert an, sodass der
    /// Client den Anfang der Liste kontrolliert.
    /// </remarks>
    public static string ResolveClientIp(HttpContext httpContext)
    {
        var realIp = httpContext.Request.Headers["X-Real-IP"].ToString();
        if (!string.IsNullOrWhiteSpace(realIp))
        {
            return realIp.Trim();
        }

        return httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";
    }
}
