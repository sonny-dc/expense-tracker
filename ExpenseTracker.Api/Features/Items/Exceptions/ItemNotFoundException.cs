using ExpenseTracker.Api.Infrastructure.Errors;

namespace ExpenseTracker.Api.Features.Items.Exceptions;

public sealed class ItemNotFoundException : ApiException
{
    public ItemNotFoundException(string message = "The requested item was not found.") 
        : base(
            statusCode: StatusCodes.Status404NotFound, 
            title: "Item not found", 
            detail: message)
    {
    }

}
