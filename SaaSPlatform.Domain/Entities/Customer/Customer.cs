namespace SaaSPlatform.Domain.Entities.Customer;

public class Customer : BaseEntity
{

    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public Email Email { get; private set; }

    private Customer() { }

    public Customer(string firstName, string lastName, Email email)
    {
        SetFirstName(firstName);
        SetLastName(lastName);


        Id = Guid.NewGuid();
        Email = email;

        CreatedDateTime = DateTime.UtcNow;
    }

    public void UpdateProfile(string firstName, string lastName, Email email)
    {
        SetFirstName(firstName);
        SetLastName(lastName);
        Email = email;
        Touch();
    }

    public void ChangeEmailAddress(Email email)
    {
        Email = email;
        Touch();
    }

    private void SetFirstName(string firstName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentNullException(nameof(firstName), "first name should not be null");

        FirstName = firstName;
    }

    private void SetLastName(string lastName)
    {
        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentNullException(nameof(lastName), "last name should not be null");

        LastName = lastName;
    }
}