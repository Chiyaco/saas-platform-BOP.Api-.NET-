using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SaaSPlatform.Application.Abstractions.Interfaces;
using SaaSPlatform.Infrastructure.Persistence;
using SaaSPlatform.Infrastructure.Persistence.Authentication;

namespace SaaSPlatform.Application.Tests.Models;

public class DbContextFixture
{
    public ApplicationDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var serviceProvider = new ServiceCollection()
            .AddScoped<ITenantProvider, FakeTenantProvider>()
            .BuildServiceProvider();

        return new ApplicationDbContext(options, serviceProvider);
    }
}