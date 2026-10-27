using ExpenseTracker.Api.Infrastructure.Errors;

namespace ExpenseTracker.Api.Features.Items.Exceptions;

public sealed class ItemNotFoundException : ApiException
{
    public ItemNotFoundException(string message, string? detail = null)
        : base(
            statusCode: StatusCodes.Status404NotFound,
            title: "Item not found",
            detail: detail ?? "The requested item was not found.",
            message: message)
    {
    }
}
