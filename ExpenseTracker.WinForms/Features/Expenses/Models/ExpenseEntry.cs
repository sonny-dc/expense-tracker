namespace ExpenseTracker.WinForms.Features.Expenses.Models;

public sealed class ExpenseEntry
{
    public int ExpenseEntryId { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime ExpenseDateTime { get; set; }
    public decimal TotalCost { get; set; }
    public string? Notes { get; set; }

}
