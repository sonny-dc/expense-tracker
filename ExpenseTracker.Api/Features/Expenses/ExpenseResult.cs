using ExpenseTracker.Api.Features.Expenses.Entries;
using ExpenseTracker.Api.Features.Expenses.Items;

namespace ExpenseTracker.Api.Features.Expenses;

public sealed class ExpenseResult
{
    public ExpenseResult(ExpenseEntry expenseEntry, IReadOnlyList<ExpenseItem> expenseItems)
    {
        ExpenseEntry = expenseEntry;
        ExpenseItems = expenseItems;
    }
    public ExpenseEntry ExpenseEntry { get; }

    public IReadOnlyList<ExpenseItem> ExpenseItems { get; }
}
