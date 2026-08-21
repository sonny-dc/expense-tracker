using FluentValidation;

namespace ExpenseTracker.Api.Features.Items.Validators;

public class UpdateItemRequestValidator
    : AbstractValidator<UpdateItemRequest>
{
    public UpdateItemRequestValidator()
    {
        RuleFor(request => request)
            .Must(HaveAtLeastOneValue)
            .WithMessage(
                "At least one item field must be provided.");

        RuleFor(request => request.Name)
            .NotEmpty()
            .MaximumLength(100)
            .When(request => request.Name is not null);

        RuleFor(request => request.Code)
            .NotEmpty()
            .MaximumLength(30)
            .When(request => request.Code is not null);

        RuleFor(request => request.Brand)
            .NotEmpty()
            .MaximumLength(100)
            .When(request => request.Brand is not null);

        RuleFor(request => request.UnitPrice)
            .GreaterThanOrEqualTo(0)
            .When(request => request.UnitPrice.HasValue);
    }

    private static bool HaveAtLeastOneValue(
        UpdateItemRequest request)
    {
        return 
            request.Name is not null ||
            request.Code is not null ||
            request.Brand is not null ||
            request.UnitPrice.HasValue;
    }
}