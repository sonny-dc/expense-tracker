namespace ExpenseTracker.Api.Features.Items;

public static class ItemServiceExtensions
{
    public static IServiceCollection AddItems(
        this IServiceCollection services)
    {
        services.AddScoped<ItemRepository>();
        services.AddScoped<ItemService>();

        return services;
    }
}
