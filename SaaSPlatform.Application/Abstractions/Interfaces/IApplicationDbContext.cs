using Microsoft.EntityFrameworkCore;
using SaaSPlatform.Domain.Entities.Customer;
using SaaSPlatform.Domain.Entities.Order;
using SaaSPlatform.Domain.Entities.Product;
using SaaSPlatform.Domain.Entities.Tenants;
using SaaSPlatform.Domain.Entities.Users;

namespace SaaSPlatform.Application.Abstractions.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Customer> Customers { get; }
    DbSet<Order> Orders { get; }
    DbSet<Product> Products { get; }
    public DbSet<User> Users { get; }
    public DbSet<Tenant> Tenants { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}