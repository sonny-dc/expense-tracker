namespace ExpenseTracker.Api.Features.Expenses.Items.Requests;

public sealed class CreateExpenseItemRequest
{
    public int ItemId { get; set; }
    public decimal Quantity { get; set; }
}
