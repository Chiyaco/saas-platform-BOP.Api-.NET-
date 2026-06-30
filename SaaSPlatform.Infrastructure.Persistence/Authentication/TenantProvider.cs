using Microsoft.AspNetCore.Http;
using SaaSPlatform.Application.Abstractions.Interfaces;

namespace SaaSPlatform.Infrastructure.Persistence.Authentication;

public class TenantProvider : ITenantProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TenantProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    //public Guid TenantId
    //{
    //    get
    //    {
    //        var tenantId = _httpContextAccessor.HttpContext?
    //            .Request.Headers["tenant_id"]
    //            .FirstOrDefault();

    //        if (string.IsNullOrWhiteSpace(tenantId))
    //            throw new UnauthorizedAccessException("Tenant header is missing.");

    //        return Guid.Parse(tenantId);
    //    }
    //}

    public Guid TenantId => Guid.Parse("11111111-1111-1111-1111-111111111111");
}