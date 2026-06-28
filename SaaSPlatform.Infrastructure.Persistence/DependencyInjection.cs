using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SaaSPlatform.Application.Abstractions.Interfaces;
using SaaSPlatform.Infrastructure.Persistence.Authentication;
using Microsoft.AspNetCore.Http;

namespace SaaSPlatform.Infrastructure.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructurePersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("Default");

            options.UseMySql(
                connectionString,
                ServerVersion.AutoDetect(connectionString));
        });

        services.AddScoped<IApplicationDbContext>(provider =>
            provider.GetRequiredService<ApplicationDbContext>());


        services.AddScoped<ITenantProvider, TenantProvider>();

        return services;
    }
}
