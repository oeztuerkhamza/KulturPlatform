using System.Net;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using FluentAssertions;
using KulturPlatform.Application.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace KulturPlatform.API.IntegrationTests.Controllers;

public class NewsletterControllerTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly RecordingEmailService _emails = new();

    public NewsletterControllerTests(CustomWebApplicationFactory factory)
    {
        _client = factory.WithWebHostBuilder(builder =>
        {
            // The per-IP limit of 3 would otherwise trip inside a single test.
            builder.UseSetting("AbuseProtection:SubscribePerIpPermits", "100");
            builder.ConfigureTestServices(services =>
                services.AddScoped<IEmailService>(_ => _emails));
        }).CreateClient();
    }

    [Fact]
    public async Task Subscribe_WithoutChallenge_ShouldBeRejectedAndSendNoMail()
    {
        var response = await _client.PostAsJsonAsync("/api/newsletter/subscribe",
            new { email = "bot-victim@example.com" });

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        _emails.Recipients.Should().BeEmpty();
    }

    [Fact]
    public async Task Subscribe_WithUnsolvedChallenge_ShouldBeRejected()
    {
        var (token, _) = await GetChallengeAsync();

        var response = await _client.PostAsJsonAsync("/api/newsletter/subscribe",
            new { email = "unsolved@example.com", challengeToken = token, challengeNonce = "not-a-solution" });

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        _emails.Recipients.Should().BeEmpty();
    }

    [Fact]
    public async Task Subscribe_WithForgedToken_ShouldBeRejected()
    {
        var forged = $"{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}.{new string('A', 32)}.{new string('B', 64)}";

        var response = await _client.PostAsJsonAsync("/api/newsletter/subscribe",
            new { email = "forged@example.com", challengeToken = forged, challengeNonce = Solve(forged, 12) });

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        _emails.Recipients.Should().BeEmpty();
    }

    [Fact]
    public async Task Subscribe_WithSolvedChallenge_ShouldSendOneVerificationMail()
    {
        var response = await SubscribeAsync("real-user@example.com");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        _emails.Recipients.Should().Equal("real-user@example.com");
    }

    [Fact]
    public async Task Subscribe_ReplayingASolvedChallenge_ShouldBeRejected()
    {
        var (token, difficulty) = await GetChallengeAsync();
        var body = new { email = "replay@example.com", challengeToken = token, challengeNonce = Solve(token, difficulty) };

        var first = await _client.PostAsJsonAsync("/api/newsletter/subscribe", body);
        var second = await _client.PostAsJsonAsync("/api/newsletter/subscribe", body);

        first.StatusCode.Should().Be(HttpStatusCode.OK);
        second.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        _emails.Recipients.Should().Equal("replay@example.com");
    }

    [Fact]
    public async Task Subscribe_PendingAddressAgain_ShouldNotResendVerificationMail()
    {
        var first = await SubscribeAsync("pending@example.com");
        var second = await SubscribeAsync("pending@example.com");

        first.StatusCode.Should().Be(HttpStatusCode.OK);
        second.StatusCode.Should().Be(HttpStatusCode.OK);
        _emails.Recipients.Should().Equal("pending@example.com");
    }

    [Fact]
    public async Task Subscribe_WithFilledHoneypot_ShouldSendNoMail()
    {
        var (token, difficulty) = await GetChallengeAsync();

        var response = await _client.PostAsJsonAsync("/api/newsletter/subscribe", new
        {
            email = "honeypot@example.com",
            website = "http://spam.example",
            challengeToken = token,
            challengeNonce = Solve(token, difficulty)
        });

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        _emails.Recipients.Should().BeEmpty();
    }

    private async Task<HttpResponseMessage> SubscribeAsync(string email)
    {
        var (token, difficulty) = await GetChallengeAsync();
        return await _client.PostAsJsonAsync("/api/newsletter/subscribe",
            new { email, challengeToken = token, challengeNonce = Solve(token, difficulty) });
    }

    private async Task<(string Token, int Difficulty)> GetChallengeAsync()
    {
        var json = await _client.GetFromJsonAsync<JsonElement>("/api/newsletter/challenge");
        return (json.GetProperty("token").GetString()!, json.GetProperty("difficulty").GetInt32());
    }

    // Same algorithm as solveChallenge() in the frontend's newsletterApi.ts.
    private static string Solve(string token, int difficulty)
    {
        for (var nonce = 0; ; nonce++)
        {
            var hash = SHA256.HashData(Encoding.UTF8.GetBytes($"{token}:{nonce}"));
            var zeroBits = 0;
            foreach (var b in hash)
            {
                if (b == 0)
                {
                    zeroBits += 8;
                    continue;
                }

                zeroBits += System.Numerics.BitOperations.LeadingZeroCount((uint)b) - 24;
                break;
            }

            if (zeroBits >= difficulty)
            {
                return nonce.ToString();
            }
        }
    }

    private sealed class RecordingEmailService : IEmailService
    {
        public List<string> Recipients { get; } = new();

        public Task SendEmailAsync(string toEmail, string subject, string htmlBody, string? replyToEmail = null, CancellationToken cancellationToken = default)
        {
            Recipients.Add(toEmail);
            return Task.CompletedTask;
        }

        public Task SendContactMessageNotificationAsync(string senderName, string senderEmail, string? senderPhone, string subject, string message, CancellationToken cancellationToken = default) => Task.CompletedTask;

        public Task SendVolunteerSubmissionNotificationAsync(string fullName, string email, string? phoneNumber, string message, CancellationToken cancellationToken = default) => Task.CompletedTask;

        public Task<bool> IsRateLimitExceededAsync(string identifier) => Task.FromResult(false);

        public string SanitizeHtmlContent(string content) => content;
    }
}
