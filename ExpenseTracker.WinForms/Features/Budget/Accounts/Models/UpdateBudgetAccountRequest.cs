namespace ExpenseTracker.WinForms.Features.Budget.Accounts.Models;

public sealed class UpdateBudgetAccountRequest
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal? Balance { get; set; }
    public BudgetAccountStatus? Status { get; set; }
}
