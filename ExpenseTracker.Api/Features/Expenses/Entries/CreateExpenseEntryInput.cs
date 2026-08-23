namespace ExpenseTracker.Api.Features.Expenses.Entries;

public sealed class CreateExpenseEntryInput
{
    public decimal TotalCost { get; set; }
    public string? Notes { get; set; }
}
