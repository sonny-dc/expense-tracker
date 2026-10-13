namespace ExpenseTracker.Api.Features.Budget.Accounts;

public sealed class BudgetAccount
{
    public int BudgetAccountId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Balance { get; set; }
    public BudgetAccountStatus Status { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public DateTime LastUpdateDateTime { get; set; }
}
