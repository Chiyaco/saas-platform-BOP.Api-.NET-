using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SaaSPlatform.Application.Abstractions.Interfaces;
using SaaSPlatform.Application.Common.Behaviors;
using System.Reflection;

namespace SaaSPlatform.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TenantBehavior<,>));


        return services;
    }
}