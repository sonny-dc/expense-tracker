using ExpenseTracker.Api.Infrastructure.Errors;

namespace ExpenseTracker.Api.Features.Budget.Accounts.Exceptions;

public sealed class BudgetAccountStatusMismatchException : ApiException
{
    public BudgetAccountStatusMismatchException(string message)
        : base(
            statusCode: StatusCodes.Status409Conflict,
            title: "Budget account status mismatch",
            detail: "The budget account's current status does not allow this operation.",
            message: message)
    {
    }
}
