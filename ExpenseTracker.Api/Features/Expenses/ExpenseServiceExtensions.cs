using FluentValidation;

using ExpenseTracker.Api.Features.Expenses.Entries;
using ExpenseTracker.Api.Features.Expenses.Items;
using ExpenseTracker.Api.Features.Expenses.Validators;

namespace ExpenseTracker.Api.Features.Expenses;

public static class ExpenseServiceExtensions
{
    public static IServiceCollection AddExpenses(
        this IServiceCollection services)
    {
        // Entry
        services.AddScoped<ExpenseEntryRepository>();
        services.AddScoped<ExpenseEntryService>();

        // Item
        services.AddScoped<ExpenseItemRepository>();
        services.AddScoped<ExpenseItemService>();

        // Main Expense
        services.AddScoped<ExpenseService>();

        services.AddValidatorsFromAssemblyContaining<
            CreateExpenseRequestValidator>();

        return services;
    }
}
