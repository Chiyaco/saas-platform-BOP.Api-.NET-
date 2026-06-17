using FluentValidation;

namespace SaaSPlatform.Application.Features.Orders.Commands;

public class CreateOrderCommandValidation : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidation()
    {
        RuleFor(x => x.customerId)
            .NotEmpty()
            .WithMessage("CustomerId is required");
    }
}
