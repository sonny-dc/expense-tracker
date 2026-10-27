using ExpenseTracker.Api.Infrastructure.Errors;

namespace ExpenseTracker.Api.Features.Budget.Logs.Exceptions;

public sealed class BudgetAccountLogNotFoundException : ApiException
{
    public BudgetAccountLogNotFoundException(string message)
        : base(
            statusCode: StatusCodes.Status404NotFound,
            title: "Budget account log not found",
            detail: "The requested budget account log was not found.",
            message: message)
    {
    }
}
