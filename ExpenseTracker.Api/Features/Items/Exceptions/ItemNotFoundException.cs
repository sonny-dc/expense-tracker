using ExpenseTracker.Api.Infrastructure.Errors;

namespace ExpenseTracker.Api.Features.Items.Exceptions;

public sealed class ItemNotFoundException : ApiException
{
    public ItemNotFoundException() 
        : base(
            statusCode: StatusCodes.Status404NotFound, 
            title: "Item not found", 
            detail: "The requested item was not found.")
    {
    }

}
