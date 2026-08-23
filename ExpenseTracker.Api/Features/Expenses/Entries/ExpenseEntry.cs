namespace ExpenseTracker.Api.Features.Expenses.Entries;

public sealed class ExpenseEntry
{
    public int ExpenseEntryId { get; set; }
    public DateTime ExpenseDateTime { get; set; }
    public decimal TotalCost { get; set; }
    public string? Notes { get; set; }

}
