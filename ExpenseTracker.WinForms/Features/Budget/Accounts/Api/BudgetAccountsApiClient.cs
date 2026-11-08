using System.Net.Http.Json;
using System.Text.Json;

using ExpenseTracker.WinForms.Features.Budget.Accounts.Models;
using ExpenseTracker.WinForms.Infrastructure.Http;

namespace ExpenseTracker.WinForms.Features.Budget.Accounts.Api;

public sealed class BudgetAccountsApiClient
{
    private const string BudgetAccountsRoute = "budget/accounts";

    private readonly IHttpClientFactory _httpClientFactory;

    public BudgetAccountsApiClient(
        IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IReadOnlyList<BudgetAccount>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        HttpClient httpClient =
            _httpClientFactory.CreateApiClient();

        using HttpResponseMessage response =
            await httpClient.GetAsync(
                BudgetAccountsRoute,
                cancellationToken);

        await response.EnsureApiSuccessAsync(
            cancellationToken);

        IReadOnlyList<BudgetAccount>? budgetAccounts =
            await response.Content.ReadFromJsonAsync<
                IReadOnlyList<BudgetAccount>>(
                    ApiHttpClient.JsonOptions,
                    cancellationToken);

        return budgetAccounts ?? [];
    }

    public async Task<BudgetAccount> GetByIdAsync(
        int budgetAccountId,
        CancellationToken cancellationToken = default)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(
            budgetAccountId);

        HttpClient httpClient =
            _httpClientFactory.CreateApiClient();

        string budgetAccountRoute =
            $"{BudgetAccountsRoute}/{budgetAccountId}";

        using HttpResponseMessage response =
            await httpClient.GetAsync(
                budgetAccountRoute,
                cancellationToken);

        await response.EnsureApiSuccessAsync(
            cancellationToken);

        BudgetAccount? budgetAccount =
            await response.Content.ReadFromJsonAsync<
                BudgetAccount>(
                    ApiHttpClient.JsonOptions,
                    cancellationToken);

        return budgetAccount
            ?? throw new JsonException(
                "The API response did not contain a valid budget account.");
    }

    public async Task<BudgetAccount> CreateAsync(
        CreateBudgetAccountRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        HttpClient httpClient =
            _httpClientFactory.CreateApiClient();

        using HttpResponseMessage response =
            await httpClient.PostAsJsonAsync(
                BudgetAccountsRoute,
                request,
                ApiHttpClient.JsonOptions,
                cancellationToken);

        await response.EnsureApiSuccessAsync(
            cancellationToken);

        BudgetAccount? createdBudgetAccount =
            await response.Content.ReadFromJsonAsync<
                BudgetAccount>(
                    ApiHttpClient.JsonOptions,
                    cancellationToken);

        return createdBudgetAccount
            ?? throw new JsonException(
                "The API response did not contain a valid budget account.");
    }

    public async Task<BudgetAccount> UpdateAsync(
        int budgetAccountId,
        UpdateBudgetAccountRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(
            budgetAccountId);

        ArgumentNullException.ThrowIfNull(request);

        HttpClient httpClient =
            _httpClientFactory.CreateApiClient();

        string budgetAccountRoute =
            $"{BudgetAccountsRoute}/{budgetAccountId}";

        using HttpResponseMessage response =
            await httpClient.PatchAsJsonAsync(
                budgetAccountRoute,
                request,
                ApiHttpClient.JsonOptions,
                cancellationToken);

        await response.EnsureApiSuccessAsync(
            cancellationToken);

        BudgetAccount? updatedBudgetAccount =
            await response.Content.ReadFromJsonAsync<
                BudgetAccount>(
                    ApiHttpClient.JsonOptions,
                    cancellationToken);

        return updatedBudgetAccount ??
            throw new JsonException(
                "The API response did not contain a valid budget account.");
    }

    public async Task DeleteAsync(
        int budgetAccountId,
        CancellationToken cancellationToken = default)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(
            budgetAccountId);

        HttpClient httpClient =
            _httpClientFactory.CreateApiClient();

        string budgetAccountRoute =
            $"{BudgetAccountsRoute}/{budgetAccountId}";

        using HttpResponseMessage response =
            await httpClient.DeleteAsync(
                budgetAccountRoute,
                cancellationToken);

        await response.EnsureApiSuccessAsync(
            cancellationToken);
    }

}
