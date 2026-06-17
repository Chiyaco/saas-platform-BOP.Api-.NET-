using FluentAssertions;
using SaaSPlatform.Application.Features.Products.Commands;
using SaaSPlatform.Application.Tests.Models;
using FluentValidation.TestHelper;
using SaaSPlatform.Domain.Entities.Product;

namespace SaaSPlatform.Application.Tests.Products.Commands;

public class ProductsCommandTests
{
    private readonly DbContextFixture _contextFixture = new();
    private readonly CreateProductCommandValidator _validator = new();

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void CreateProductCommandValidator_Should_Have_Error_When_Name_Is_Null(string? productName)
    {
        // Arrange 

        var command = new CreateProductCommand(productName, 100m);

        // Act 

        var result = _validator.TestValidate(command);

        // Assert

        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void CreateProductCommandValidator_Should_Have_Error_When_Price_Is_Invalide(decimal price)
    {
        // Arranhge 

        var command = new CreateProductCommand("Laptop", price);

        // Act 

        var result = _validator.TestValidate(command);

        // Assert

        result.ShouldHaveValidationErrorFor(x => x.Price);
    }

    [Fact]
    public async Task CreateProductCommand_Should_Create_Product_When_It_Is_Not_Exists()
    {

        // Arrange

        await using var dbContext = _contextFixture.CreateDbContext();

        var command = new CreateProductCommand("Laptop", 1000m);
        var commandHandler = new CreateProductCommandHandler(dbContext);

        // Act 

        var result = await commandHandler.Handle(command, CancellationToken.None);

        // Assert

        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task CreateProductCommand_Should_Return_Failure_When_Product_Exists()
    {
        // Arrange

        await using var dbContext = _contextFixture.CreateDbContext();
        var product = new Product("Laptop", 1000m);

        dbContext.Products.Add(product);
        await dbContext.SaveChangesAsync();

        var command = new CreateProductCommand("Laptop", 2000m);
        var commandHandler = new CreateProductCommandHandler(dbContext);

        // Act

        var result = await commandHandler.Handle(command, CancellationToken.None);

        // Assert

        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be("The entered Product is exists");
    }
}
