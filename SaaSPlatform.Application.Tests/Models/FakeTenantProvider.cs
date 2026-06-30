using SaaSPlatform.Application.Abstractions.Interfaces;

namespace SaaSPlatform.Application.Tests.Models;

public class FakeTenantProvider : ITenantProvider
{
    public Guid TenantId { get; set; } = Guid.Parse("11111111 - 1111 - 1111 - 1111 - 111111111111");
}
