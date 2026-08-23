using FluentValidation;

namespace ExpenseTracker.Api.Features.Items.Validators;

public sealed class CreateItemRequestValidator
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
            .MaximumLength(100);

        RuleFor(request => request.UnitPrice)
            .GreaterThanOrEqualTo(0)
            .PrecisionScale(
                precision: 18,
                scale: 2,
                ignoreTrailingZeros: true);
    }
}
