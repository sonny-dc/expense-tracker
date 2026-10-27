using ExpenseTracker.Api.Infrastructure.Errors;

namespace ExpenseTracker.Api.Features.Budget.Accounts.Exceptions;

public sealed class BudgetAccountNotFoundException : ApiException
{
    public BudgetAccountNotFoundException(string message)
        : base(
            statusCode: StatusCodes.Status404NotFound,
            title: "Budget account not found",
            detail: "The requested budget account was not found.",
            message: message)
    {
    }
}
