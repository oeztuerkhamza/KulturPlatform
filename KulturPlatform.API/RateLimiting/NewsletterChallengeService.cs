using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Caching.Memory;

namespace KulturPlatform.API;

/// <summary>
/// Stellt fuer die oeffentliche Newsletter-Anmeldung eine signierte
/// Rechenaufgabe (Proof of Work) aus und prueft deren Loesung.
/// </summary>
/// <remarks>
/// Die Bots, die den Endpunkt missbrauchen, laden die Website nie: sie schicken
/// den POST direkt an die API. Ein Honeypot im Formular erreicht sie deshalb
/// nicht. Mit dieser Aufgabe muss jeder Aufrufer zuerst ein Token abholen und
/// dafuer Rechenzeit aufwenden; ein nacktes POST wird verworfen.
/// Der Schluessel wird pro Prozess zufaellig erzeugt. Tokens gelten nur wenige
/// Minuten, ein Neustart macht also hoechstens gerade offene Formulare ungueltig.
/// </remarks>
public sealed class NewsletterChallengeService
{
    private static readonly TimeSpan MaxAge = TimeSpan.FromMinutes(10);
    private const int MaxNonceLength = 32;

    private readonly byte[] _key = RandomNumberGenerator.GetBytes(32);
    private readonly IMemoryCache _cache;

    public NewsletterChallengeService(IConfiguration configuration, IMemoryCache cache)
    {
        _cache = cache;
        DifficultyBits = Math.Clamp(
            configuration.GetValue<int?>("AbuseProtection:ChallengeDifficultyBits") ?? 12, 8, 24);
    }

    /// <summary>Anzahl fuehrender Nullbits, die der SHA-256-Hash haben muss.</summary>
    public int DifficultyBits { get; }

    public string Issue()
    {
        var payload = $"{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}.{Convert.ToHexString(RandomNumberGenerator.GetBytes(16))}";
        return $"{payload}.{Sign(payload)}";
    }

    /// <summary>
    /// Prueft Signatur, Alter und Loesung und entwertet das Token. Jedes Token
    /// gilt genau einmal, sonst liesse sich eine Loesung beliebig oft verwenden.
    /// </summary>
    public bool TryConsume(string? token, string? nonce)
    {
        if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(nonce) || nonce.Length > MaxNonceLength)
        {
            return false;
        }

        var parts = token.Split('.');
        if (parts.Length != 3 || !long.TryParse(parts[0], out var issuedAt))
        {
            return false;
        }

        var expected = Encoding.ASCII.GetBytes(Sign($"{parts[0]}.{parts[1]}"));
        var actual = Encoding.ASCII.GetBytes(parts[2]);
        if (!CryptographicOperations.FixedTimeEquals(expected, actual))
        {
            return false;
        }

        var age = DateTimeOffset.UtcNow - DateTimeOffset.FromUnixTimeSeconds(issuedAt);
        if (age < TimeSpan.Zero || age > MaxAge)
        {
            return false;
        }

        var hash = SHA256.HashData(Encoding.UTF8.GetBytes($"{token}:{nonce}"));
        if (LeadingZeroBits(hash) < DifficultyBits)
        {
            return false;
        }

        var cacheKey = $"newsletter-challenge:{parts[1]}";
        if (_cache.TryGetValue(cacheKey, out _))
        {
            return false;
        }

        _cache.Set(cacheKey, true, MaxAge);
        return true;
    }

    private string Sign(string payload) =>
        Convert.ToHexString(HMACSHA256.HashData(_key, Encoding.ASCII.GetBytes(payload)));

    private static int LeadingZeroBits(byte[] hash)
    {
        var bits = 0;
        foreach (var b in hash)
        {
            if (b == 0)
            {
                bits += 8;
                continue;
            }

            bits += System.Numerics.BitOperations.LeadingZeroCount((uint)b) - 24;
            break;
        }

        return bits;
    }
}
