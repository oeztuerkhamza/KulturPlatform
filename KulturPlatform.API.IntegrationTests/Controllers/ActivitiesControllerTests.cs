using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using KulturPlatform.Application.Dtos.Common;
using Xunit;

namespace KulturPlatform.API.IntegrationTests.Controllers;

public class ActivitiesControllerTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory _factory;

    public ActivitiesControllerTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetAll_ShouldReturnOk()
    {
        // Act
        var response = await _client.GetAsync("/api/activities");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetAll_ShouldReturnPagedResult()
    {
        // Act
        var response = await _client.GetAsync("/api/activities?pageNumber=1&pageSize=10");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var content = await response.Content.ReadAsStringAsync();
        content.Should().Contain("pageNumber");
        content.Should().Contain("pageSize");
        content.Should().Contain("totalCount");
        content.Should().Contain("items");
    }

    [Fact]
    public async Task GetAll_WithInvalidPageNumber_ShouldReturnOkWithDefaultPage()
    {
        // Act - page 0 should default to page 1
        var response = await _client.GetAsync("/api/activities?pageNumber=0&pageSize=10");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetAll_WithLargePageSize_ShouldCapAt100()
    {
        // Act - page size 1000 should cap at 100
        var response = await _client.GetAsync("/api/activities?pageNumber=1&pageSize=1000");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().Contain("\"pageSize\":100");
    }

    [Fact]
    public async Task GetAll_ShouldIncludeCorrelationIdInResponse()
    {
        // Arrange
        var correlationId = "test-correlation-123";
        _client.DefaultRequestHeaders.Add("X-Correlation-ID", correlationId);

        // Act
        var response = await _client.GetAsync("/api/activities");

        // Assert
        response.Headers.Should().ContainKey("X-Correlation-ID");
        response.Headers.GetValues("X-Correlation-ID").First().Should().Be(correlationId);
    }

    [Fact]
    public async Task GetAll_WithoutCorrelationId_ShouldGenerateOne()
    {
        // Act
        var response = await _client.GetAsync("/api/activities");

        // Assert
        response.Headers.Should().ContainKey("X-Correlation-ID");
        var correlationId = response.Headers.GetValues("X-Correlation-ID").First();
        correlationId.Should().NotBeNullOrEmpty();
        Guid.TryParse(correlationId, out _).Should().BeTrue();
    }

    [Fact]
    public async Task GetUpcoming_ShouldReturnOk()
    {
        // Act
        var response = await _client.GetAsync("/api/activities/upcoming");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetById_WithInvalidId_ShouldReturnNotFound()
    {
        // Arrange
        var invalidId = Guid.NewGuid();

        // Act
        var response = await _client.GetAsync($"/api/activities/{invalidId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Create_WithoutAuthentication_ShouldReturnUnauthorized()
    {
        // Arrange
        var createDto = new
        {
            titleTr = "Test Activity",
            titleDe = "Test Aktivität",
            descriptionTr = "Description",
            descriptionDe = "Beschreibung",
            date = DateTime.UtcNow.AddDays(7).ToString("yyyy-MM-dd"),
            category = "Cultural",
            isActive = true,
            address = new
            {
                street = "Test Street",
                houseNo = "123",
                zipCode = "12345",
                city = "Test City",
                state = "Test State",
                country = "Germany"
            }
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/activities", createDto);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Delete_WithoutAuthentication_ShouldReturnUnauthorized()
    {
        // Arrange
        var activityId = Guid.NewGuid();

        // Act
        var response = await _client.DeleteAsync($"/api/activities/{activityId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}
