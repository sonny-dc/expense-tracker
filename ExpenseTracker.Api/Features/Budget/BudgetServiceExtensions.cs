using ExpenseTracker.Api.Features.Budget.Accounts;

using ExpenseTracker.Api.Features.Budget.Logs;

namespace ExpenseTracker.Api.Features.Budget;

public static class BudgetServiceExtensions
{
    public static IServiceCollection AddBudgets(
        this IServiceCollection services)
    {
        // Account
        services.AddScoped<BudgetAccountRepository>();
        services.AddScoped<BudgetAccountService>();

        // Logs
        services.AddScoped<BudgetAccountLogRepository>();
        services.AddScoped<BudgetAccountLogService>();

        // Parent Budget Service
        services.AddScoped<BudgetService>();

        return services;
    }
}
