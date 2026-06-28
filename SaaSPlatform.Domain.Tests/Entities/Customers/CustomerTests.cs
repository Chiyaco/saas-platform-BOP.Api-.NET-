using FluentAssertions;
using SaaSPlatform.Domain.Entities.Customer;

namespace SaaSPlatform.Domain.Tests.Entities.Customers;

public class CustomerTests
{
    [Fact]
    public void CustomerCreate_Should_SetPropertiesCorrectly()
    {
        // Arrange
        var firstName = "chia";
        var lastName = "karimi";
        var email = new Email("chia.karimi@gmail.com");

        // Act

        var customer = new Customer(firstName, lastName, email);

        // Assert

        customer.FirstName.Should().Be(firstName);
        customer.LastName.Should().Be(lastName);
        customer.Email.Should().Be(email);
    }

    [Fact]
    // To test business rules
    public void CustomerCreate_Should_ThrowException_WhenEmailIsNull()
    {
        // Arrange
        Action act = () =>
        {
            var email = new Email(string.Empty);

            var customer = new Customer("chia", "karimi", email);
        };

        //Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void CustomerCreate_Should_ThrowException_When_FirstName_Is_null()
    {
        // Arrange
        var firstName = string.Empty;
        var lastName = "Karimi";
        var email = new Email("chia.karimi@gmail.com");

        // Act
        Action act = () =>
        {
            var customer = new Customer(firstName, lastName, email);
        };

        // Assert

        act.Should()
            .Throw<ArgumentNullException>()
            .WithMessage("*first name should not be null*");

        act.Should()
            .Throw<ArgumentNullException>()
            .Where(e => e.ParamName == "firstName");
    }

    [Fact]
    public void CustomerCreate_Should_ThrowException_When_LastName_Is_null()
    {
        // Arrange
        var firstName = "Chia";
        var lastName = string.Empty;
        var email = new Email("chia.karimi@gmail.com");
        var tenantId = Guid.NewGuid();

        // Act

        Action act = () =>
        {
            var customer = new Customer(firstName, lastName, email);
        };

        //Assert
        act.Should().Throw<ArgumentException>()
            .WithMessage("*last name should not be null*");

        act.Should().Throw<ArgumentNullException>()
            .Where(e => e.ParamName == "lastName");
    }

    [Fact]
    public void ChangeEmailAddress_Should_Throw_When_Email_Is_Nul()
    {
        // Arrange
        var firstName = "Chia";
        var lastName = "Karimi";
        var email = new Email("chia.karimi@gmail.com");

        // Act

        Action act = () =>
        {
            var customer = new Customer(firstName, lastName, email);

            customer.ChangeEmailAddress(new Email(string.Empty));
        };
        // Assert

        act.Should()
            .Throw<ArgumentException>();
    }

}
