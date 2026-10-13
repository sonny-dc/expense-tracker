namespace ExpenseTracker.Api.Features.Budget.Accounts.Requests;

public sealed class UpdateBudgetAccountRequest
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal? Balance { get; set; }
    public BudgetAccountStatus? Status { get; set; }
}
