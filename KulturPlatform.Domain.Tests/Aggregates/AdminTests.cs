using FluentAssertions;
using KulturPlatform.Domain.Commons.Aggregates;
using KulturPlatform.Domain.Commons.Constants;
using KulturPlatform.Domain.Commons.ValueObjects;
using Xunit;

namespace KulturPlatform.Domain.Tests.Aggregates;

public class AdminTests
{
    [Fact]
    public void CreateNew_WithValidData_ShouldCreateAdmin()
    {
        // Arrange
        var email = Email.Create("admin@test.com");
        var password = Password.Create("Password123!");
        var name = new Name("John Doe");
        var role = Roles.SystemAdmin;

        // Act
        var admin = Admin.CreateNew(email, password, name, role);

        // Assert
        admin.Should().NotBeNull();
        admin.Id.Should().NotBeEmpty();
        admin.Email.Should().Be(email);
        admin.Password.Should().Be(password);
        admin.Name.Should().Be(name);
        admin.Role.Should().Be(role);
        admin.IsActive.Should().BeTrue();
        admin.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Theory]
    [InlineData(Roles.SystemAdmin)]
    [InlineData(Roles.UserAdmin)]
    [InlineData(Roles.User)]
    public void CreateNew_WithDifferentRoles_ShouldCreateAdminWithCorrectRole(string role)
    {
        // Arrange
        var email = Email.Create("admin@test.com");
        var password = Password.Create("Password123!");
        var name = new Name("John Doe");

        // Act
        var admin = Admin.CreateNew(email, password, name, role);

        // Assert
        admin.Role.Should().Be(role);
    }

    [Fact]
    public void Activate_WhenInactive_ShouldActivateAdmin()
    {
        // Arrange
        var email = Email.Create("admin@test.com");
        var password = Password.Create("Password123!");
        var name = new Name("John Doe");
        var admin = Admin.CreateNew(email, password, name, Roles.User);
        admin.Deactivate();

        // Act
        admin.Activate();

        // Assert
        admin.IsActive.Should().BeTrue();
    }

    [Fact]
    public void Deactivate_WhenActive_ShouldDeactivateAdmin()
    {
        // Arrange
        var email = Email.Create("admin@test.com");
        var password = Password.Create("Password123!");
        var name = new Name("John Doe");
        var admin = Admin.CreateNew(email, password, name, Roles.User);

        // Act
        admin.Deactivate();

        // Assert
        admin.IsActive.Should().BeFalse();
    }

    [Fact]
    public void UpdatePassword_WithNewPassword_ShouldUpdatePassword()
    {
        // Arrange
        var email = Email.Create("admin@test.com");
        var oldPassword = Password.Create("OldPassword123!");
        var name = new Name("John Doe");
        var admin = Admin.CreateNew(email, oldPassword, name, Roles.User);
        var newPassword = Password.Create("NewPassword123!");

        // Act
        admin.UpdatePassword(newPassword);

        // Assert
        admin.Password.Should().Be(newPassword);
        admin.Password.Should().NotBe(oldPassword);
    }

    [Fact]
    public void UpdateRole_WithValidRole_ShouldUpdateRole()
    {
        // Arrange
        var email = Email.Create("admin@test.com");
        var password = Password.Create("Password123!");
        var name = new Name("John Doe");
        var admin = Admin.CreateNew(email, password, name, Roles.User);

        // Act
        admin.UpdateRole(Roles.SystemAdmin);

        // Assert
        admin.Role.Should().Be(Roles.SystemAdmin);
    }

    [Fact]
    public void UpdateRole_WithInvalidRole_ShouldThrowArgumentException()
    {
        // Arrange
        var email = Email.Create("admin@test.com");
        var password = Password.Create("Password123!");
        var name = new Name("John Doe");
        var admin = Admin.CreateNew(email, password, name, Roles.User);

        // Act & Assert
        var action = () => admin.UpdateRole("InvalidRole");
        action.Should().Throw<ArgumentException>()
            .WithMessage("Invalid role: InvalidRole*");
    }

    [Fact]
    public void UpdateEmail_WithNewEmail_ShouldUpdateEmail()
    {
        // Arrange
        var email = Email.Create("admin@test.com");
        var password = Password.Create("Password123!");
        var name = new Name("John Doe");
        var admin = Admin.CreateNew(email, password, name, Roles.User);
        var newEmail = Email.Create("newemail@test.com");

        // Act
        admin.UpdateEmail(newEmail);

        // Assert
        admin.Email.Should().Be(newEmail);
    }

    [Fact]
    public void UpdateLastLogin_ShouldUpdateLastLoginTime()
    {
        // Arrange
        var email = Email.Create("admin@test.com");
        var password = Password.Create("Password123!");
        var name = new Name("John Doe");
        var admin = Admin.CreateNew(email, password, name, Roles.User);

        // Act
        admin.UpdateLastLogin();

        // Assert
        admin.LastLoginAt.Should().NotBeNull();
        admin.LastLoginAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public void IsSystemAdmin_WhenSystemAdmin_ShouldReturnTrue()
    {
        // Arrange
        var email = Email.Create("admin@test.com");
        var password = Password.Create("Password123!");
        var name = new Name("John Doe");
        var admin = Admin.CreateNew(email, password, name, Roles.SystemAdmin);

        // Act & Assert
        admin.IsSystemAdmin().Should().BeTrue();
    }

    [Fact]
    public void IsSystemAdmin_WhenNotSystemAdmin_ShouldReturnFalse()
    {
        // Arrange
        var email = Email.Create("admin@test.com");
        var password = Password.Create("Password123!");
        var name = new Name("John Doe");
        var admin = Admin.CreateNew(email, password, name, Roles.User);

        // Act & Assert
        admin.IsSystemAdmin().Should().BeFalse();
    }
}
