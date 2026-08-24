namespace ExpenseTracker.WinForms.Features.Expenses.Models;

public sealed class CreateExpenseRequest
{
    public string Title { get; set; } = string.Empty;
    public string? Notes { get; set; }
    public IReadOnlyList<CreateExpenseItemRequest> Items { get; set; } = [];
}
