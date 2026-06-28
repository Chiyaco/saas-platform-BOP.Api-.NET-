using MediatR;
using Microsoft.EntityFrameworkCore;
using SaaSPlatform.Application.Abstractions.Interfaces;
using SaaSPlatform.Application.Common.Models;
using SaaSPlatform.Application.Features.Customers.DTOs;

namespace SaaSPlatform.Application.Features.Customers.Queries;

public record GetCustomerByIdQuery(Guid Id) : IRequest<Result<CustomerDetailDto>>;

public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, Result<CustomerDetailDto>>
{
    private readonly IApplicationDbContext _dbContext;

    public GetCustomerByIdQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<CustomerDetailDto>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            if(request.Id == Guid.Empty)
                return Result<CustomerDetailDto>.Failure("The customer Id should nopt be null!");

            var customer = await _dbContext.Customers.AsNoTracking()
                .Where(c => c.Id == request.Id)
                .Select(c =>
                    new CustomerDetailDto
                    {
                        FirstName = c.FirstName,
                        LastName = c.LastName,
                        Email = c.Email.Value
                    }).FirstOrDefaultAsync(cancellationToken);

            if (customer == null)
                return Result<CustomerDetailDto>.Failure("The customer didn't find!");

            return Result<CustomerDetailDto>.Success(customer);
        }
        catch (Exception ex)
        {
            return Result<CustomerDetailDto>.Failure(ex.Message);
        }
    }
}