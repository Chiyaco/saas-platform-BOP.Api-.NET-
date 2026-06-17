using FluentAssertions;
using FluentValidation.TestHelper;
using SaaSPlatform.Application.Features.Products.Queries;
using SaaSPlatform.Application.Tests.Models;
using SaaSPlatform.Domain.Entities.Product;

namespace SaaSPlatform.Application.Tests.Products.Queries;

public class ProductQueryTests
{
    private readonly DbContextFixture _dbContextFixture = new();
    private readonly GetProductByIdQueryValidator _validator = new();

    [Fact]
    public async Task GetProductQueryValidator_Should_Return_Value()
    {
        // Arrange

        var query = new GetProductByIdQuery(Guid.Empty);

        // Act
        var result = await _validator.TestValidateAsync(query);

        // Assert

        result.ShouldHaveValidationErrorFor(x => x.Id);
    }

    [Fact]
    public async Task GetProductQuery_Should_Return_Value()
    {
        // Arrange

        await using var dbContext = _dbContextFixture.CreateDbContext();

       var product = await SeedData.CreateRowDataAsync<Product>(dbContext,
            () => new Product(
                "Laptop",
                1000m
            ));
        
       var query = new GetProductByIdQuery(product.Id);
       var queryHandler = new GetProductByIdQueryHandler(dbContext);

       // Act 

       var result = await queryHandler.Handle(query, CancellationToken.None);

       // Assert
       result.Should().NotBeNull();
       result.IsSuccess.Should().BeTrue();
       result.Value?.Name.Should().Be("Laptop");
       result.Value?.Price.Should().Be(1000m);
    }
}
