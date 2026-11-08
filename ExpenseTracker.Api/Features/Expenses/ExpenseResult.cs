using ExpenseTracker.Api.Features.Budget;
using ExpenseTracker.Api.Features.Expenses.Entries;
using ExpenseTracker.Api.Features.Expenses.Items;

namespace ExpenseTracker.Api.Features.Expenses;

public sealed class ExpenseResult
{
    public ExpenseResult(
        ExpenseEntry expenseEntry,
        IReadOnlyList<ExpenseItem> expenseItems,
        BudgetExpenseResult? budgetExpenseResult = null)
    {
        ExpenseEntry = expenseEntry;
        ExpenseItems = expenseItems;
        BudgetExpenseResult = budgetExpenseResult;
    }

    public ExpenseEntry ExpenseEntry { get; }

    public IReadOnlyList<ExpenseItem> ExpenseItems { get; }

    public BudgetExpenseResult? BudgetExpenseResult { get; }
}
