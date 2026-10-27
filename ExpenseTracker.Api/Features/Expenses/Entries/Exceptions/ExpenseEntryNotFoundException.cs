using ExpenseTracker.Api.Infrastructure.Errors;

namespace ExpenseTracker.Api.Features.Expenses.Entries.Exceptions;

public sealed class ExpenseEntryNotFoundException : ApiException
{
    public ExpenseEntryNotFoundException(string message)
        : base(
            statusCode: StatusCodes.Status404NotFound,
            title: "Expense entry not found",
            detail: "The requested expense entry was not found.",
            message: message)
    {
    }

}
