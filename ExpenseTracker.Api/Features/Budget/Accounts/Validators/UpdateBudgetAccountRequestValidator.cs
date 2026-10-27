using FluentValidation;
using ExpenseTracker.Api.Features.Budget.Accounts.Requests;

namespace ExpenseTracker.Api.Features.Budget.Accounts.Validators;

public sealed class UpdateBudgetAccountRequestValidator
    : AbstractValidator<UpdateBudgetAccountRequest>
{
    public UpdateBudgetAccountRequestValidator()
    {
        RuleFor(request => request.Name)
            .NotEmpty()
            .MaximumLength(100)
            .When(request => request.Name is not null);

        RuleFor(request => request.Description)
            .MaximumLength(500)
            .When(request => request.Description is not null);

        RuleFor(request => request.Balance)
            .PrecisionScale(
                precision: 18,
                scale: 2,
                ignoreTrailingZeros: true)
            .When(request => request.Balance is not null);

        RuleFor(request => request.Status)
            .IsInEnum()
            .When(request => request.Status is not null);
    }
}
