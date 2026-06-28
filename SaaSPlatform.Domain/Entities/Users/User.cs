using SaaSPlatform.Domain.Entities.Customer;
using System.Text.RegularExpressions;

namespace SaaSPlatform.Domain.Entities.Users;

public class User : BaseEntity
{
    public string UserName { get; private set; }

    public string Email { get; private set; }

    private User()
    {
    }

    public User(string userName, string email)
    {
        SetUserName(userName);
        EmailAddressValidation(email);

        Id = Guid.NewGuid();
        CreatedDateTime = DateTime.UtcNow;
    }

    public void Update(string userName, string email)
    {
        SetUserName(userName);

        EmailAddressValidation(email);

        Touch();
    }

    private void SetUserName(string userName)
    {
        if (string.IsNullOrWhiteSpace(userName))
            throw new ArgumentNullException(nameof(userName), "user name should not be null");

        UserName = userName;
    }

    private void EmailAddressValidation(string email)
    {

        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException(nameof(email), "email address should not be null");

        const string pattern =
            @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

        if (!Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase))
            throw new ArgumentException(nameof(email), "email address is not valid");

        Email = email;
    }
}
