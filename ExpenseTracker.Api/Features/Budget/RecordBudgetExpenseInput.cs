namespace ExpenseTracker.Api.Features.Budget;

public sealed class BudgetExpenseInput
{
    public int BudgetAccountId { get; set; }
    public int ExpenseEntryId { get; set; }
    public decimal Amount { get; set; }
}
