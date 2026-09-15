using System.Net.Http.Json;
using System.Text.Json;

using ExpenseTracker.WinForms.Features.Expenses.Models;

using ExpenseTracker.WinForms.Infrastructure.Http;

namespace ExpenseTracker.WinForms.Features.Expenses.Api;

public sealed class ExpensesApiClient
{
    private const string ExpensesRoute = "expenses";

    private const string ExpenseEntriesRoute = $"{ExpensesRoute}/entries";

    private readonly IHttpClientFactory _httpClientFactory;

    public ExpensesApiClient(
        IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IReadOnlyList<ExpenseResult>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        HttpClient httpClient =
            _httpClientFactory.CreateApiClient();

        using HttpResponseMessage response =
            await httpClient.GetAsync(
                ExpensesRoute,
                cancellationToken);

        await response.EnsureApiSuccessAsync(
            cancellationToken);

        IReadOnlyList<ExpenseResult>? expenses =
            await response.Content.ReadFromJsonAsync<
                IReadOnlyList<ExpenseResult>>(
                    cancellationToken);

        return expenses ?? [];
    }

    public async Task<ExpenseResult> GetByIdAsync(
        int expenseEntryId,
        CancellationToken cancellationToken = default)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(
            expenseEntryId);

        HttpClient httpClient =
            _httpClientFactory.CreateApiClient();

        string expenseRoute =
            $"{ExpensesRoute}/{expenseEntryId}";

        using HttpResponseMessage response =
            await httpClient.GetAsync(
                expenseRoute,
                cancellationToken);

        await response.EnsureApiSuccessAsync(
            cancellationToken);

        ExpenseResult? expense =
            await response.Content.ReadFromJsonAsync<
                ExpenseResult>(
                    cancellationToken);

        return expense
            ?? throw new JsonException(
                "The API response did not contain a valid expense.");
    }

    public async Task<ExpenseEntrySummary> GetSummaryAsync(
        CancellationToken cancellationToken = default)
    {
        HttpClient httpClient =
            _httpClientFactory.CreateApiClient();

        using HttpResponseMessage response =
            await httpClient.GetAsync(
                $"{ExpenseEntriesRoute}/summary",
                cancellationToken);

        await response.EnsureApiSuccessAsync(
            cancellationToken);

        ExpenseEntrySummary? summary =
            await response.Content.ReadFromJsonAsync<
                ExpenseEntrySummary>(
                    cancellationToken);

        return summary
            ?? throw new JsonException(
                "The API response did not contain a valid expense summary.");
    }

    public async Task<ExpenseResult> CreateAsync(
        CreateExpenseRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        HttpClient httpClient =
            _httpClientFactory.CreateApiClient();

        using HttpResponseMessage response =
            await httpClient.PostAsJsonAsync(
                ExpensesRoute,
                request,
                cancellationToken);

        await response.EnsureApiSuccessAsync(
            cancellationToken);

        ExpenseResult? createdExpense =
            await response.Content.ReadFromJsonAsync<ExpenseResult>(
                cancellationToken);

        return createdExpense
            ?? throw new JsonException(
                "The API response did not contain a valid expense.");
    }
}
