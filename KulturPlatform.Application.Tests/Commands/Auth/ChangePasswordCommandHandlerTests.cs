using FluentAssertions;
using KulturPlatform.Application.Commands.Auth;
using KulturPlatform.Application.Interfaces.Admin;
using KulturPlatform.Domain.Commons.Aggregates;
using KulturPlatform.Domain.Commons.Constants;
using KulturPlatform.Domain.Commons.ValueObjects;
using Moq;
using Xunit;

namespace KulturPlatform.Application.Tests.Commands.Auth;

public class ChangePasswordCommandHandlerTests
{
    private readonly Mock<IAdminRepository> _adminRepositoryMock;
    private readonly ChangePasswordCommandHandler _handler;

    public ChangePasswordCommandHandlerTests()
    {
        _adminRepositoryMock = new Mock<IAdminRepository>();
        _handler = new ChangePasswordCommandHandler(
            _adminRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_WithValidData_ShouldChangePassword()
    {
        // Arrange
        var adminId = Guid.NewGuid();
        var currentPassword = "CurrentPassword123!";
        var newPassword = "NewPassword123!";
        
        var admin = Admin.CreateNew(
            Email.Create("admin@test.com"),
            Password.Create(currentPassword),
            new Name("Test Admin"),
            Roles.SystemAdmin);

        var command = new ChangePasswordCommand(adminId, currentPassword, newPassword);

        _adminRepositoryMock
            .Setup(x => x.GetByIdAsync(adminId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(admin);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        admin.Password.Verify(newPassword).Should().BeTrue();
        admin.Password.Verify(currentPassword).Should().BeFalse();
    }

    [Fact]
    public async Task Handle_WithNonExistentAdmin_ShouldThrowKeyNotFoundException()
    {
        // Arrange
        var adminId = Guid.NewGuid();
        var command = new ChangePasswordCommand(adminId, "Current123!", "New123!");

        _adminRepositoryMock
            .Setup(x => x.GetByIdAsync(adminId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Admin?)null);

        // Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"Admin with Id {adminId} not found.");
    }

    [Fact]
    public async Task Handle_WithIncorrectCurrentPassword_ShouldThrowUnauthorizedException()
    {
        // Arrange
        var adminId = Guid.NewGuid();
        var admin = Admin.CreateNew(
            Email.Create("admin@test.com"),
            Password.Create("CorrectPassword123!"),
            new Name("Test Admin"),
            Roles.SystemAdmin);

        var command = new ChangePasswordCommand(adminId, "WrongPassword123!", "NewPassword123!");

        _adminRepositoryMock
            .Setup(x => x.GetByIdAsync(adminId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(admin);

        // Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<UnauthorizedAccessException>()
            .WithMessage("Current password is incorrect.");
    }

    [Fact]
    public async Task Handle_ShouldCallSaveChangesAsync()
    {
        // Arrange
        var adminId = Guid.NewGuid();
        var currentPassword = "CurrentPassword123!";
        var newPassword = "NewPassword123!";
        
        var admin = Admin.CreateNew(
            Email.Create("admin@test.com"),
            Password.Create(currentPassword),
            new Name("Test Admin"),
            Roles.SystemAdmin);

        var command = new ChangePasswordCommand(adminId, currentPassword, newPassword);

        _adminRepositoryMock
            .Setup(x => x.GetByIdAsync(adminId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(admin);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _adminRepositoryMock.Verify(
            x => x.Update(admin, It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldNotCallSaveChanges_WhenPasswordIncorrect()
    {
        // Arrange
        var adminId = Guid.NewGuid();
        var admin = Admin.CreateNew(
            Email.Create("admin@test.com"),
            Password.Create("CorrectPassword123!"),
            new Name("Test Admin"),
            Roles.SystemAdmin);

        var command = new ChangePasswordCommand(adminId, "WrongPassword123!", "NewPassword123!");

        _adminRepositoryMock
            .Setup(x => x.GetByIdAsync(adminId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(admin);

        // Act
        try
        {
            await _handler.Handle(command, CancellationToken.None);
        }
        catch (UnauthorizedAccessException)
        {
            // Expected exception
        }

        // Assert
        _adminRepositoryMock.Verify(
            x => x.Update(It.IsAny<Admin>(), It.IsAny<CancellationToken>()),
            Times.Never);
    }
}
