using System.Net.Http.Json;

using ExpenseTracker.WinForms.Features.Budget.Logs.Models;
using ExpenseTracker.WinForms.Infrastructure.Http;

namespace ExpenseTracker.WinForms.Features.Budget.Logs.Api;

public sealed class BudgetAccountLogsApiClient
{
    private const string BudgetLogsRoute = "budget/logs";

    private readonly IHttpClientFactory _httpClientFactory;

    public BudgetAccountLogsApiClient(
        IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IReadOnlyList<BudgetAccountLog>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        HttpClient httpClient =
            _httpClientFactory.CreateApiClient();

        using HttpResponseMessage response =
            await httpClient.GetAsync(
                BudgetLogsRoute,
                cancellationToken);

        await response.EnsureApiSuccessAsync(
            cancellationToken);

        IReadOnlyList<BudgetAccountLog>? budgetAccountLogs =
            await response.Content.ReadFromJsonAsync<
                IReadOnlyList<BudgetAccountLog>>(
                    ApiHttpClient.JsonOptions,
                    cancellationToken);

        return budgetAccountLogs ?? [];
    }

    public async Task<IReadOnlyList<BudgetAccountLog>> GetAllByBudgetAccountIdAsync(
            int budgetAccountId,
            CancellationToken cancellationToken = default)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(
            budgetAccountId);

        HttpClient httpClient =
            _httpClientFactory.CreateApiClient();

        string budgetAccountLogsRoute =
            $"{BudgetLogsRoute}/accounts/{budgetAccountId}";

        using HttpResponseMessage response =
            await httpClient.GetAsync(
                budgetAccountLogsRoute,
                cancellationToken);

        await response.EnsureApiSuccessAsync(
            cancellationToken);

        IReadOnlyList<BudgetAccountLog>? budgetAccountLogs =
            await response.Content.ReadFromJsonAsync<
                IReadOnlyList<BudgetAccountLog>>(
                    ApiHttpClient.JsonOptions,
                    cancellationToken);

        return budgetAccountLogs ?? [];
    }

    public async Task<BudgetAccountLog?> GetByExpenseEntryIdAsync(
        int expenseEntryId,
        CancellationToken cancellationToken = default)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(
            expenseEntryId);

        HttpClient httpClient =
            _httpClientFactory.CreateApiClient();

        string budgetAccountLogRoute =
            $"{BudgetLogsRoute}/expense-entries/{expenseEntryId}";

        using HttpResponseMessage response =
            await httpClient.GetAsync(
                budgetAccountLogRoute,
                cancellationToken);

        await response.EnsureApiSuccessAsync(
            cancellationToken);

        return await response.Content.ReadFromJsonAsync<
            BudgetAccountLog>(
                ApiHttpClient.JsonOptions,
                cancellationToken);
    }

}
