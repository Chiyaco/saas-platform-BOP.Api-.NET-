using MediatR;
using SaaSPlatform.Application.Abstractions.Interfaces;
using SaaSPlatform.Application.Common.Models;
using SaaSPlatform.Domain.Entities.Order;

namespace SaaSPlatform.Application.Features.Orders.Commands;

public record CreateOrderCommand(Guid customerId) : IRequest<Result>;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Result>
{
    private readonly IApplicationDbContext _dbContext;

    public CreateOrderCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = new Order(request.customerId);

        _dbContext.Orders.Add(order);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success;
    }
}