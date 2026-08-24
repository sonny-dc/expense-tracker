namespace ExpenseTracker.WinForms.Features.Items.Models;

public sealed class UpdateItemRequest
{
    public string? Name { get; set; }

    public string? Code { get; set; }

    public string? Brand { get; set; }

    public decimal? UnitPrice { get; set; }
}
