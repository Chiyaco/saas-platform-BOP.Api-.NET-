using MediatR;
using Microsoft.EntityFrameworkCore;
using SaaSPlatform.Application.Abstractions.Interfaces;
using SaaSPlatform.Application.Common.Models;
using SaaSPlatform.Application.Features.Customers.DTOs;

namespace SaaSPlatform.Application.Features.Customers.Queries;


public record GetAllCustomerQuery(int PageNumber = 1, int PageSize = 10)
    : IRequest<Result<PagedResult<CustomerDetailDto>>>;


public class GetAllCustomerQueryHandler : IRequestHandler<GetAllCustomerQuery, Result<PagedResult<CustomerDetailDto>>>
{
    private readonly IApplicationDbContext _dbContext;

    public GetAllCustomerQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<PagedResult<CustomerDetailDto>>> Handle(GetAllCustomerQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var query = _dbContext.Customers.AsNoTracking()
                    .OrderBy(c => c.LastName)
                    .Select(c =>
                        new CustomerDetailDto
                        {
                            FirstName = c.FirstName,
                            LastName = c.LastName,
                            Email = c.Email.Value,
                        });

            var pagedResult = await PagedResult<CustomerDetailDto>.CreateAsync(
                    query,
                    request.PageNumber,
                    request.PageSize,
                    cancellationToken
                );

            return Result<PagedResult<CustomerDetailDto>>.Success(pagedResult); ;

        }
        catch (Exception ex)
        {
            return Result<PagedResult<CustomerDetailDto>>.Failure(ex.Message);
        }
    }
}