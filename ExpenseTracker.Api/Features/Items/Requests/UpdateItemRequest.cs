namespace ExpenseTracker.Api.Features.Items;

public sealed class UpdateItemRequest
{
    public string? Name { get; set; }

    public string? Code { get; set; }

    public string? Brand { get; set; }

    public decimal? UnitPrice { get; set; }
}
