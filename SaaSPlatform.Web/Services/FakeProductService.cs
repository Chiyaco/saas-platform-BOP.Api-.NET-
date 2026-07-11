using SaaSPlatform.Web.Models.DTOs;

namespace SaaSPlatform.Web.Services;

public class FakeProductService
{
    private readonly List<ProductDto> products =
    [
        new()
        {
            Id = 1,
            Name = "Laptop",
            Category = "Computer",
            Price = 1200,
            Stock = 15
        },

        new()
        {
            Id = 2,
            Name = "Phone",
            Category = "Mobile",
            Price = 800,
            Stock = 30
        },

        new()
        {
            Id = 3,
            Name = "Keyboard",
            Category = "Accessories",
            Price = 80,
            Stock = 100
        },

        new()
        {
            Id = 4,
            Name = "Monitor",
            Category = "Display",
            Price = 350,
            Stock = 20
        },

        new()
        {
            Id = 5,
            Name = "Mouse",
            Category = "Accessories",
            Price = 40,
            Stock = 200
        }
    ];

    public Task<List<ProductDto>> GetProducts()
    {
        return Task.FromResult(products);
    }

    public Task<ProductDto?> GetProduct(int id)
    {
        return Task.FromResult(
            products.FirstOrDefault(x => x.Id == id));
    }

    public Task Add(ProductDto product)
    {
        product.Id = products.Max(x => x.Id) + 1;

        products.Add(product);

        return Task.CompletedTask;
    }

    public Task Update(ProductDto product)
    {
        var old =
            products.First(x => x.Id == product.Id);

        old.Name = product.Name;
        old.Category = product.Category;
        old.Price = product.Price;
        old.Stock = product.Stock;

        return Task.CompletedTask;
    }

    public Task Delete(int id)
    {
        var item =
            products.First(x => x.Id == id);

        products.Remove(item);

        return Task.CompletedTask;
    }
}
