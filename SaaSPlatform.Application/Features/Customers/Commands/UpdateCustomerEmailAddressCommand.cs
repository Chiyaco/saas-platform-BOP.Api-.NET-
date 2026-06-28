using MediatR;
using Microsoft.EntityFrameworkCore;
using SaaSPlatform.Application.Abstractions.Interfaces;
using SaaSPlatform.Application.Common.Models;
using SaaSPlatform.Application.Features.Customers.DTOs;
using SaaSPlatform.Domain.Entities.Customer;

namespace SaaSPlatform.Application.Features.Customers.Commands
{
    public record UpdateCustomerEmailAddressCommand(UpdateCustomerEmailAddressCommandDto requestDto) : IRequest<Result>;

    public class UpdateCustomerEmailAddressCommandHandler : IRequestHandler<UpdateCustomerEmailAddressCommand, Result>
    {
        private readonly IApplicationDbContext _dbContext;

        public UpdateCustomerEmailAddressCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(UpdateCustomerEmailAddressCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existedCustomer = await _dbContext.Customers
                    .Where(c => c.Id == request.requestDto.Id)
                    .FirstOrDefaultAsync(cancellationToken);

                if (existedCustomer == null)
                    return Result.Failure("The customer didn't find!");

                existedCustomer.ChangeEmailAddress(new Email(request.requestDto.EmailAddress));
                await _dbContext.SaveChangesAsync(cancellationToken);

                return Result.Success;
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }
    }

}
