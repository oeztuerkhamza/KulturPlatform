using FluentAssertions;
using KulturPlatform.Application.Commands.Auth;
using KulturPlatform.Application.Interfaces.Admin;
using KulturPlatform.Application.Interfaces.Auth;
using KulturPlatform.Domain.Commons.Aggregates;
using KulturPlatform.Domain.Commons.Constants;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;
using Moq;
using Xunit;

namespace KulturPlatform.Application.Tests.Commands.Auth;

public class LoginCommandHandlerTests
{
    private readonly Mock<IAdminRepository> _adminReadRepositoryMock;
    private readonly Mock<ITokenService> _tokenServiceMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly LoginCommandHandler _handler;

    public LoginCommandHandlerTests()
    {
        _adminReadRepositoryMock = new Mock<IAdminRepository>();
        _tokenServiceMock = new Mock<ITokenService>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new LoginCommandHandler(
            _adminReadRepositoryMock.Object,
            _tokenServiceMock.Object,
            _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_WithValidCredentials_ShouldReturnToken()
    {
        // Arrange
        var email = "admin@test.com";
        var password = "Password123!";
        var admin = Admin.CreateNew(
            Email.Create(email),
            Password.Create(password),
            new Name("Test Admin"),
            Roles.SystemAdmin);
        var expectedToken = "jwt-token-123";

        var command = new LoginCommand(email, password);

        _adminReadRepositoryMock
            .Setup(x => x.GetByEmailAsync(email, It.IsAny<CancellationToken>()))
            .ReturnsAsync(admin);

        _tokenServiceMock
            .Setup(x => x.GenerateToken(admin.Id, admin.Email.Value, admin.Role))
            .Returns(expectedToken);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Token.Should().Be(expectedToken);
        result.Email.Should().Be(email);
        result.Role.Should().Be(Roles.SystemAdmin);
    }

    [Fact]
    public async Task Handle_WithInvalidEmail_ShouldThrowUnauthorizedException()
    {
        // Arrange
        var command = new LoginCommand("nonexistent@test.com", "Password123!");

        _adminReadRepositoryMock
            .Setup(x => x.GetByEmailAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Admin?)null);

        // Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<UnauthorizedAccessException>()
            .WithMessage("Invalid email or password.");
    }

    [Fact]
    public async Task Handle_WithInactiveAdmin_ShouldThrowUnauthorizedException()
    {
        // Arrange
        var email = "admin@test.com";
        var password = "Password123!";
        var admin = Admin.CreateNew(
            Email.Create(email),
            Password.Create(password),
            new Name("Test Admin"),
            Roles.SystemAdmin);
        admin.Deactivate();

        var command = new LoginCommand(email, password);

        _adminReadRepositoryMock
            .Setup(x => x.GetByEmailAsync(email, It.IsAny<CancellationToken>()))
            .ReturnsAsync(admin);

        // Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<UnauthorizedAccessException>()
            .WithMessage("Account is deactivated.");
    }

    [Fact]
    public async Task Handle_WithIncorrectPassword_ShouldThrowUnauthorizedException()
    {
        // Arrange
        var email = "admin@test.com";
        var admin = Admin.CreateNew(
            Email.Create(email),
            Password.Create("CorrectPassword123!"),
            new Name("Test Admin"),
            Roles.SystemAdmin);

        var command = new LoginCommand(email, "WrongPassword123!");

        _adminReadRepositoryMock
            .Setup(x => x.GetByEmailAsync(email, It.IsAny<CancellationToken>()))
            .ReturnsAsync(admin);

        // Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<UnauthorizedAccessException>()
            .WithMessage("Invalid email or password.");
    }

    [Fact]
    public async Task Handle_ShouldCallRepositoryOnce()
    {
        // Arrange
        var email = "admin@test.com";
        var password = "Password123!";
        var admin = Admin.CreateNew(
            Email.Create(email),
            Password.Create(password),
            new Name("Test Admin"),
            Roles.SystemAdmin);

        var command = new LoginCommand(email, password);

        _adminReadRepositoryMock
            .Setup(x => x.GetByEmailAsync(email, It.IsAny<CancellationToken>()))
            .ReturnsAsync(admin);

        _tokenServiceMock
            .Setup(x => x.GenerateToken(admin.Id, admin.Email.Value, admin.Role))
            .Returns("token");

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _adminReadRepositoryMock.Verify(
            x => x.GetByEmailAsync(email, It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldCallTokenServiceOnce()
    {
        // Arrange
        var email = "admin@test.com";
        var password = "Password123!";
        var admin = Admin.CreateNew(
            Email.Create(email),
            Password.Create(password),
            new Name("Test Admin"),
            Roles.SystemAdmin);

        var command = new LoginCommand(email, password);

        _adminReadRepositoryMock
            .Setup(x => x.GetByEmailAsync(email, It.IsAny<CancellationToken>()))
            .ReturnsAsync(admin);

        _tokenServiceMock
            .Setup(x => x.GenerateToken(admin.Id, admin.Email.Value, admin.Role))
            .Returns("token");

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _tokenServiceMock.Verify(
            x => x.GenerateToken(admin.Id, admin.Email.Value, admin.Role),
            Times.Once);
    }
}
