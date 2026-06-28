using SaaSPlatform.Application.Abstractions.Interfaces;

namespace SaaSPlatform.Application.Tests.Models;

public class FakeTenantProvider : ITenantProvider
{
    public Guid TenantId { get; set; }
}
