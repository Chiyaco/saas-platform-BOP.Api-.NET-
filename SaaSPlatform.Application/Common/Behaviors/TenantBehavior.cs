using MediatR;
using SaaSPlatform.Application.Abstractions.Interfaces;

namespace SaaSPlatform.Application.Common.Behaviors;

public class TenantBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly ITenantProvider _tenantProvider;

    public TenantBehavior(ITenantProvider tenantProvider)
    {
        _tenantProvider = tenantProvider;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_tenantProvider.TenantId == Guid.Empty)
            throw new UnauthorizedAccessException("Tenant is required");

        return await next();
    }
}