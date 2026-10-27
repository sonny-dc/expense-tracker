using ExpenseTracker.Api.Infrastructure.Errors;

namespace ExpenseTracker.Api.Features.Items.Exceptions;

public sealed class DuplicateItemException : ApiException
{
    public DuplicateItemException(string message)
        : base(
            statusCode: StatusCodes.Status409Conflict,
            title: "Duplicate item",
            detail: "An item with the same name, code, and brand already exists.",
            message: message)
    {
    }
}
