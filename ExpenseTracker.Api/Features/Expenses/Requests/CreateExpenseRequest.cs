using ExpenseTracker.Api.Features.Expenses.Items.Requests;

namespace ExpenseTracker.Api.Features.Expenses.Requests;

public sealed class CreateExpenseRequest
{
    public string Title { get; set; } = string.Empty;
    public string? Notes { get; set; }
    public IReadOnlyList<CreateExpenseItemRequest> Items { get; set; } = [];
}
