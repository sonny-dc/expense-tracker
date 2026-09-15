namespace ExpenseTracker.WinForms.Features.Expenses.Models;

public sealed class CreateExpenseItemRequest
{
    public int ItemId { get; set; }
    public decimal Quantity { get; set; }
}
