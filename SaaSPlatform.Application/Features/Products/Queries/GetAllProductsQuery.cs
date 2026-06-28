using MediatR;
using Microsoft.EntityFrameworkCore;
using SaaSPlatform.Application.Abstractions.Interfaces;
using SaaSPlatform.Application.Common.Models;
using SaaSPlatform.Application.Features.Products.DTOs;

namespace SaaSPlatform.Application.Features.Products.Queries;

public record GetAllProductsQuery(int PageNumber = 1, int PageSize = 10) : IRequest<Result<PagedResult<ProductDetailsDto>>>;


public class GetAllProductQueryHandler : IRequestHandler<GetAllProductsQuery, Result<PagedResult<ProductDetailsDto>>>
{
    private readonly IApplicationDbContext _dbContext;

    public GetAllProductQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<PagedResult<ProductDetailsDto>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var query = _dbContext.Products
            .AsNoTracking()
            .OrderBy(p => p.Name)
            .Select(p =>
            new ProductDetailsDto
            {
                Name = p.Name,
                Price = p.Price
            });

        var result = await PagedResult<ProductDetailsDto>
            .CreateAsync(query, request.PageNumber, request.PageSize, cancellationToken);

        return Result<PagedResult<ProductDetailsDto>>.Success(result);
    }
}