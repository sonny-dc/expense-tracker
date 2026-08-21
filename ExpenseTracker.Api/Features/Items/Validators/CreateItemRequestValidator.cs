using FluentValidation;

namespace ExpenseTracker.Api.Features.Items.Validators;

public class CreateItemRequestValidator
    : AbstractValidator<CreateItemRequest>
{
    public CreateItemRequestValidator()
    {
        RuleFor(request => request.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(request => request.Code)
            .NotEmpty()
            .MaximumLength(30);

        RuleFor(request => request.Brand)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(request => request.UnitPrice)
            .GreaterThanOrEqualTo(0);
    }
}
