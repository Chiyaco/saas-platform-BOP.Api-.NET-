using MediatR;
using Microsoft.EntityFrameworkCore;
using SaaSPlatform.Application.Abstractions.Interfaces;
using SaaSPlatform.Application.Common.Models;
using SaaSPlatform.Application.Features.Customers.DTOs;
using SaaSPlatform.Domain.Entities.Customer;

namespace SaaSPlatform.Application.Features.Customers.Commands;

public record UpdateCustomerCommand(UpdateCustomerCommandDto updateRequest) : IRequest<Result>;


public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Result>
{
    private readonly IApplicationDbContext _dbContext;

    public UpdateCustomerCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var existedCustomer = await _dbContext.Customers
                .Where(c => c.Id == request.updateRequest.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (existedCustomer == null)
                return Result.Failure("The customer didn't find!");

            existedCustomer.UpdateProfile
                (
                    request.updateRequest.FirstName,
                    request.updateRequest.LastName,
                    new Email(request.updateRequest.EmailAddress)
                );

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success;
        }
        catch (Exception ex)
        {
            return Result.Failure(ex.Message);
        }
    }
}