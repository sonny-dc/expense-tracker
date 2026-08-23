using FluentValidation;
using ExpenseTracker.Api.Features.Expenses.Requests;

namespace ExpenseTracker.Api.Features.Expenses.Validators;

public sealed class CreateExpenseRequestValidator
    : AbstractValidator<CreateExpenseRequest>
{
    public CreateExpenseRequestValidator()
    {
        RuleFor(request => request.Title)
            .NotEmpty()
            .MaximumLength(100);
            
        RuleFor(request => request.Notes)
            .MaximumLength(500)
            .When(request => request.Notes is not null);

        RuleFor(request => request.Items)
            .NotEmpty()
            .WithMessage(
                "At least one expense item must be provided.");

        RuleForEach(request => request.Items)
            .ChildRules(item =>
            {
                item.RuleFor(request => request.ItemId)
                    .GreaterThan(0);

                item.RuleFor(request => request.Quantity)
                    .GreaterThan(0)
                    .PrecisionScale(
                        precision: 18,
                        scale: 3,
                        ignoreTrailingZeros: true);
            });
    }
}
