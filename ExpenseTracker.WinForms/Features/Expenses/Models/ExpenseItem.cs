namespace ExpenseTracker.WinForms.Features.Expenses.Models;

public sealed class ExpenseItem
{
    public int ExpenseItemId { get; set; }
    public int ExpenseEntryId { get; set; }
    public int? ItemId { get; set; }
    public string ItemNameSnapshot { get; set; } = string.Empty;
    public string ItemCodeSnapshot { get; set; } = string.Empty;
    public string BrandSnapshot { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public decimal UnitPriceSnapshot { get; set; }
    public decimal LineTotal { get; set; }
}
