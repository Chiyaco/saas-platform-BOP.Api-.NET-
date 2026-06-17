using FluentAssertions;
using SaaSPlatform.Domain.Entities.Product;

namespace SaaSPlatform.Domain.Tests.Entities.Products;

public class ProductTests
{
    [Fact]
    public void CreateProduct_Should_Set_Properties_Correctly()
    {
        // Arrange
        var productName = "Test";
        var price = 18m;

        // Act
        var product = new Product(productName, price);

        // Assert
        product.Name.Should().Be(productName);
        product.Price.Should().Be(price);
    }


    [Theory]
    [InlineData(null)]
    [InlineData(" ")]
    [InlineData("")]
    public void Constructor_Should_Throw_When_ProductName_Is_Invalid(string? name)
    {
        // Arrange 
        decimal price = 100m;

        // Act
        Action act = () =>
        {
            var product = new Product(name, price);
        };

        // Assert
        act.Should().
            Throw<ArgumentException>()
             .WithMessage("*Name is required.*");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Constructure_Should_Throw_When_Price_Is_Invalid(decimal price)
    {
        // Arrange 
        var name = "Test";

        // Act
        Action act = () =>
        {
            var product = new Product(name, price);
        };

        // Assert
        act.Should()
            .Throw<ArgumentException>()
            .WithMessage("*Price must be greater than zero.*");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void ChangePrice_Should_Throw_When_Price_Is_Invalid(decimal price)
    {
        // Arrange 
        var product = new Product("Test", 100m);

        // Act 

        Action act = () =>
        {
            product.ChangePrice(price);
        };

        // Assert 
        act.Should()
            .Throw<ArgumentException>()
            .WithMessage("*Price must be greater than zero.*");
    }
}
