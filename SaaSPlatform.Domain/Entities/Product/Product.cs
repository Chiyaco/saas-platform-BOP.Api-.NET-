namespace SaaSPlatform.Domain.Entities.Product;

public class Product : BaseEntity
{
    public string Name { get; private set; }

    public decimal Price { get; private set; }

    private Product() { }

    public Product(string? name, decimal price)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(nameof(name), "Name is required.");

        if (price <= 0)
            throw new ArgumentException(nameof(price), "Price must be greater than zero.");

        Id = Guid.NewGuid();
        Name = name;
        CreatedDateTime = DateTime.Now;

        SetPrice(price);
    }

    public void ChangePrice(decimal price)
    {
        SetPrice(price);

        Touch();
    }

    private void SetPrice(decimal price)
    {
        if (price <= 0)
            throw new ArgumentException(nameof(price), "Price must be greater than zero.");

        Price = price;
    }
}