using FluentAssertions;
using SaaSPlatform.Application.Features.Customers.Commands;
using SaaSPlatform.Application.Features.Customers.DTOs;
using SaaSPlatform.Application.Tests.Models;
using SaaSPlatform.Domain.Entities.Customer;

namespace SaaSPlatform.Application.Tests.Customers.Commands;

public class UpdateCustomerCommandTests
{
    private readonly DbContextFixture _contextFixture = new();

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public async Task UpdateCustomerCommand_Throw_When_FirstName_Is_Null(string firstName)
    {
        // Arrange

        await using var dbContext = _contextFixture.CreateDbContext();

        var existedCustomer = await SeedData.CreateRowDataAsync<Customer>(dbContext,
            () => new Customer(
                "Chia",
                "Karimi",
                new Email("Chia.karimi@gmail.com")));



        var requestDto = new UpdateCustomerCommandDto()
        {
            Id = existedCustomer.Id,
            FirstName = firstName,
            LastName = existedCustomer.LastName,
            EmailAddress = existedCustomer.Email.Value
        };

        var query = new UpdateCustomerCommand(requestDto);
        var queryHandler = new UpdateCustomerCommandHandler(dbContext);

        // Act
        var result = await queryHandler.Handle(query, CancellationToken.None);

        // Assert

        result.IsSuccess.Should().BeFalse();
        result.Error.Should().NotBeNull();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public async Task UpdateCustomerCommand_Throw_When_LastName_Is_Null(string lastName)
    {
        // Arrange

        await using var dbContext = _contextFixture.CreateDbContext();

        var existedCustomer = await SeedData.CreateRowDataAsync<Customer>(dbContext,
            () => new Customer(
                "Chia",
                "Karimi",
                new Email("Chia.karimi@gmail.com")));

        var requestDto = new UpdateCustomerCommandDto()
        {
            Id = existedCustomer.Id,
            FirstName = existedCustomer.FirstName,
            LastName = lastName,
            EmailAddress = existedCustomer.Email.Value
        };

        var query = new UpdateCustomerCommand(requestDto);
        var queryHandler = new UpdateCustomerCommandHandler(dbContext);

        // Act
        var result = await queryHandler.Handle(query, CancellationToken.None);

        // Assert

        result.IsSuccess.Should().BeFalse();
        result.Error.Should().NotBeNull();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("chia.Karimi")]
    public async Task UpdateCustomerCommand_Throw_When_EmailAddress_Is_Invalid(string emailAddress)
    {
        // Arrange

        await using var dbContext = _contextFixture.CreateDbContext();

        var existedCustomer = await SeedData.CreateRowDataAsync<Customer>(dbContext,
            () => new Customer(
                "Chia",
                "Karimi",
                new Email("Chia.karimi@gmail.com")));

        var requestDto = new UpdateCustomerCommandDto()
        {
            Id = existedCustomer.Id,
            FirstName = existedCustomer.FirstName,
            LastName = existedCustomer.LastName,
            EmailAddress = emailAddress
        };

        var query = new UpdateCustomerCommand(requestDto);
        var queryHandler = new UpdateCustomerCommandHandler(dbContext);

        // Act
        var result = await queryHandler.Handle(query, CancellationToken.None);

        // Assert

        result.IsSuccess.Should().BeFalse();
        result.Error.Should().NotBeNull();
    }

    [Fact]
    public async Task UpdateCustomerCommand_Should_Update_CustomerProfile()
    {
        // Arrange 
        await using var dbContext = _contextFixture.CreateDbContext();
        var existedCustomer = await SeedData.CreateRowDataAsync<Customer>(dbContext,
            () => new Customer(
                "Chia",
                "Karimi",
                new Email("Chia.karimi@gmail.com")));

        var requestDto = new UpdateCustomerCommandDto()
        {
            Id = existedCustomer.Id,
            FirstName = "Kak Chia",
            LastName = "Karimi Sine",
            EmailAddress = "chia.karimi2@gmail.com"
        };

        var command = new UpdateCustomerCommand(requestDto);
        var handler = new UpdateCustomerCommandHandler(dbContext);

        // Act 

        var result = await handler.Handle(command, CancellationToken.None);

        // Assert

        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task UpdateCustomerCommand_Should_Change_Email_Address()
    {
        // Arrange 

        await using var dbContext = _contextFixture.CreateDbContext();

        var existedCustomer = await SeedData.CreateRowDataAsync<Customer>(dbContext,
            () => new Customer(
                "Chia",
                "Karimi",
                new Email("Chia.karimi@gmail.com")));

        var requestDto = new UpdateCustomerEmailAddressCommandDto
        {
            EmailAddress = "Chia.karimi22@gmail.com",
            Id = existedCustomer.Id
        };

        var command = new UpdateCustomerEmailAddressCommand(requestDto);
        var handler = new UpdateCustomerEmailAddressCommandHandler(dbContext);

        // Act 

        var result = await handler.Handle(command, CancellationToken.None);

        // Assert

        result.IsSuccess.Should().BeTrue();
    }
}
