using MediatR;
using Microsoft.EntityFrameworkCore;
using SaaSPlatform.Application.Abstractions.Interfaces;
using SaaSPlatform.Application.Common.Models;
using SaaSPlatform.Application.Features.Products.DTOs;

namespace SaaSPlatform.Application.Features.Products.Queries;

public record GetProductByIdQuery(Guid Id) : IRequest<Result<ProductDetailsDto>>;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Result<ProductDetailsDto>>
{
    private readonly IApplicationDbContext _dbContext;

    public GetProductByIdQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<ProductDetailsDto>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _dbContext.Products
            .AsNoTracking()
            .Where(p => p.Id == request.Id)
            .Select(p =>
                new ProductDetailsDto
                {
                    Price = p.Price,
                    Name = p.Name
                }
            ).FirstOrDefaultAsync(cancellationToken);

        return product == null
            ? Result<ProductDetailsDto>.Failure("There is no record for this")
            : Result<ProductDetailsDto>.Success(product);
    }
}
