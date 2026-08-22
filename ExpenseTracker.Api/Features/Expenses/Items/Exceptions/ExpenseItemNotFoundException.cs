using ExpenseTracker.Api.Infrastructure.Errors;

namespace ExpenseTracker.Api.Features.Expenses.Items.Exceptions;

public sealed class ExpenseItemNotFoundException : ApiException
{
    public ExpenseItemNotFoundException()
        : base(
            statusCode: StatusCodes.Status404NotFound,
            title: "Expense item not found",
            detail: "The requested expense item was not found.")
    {
    }

}
