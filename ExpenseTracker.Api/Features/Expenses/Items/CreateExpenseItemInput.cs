namespace ExpenseTracker.Api.Features.Expenses.Items;

public sealed class CreateExpenseItemInput
{
    public int ExpenseEntryId { get; set; }
    public int ItemId { get; set; }
    public string ItemNameSnapshot { get; set; } = string.Empty;
    public string ItemCodeSnapshot { get; set; } = string.Empty;
    public string BrandSnapshot { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public decimal UnitCostSnapshot { get; set; }
    public decimal LineTotal { get; set; }
}
