using FluentAssertions;
using KulturPlatform.Domain.Commons.ValueObjects;
using Xunit;

namespace KulturPlatform.Domain.Tests.ValueObjects;

public class TitleTests
{
    [Theory]
    [InlineData("Valid Title")]
    [InlineData("Test")]
    [InlineData("A")]
    public void Create_WithValidTitle_ShouldSucceed(string validTitle)
    {
        // Act
        var title = Title.Create(validTitle);

        // Assert
        title.Should().NotBeNull();
        title.Value.Should().Be(validTitle);
    }

    [Fact]
    public void Constructor_WithValidValue_ShouldSucceed()
    {
        // Arrange
        var value = "Test Title";

        // Act
        var title = new Title(value);

        // Assert
        title.Value.Should().Be(value);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Create_WithInvalidTitle_ShouldThrowArgumentException(string invalidTitle)
    {
        // Act
        Action act = () => Title.Create(invalidTitle);

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Constructor_WithInvalidValue_ShouldThrowArgumentException(string invalidValue)
    {
        // Act
        Action act = () => new Title(invalidValue);

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Equals_WithSameTitle_ShouldReturnTrue()
    {
        // Arrange
        var title1 = Title.Create("Same Title");
        var title2 = Title.Create("Same Title");

        // Act & Assert
        title1.Should().Be(title2);
        (title1 == title2).Should().BeTrue();
    }

    [Fact]
    public void Equals_WithDifferentTitle_ShouldReturnFalse()
    {
        // Arrange
        var title1 = Title.Create("Title One");
        var title2 = Title.Create("Title Two");

        // Act & Assert
        title1.Should().NotBe(title2);
        (title1 != title2).Should().BeTrue();
    }

    [Fact]
    public void GetHashCode_WithSameTitle_ShouldReturnSameHashCode()
    {
        // Arrange
        var title1 = Title.Create("Test Title");
        var title2 = Title.Create("Test Title");

        // Act & Assert
        title1.GetHashCode().Should().Be(title2.GetHashCode());
    }

    [Fact]
    public void ToString_ShouldReturnTitleValue()
    {
        // Arrange
        var titleValue = "My Title";
        var title = Title.Create(titleValue);

        // Act
        var result = title.ToString();

        // Assert
        result.Should().Be(titleValue);
    }

}
