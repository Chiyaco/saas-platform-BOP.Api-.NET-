namespace SaaSPlatform.Domain.Entities.Tenants;

public class Tenant : BaseEntity
{
    public string Name { get; private set; }

    public string Code { get; private set; }

    private Tenant()
    {

    }

    public Tenant(string name, string code)
    {
        SetName(name);
        SetCode(code);

        Id = Guid.NewGuid();

        CreatedDateTime = DateTime.UtcNow;
    }

    public void Update(string name, string code)
    {
        SetName(name);
        SetCode(code);

        Touch();
    }

    private void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(nameof(name), "The tenant name is required!");

        Name = name;
    }

    private void SetCode(string code)
    {
        if (string.IsNullOrWhiteSpace(code))
            throw new ArgumentNullException(nameof(code), "The tenant code is required!");
        Code = code.ToUpper();
    }
}