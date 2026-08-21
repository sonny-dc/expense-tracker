namespace ExpenseTracker.Api.Features.Items;

public sealed class Item
{
    public int ItemId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
}
