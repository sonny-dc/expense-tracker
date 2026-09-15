namespace ExpenseTracker.WinForms.Features.Items.Models;

public sealed class CreateItemRequest
{
    public string Name { get; set; } = string.Empty;

    public string Code { get; set; } = string.Empty;

    public string Brand { get; set; } = string.Empty;

    public decimal UnitPrice { get; set; }
}
