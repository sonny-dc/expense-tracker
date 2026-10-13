namespace ExpenseTracker.Api.Features.Budget.Accounts.Requests;

public sealed class CreateBudgetAccountRequest
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Balance { get; set; }
}
