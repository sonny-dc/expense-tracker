namespace ExpenseTracker.WinForms.Features.Expenses.Models;

public sealed class ExpenseResult
{
    public ExpenseEntry ExpenseEntry { get; set; } = new();

    public IReadOnlyList<ExpenseItem> ExpenseItems { get; set; } = [];
}
