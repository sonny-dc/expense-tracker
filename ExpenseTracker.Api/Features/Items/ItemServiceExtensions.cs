using FluentValidation;

using ExpenseTracker.Api.Features.Items.Validators;

namespace ExpenseTracker.Api.Features.Items;

public static class ItemServiceExtensions
{
    public static IServiceCollection AddItems(
        this IServiceCollection services)
    {
        services.AddScoped<ItemRepository>();
        services.AddScoped<ItemService>();

        services.AddValidatorsFromAssemblyContaining<CreateItemRequestValidator>();
        services.AddValidatorsFromAssemblyContaining<UpdateItemRequestValidator>();

        return services;
    }
}
