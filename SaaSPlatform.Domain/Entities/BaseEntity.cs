namespace SaaSPlatform.Domain.Entities;

public class BaseEntity
{
    public Guid Id { get; protected set; }

    public Guid TenantId { get; set; }

    public DateTime CreatedDateTime { get; protected set; }

    public DateTime UpdatedDateTime { get; protected set; }

    public bool IsDeleted { get; private set; }

    public void Delete()
    {
        IsDeleted = true;
    }

    protected void Touch()
    {
        UpdatedDateTime = DateTime.UtcNow;
    }
}
