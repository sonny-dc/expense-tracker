namespace ExpenseTracker.Api.Features.Budget.Logs;

public sealed class BudgetAccountLog
{
    public int BudgetAccountLogId { get; set; }
    public int BudgetAccountId { get; set; }
    public int? ExpenseEntryId { get; set; }
    public decimal Amount { get; set; }
    public decimal BalanceBefore { get; set; }
    public decimal BalanceAfter { get; set; }
    public BudgetAccountLogType LogType { get; set; }
    public DateTime LogDateTime { get; set; }
}
