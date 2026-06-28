using MediatR;
using Microsoft.EntityFrameworkCore;
using SaaSPlatform.Application.Abstractions.Interfaces;
using SaaSPlatform.Application.Common.Models;
using SaaSPlatform.Domain.Entities.Product;

namespace SaaSPlatform.Application.Features.Products.Commands;


public record CreateProductCommand(string Name, decimal Price) : IRequest<Result>;


public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result>
{
    private readonly IApplicationDbContext _dbContext;

    public CreateProductCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        bool exist = await _dbContext.Products
            .AsNoTracking()
            .AnyAsync(p => p.Name.ToLower() == request.Name.ToLower());

        if (exist)
            return Result.Failure("The entered Product is exists");

        var product = new Product(request.Name, request.Price);

        _dbContext.Products.Add(product);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success;
    }
}