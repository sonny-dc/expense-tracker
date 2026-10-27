using FluentValidation;
using ExpenseTracker.Api.Features.Budget.Accounts.Requests;

namespace ExpenseTracker.Api.Features.Budget.Accounts.Validators;

public sealed class CreateBudgetAccountRequestValidator
    : AbstractValidator<CreateBudgetAccountRequest>
{
    public CreateBudgetAccountRequestValidator()
    {
        RuleFor(request => request.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(request => request.Description)
            .MaximumLength(500)
            .When(request => request.Description is not null);

        RuleFor(request => request.Balance)
            .PrecisionScale(
                precision: 18,
                scale: 2,
                ignoreTrailingZeros: true);
    }
}
