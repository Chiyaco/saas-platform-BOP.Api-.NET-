using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SaaSPlatform.Application.Features.Orders.Commands;
using SaaSPlatform.Application.Tests.Models;

namespace SaaSPlatform.Application.Tests.Orders.Commands;

public class CreateOrderCommandTests
{
    private readonly DbContextFixture _contextFixture = new();

    [Fact]
    public async Task CreateOrderCommand_Should_Return_Fail_When_CustomerId_Is_Empty()
    {
        // Arrange 

        await using var dbContext = _contextFixture.CreateDbContext();

        var services = new ServiceCollection();

        services.AddApplication(); // your DI setup
        services.AddSingleton(dbContext);

        var provider = services.BuildServiceProvider();
        var mediator = provider.GetRequiredService<IMediator>();

        var command = new CreateOrderCommand(Guid.Empty);

        //var command = new CreateOrderCommand(Guid.Empty);
        //var handler = new CreateOrderCommandHandler(dbContext);

        // Act 

        var result = await mediator.Send(command);

        // Assert

        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be("CustomerId is required");

    }

    [Fact]
    public async Task CreateOrderCommand_Should_Return_Success_When_CustomerId_Is_Valid()
    {
        // Arrange

        await using var dbContext = _contextFixture.CreateDbContext();

        var command = new CreateOrderCommand(Guid.NewGuid());
        var handler = new CreateOrderCommandHandler(dbContext);

        // Act 

        var result = await handler.Handle(command, CancellationToken.None);

        // Assert

        result.IsSuccess.Should().BeTrue();
    }
}
