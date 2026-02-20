using FluentAssertions;
using KulturPlatform.Domain.Commons.ValueObjects;
using Xunit;

namespace KulturPlatform.Domain.Tests.ValueObjects;

public class PasswordTests
{
    [Fact]
    public void Create_WithValidPassword_ShouldHashPassword()
    {
        // Arrange
        var plainPassword = "SecurePassword123!";

        // Act
        var password = Password.Create(plainPassword);

        // Assert
        password.Should().NotBeNull();
        password.Value.Should().NotBeNullOrEmpty();
        password.Value.Should().NotBe(plainPassword);
        password.Value.Should().StartWith("$2");
    }

    [Fact]
    public void Verify_WithCorrectPassword_ShouldReturnTrue()
    {
        // Arrange
        var plainPassword = "MyPassword123!";
        var password = Password.Create(plainPassword);

        // Act
        var result = password.Verify(plainPassword);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void Verify_WithIncorrectPassword_ShouldReturnFalse()
    {
        // Arrange
        var password = Password.Create("CorrectPassword123!");

        // Act
        var result = password.Verify("WrongPassword123!");

        // Assert
        result.Should().BeFalse();
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Create_WithInvalidPassword_ShouldThrowArgumentException(string invalidPassword)
    {
        // Act
        Action act = () => Password.Create(invalidPassword);

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void FromHash_WithValidHash_ShouldCreatePassword()
    {
        // Arrange
        var plainPassword = "TestPassword123!";
        var originalPassword = Password.Create(plainPassword);
        var hash = originalPassword.Value;

        // Act
        var restoredPassword = Password.FromHash(hash);

        // Assert
        restoredPassword.Value.Should().Be(hash);
        restoredPassword.Verify(plainPassword).Should().BeTrue();
    }

    [Fact]
    public void DifferentPasswords_ShouldHaveDifferentHashes()
    {
        // Arrange & Act
        var password1 = Password.Create("Password1");
        var password2 = Password.Create("Password2");

        // Assert
        password1.Value.Should().NotBe(password2.Value);
    }

    [Fact]
    public void SamePassword_CreatedTwice_ShouldHaveDifferentHashes()
    {
        // Arrange
        var plainPassword = "SamePassword123!";

        // Act
        var password1 = Password.Create(plainPassword);
        var password2 = Password.Create(plainPassword);

        // Assert - BCrypt uses salts, so same password creates different hashes
        password1.Value.Should().NotBe(password2.Value);
        password1.Verify(plainPassword).Should().BeTrue();
        password2.Verify(plainPassword).Should().BeTrue();
    }
}
