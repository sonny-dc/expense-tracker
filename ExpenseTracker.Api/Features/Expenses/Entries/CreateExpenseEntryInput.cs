namespace ExpenseTracker.Api.Features.Expenses.Entries;

public sealed class CreateExpenseEntryInput
{
    public string Title { get; set; } = string.Empty;
    public decimal TotalCost { get; set; }
    public string? Notes { get; set; }
}
