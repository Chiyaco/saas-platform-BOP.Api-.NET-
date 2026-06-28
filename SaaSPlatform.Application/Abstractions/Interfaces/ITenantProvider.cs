namespace SaaSPlatform.Application.Abstractions.Interfaces;

public interface ITenantProvider
{
    Guid TenantId { get; }
}
