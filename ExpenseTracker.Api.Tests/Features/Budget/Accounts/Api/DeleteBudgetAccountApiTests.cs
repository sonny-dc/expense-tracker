using System.Net;

using ExpenseTracker.Api.Features.Budget.Accounts;
using ExpenseTracker.Api.Features.Budget.Logs;
using ExpenseTracker.Api.Tests.Features.Budget.Helpers;
using ExpenseTracker.Api.Tests.Infrastructure;

namespace ExpenseTracker.Api.Tests.Features.Budget.Accounts.Api;

public sealed class DeleteBudgetAccountApiTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public DeleteBudgetAccountApiTests(
        CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task DeleteAsync_WithExistingBudgetAccount_ShouldReturnNoContent()
    {
        BudgetAccount budgetAccount =
            await BudgetApiTestHelper.CreateBudgetAccountAsync(
                _client);

        HttpResponseMessage response =
            await _client.DeleteAsync(
                $"/api/v1/budget/accounts/" +
                $"{budgetAccount.BudgetAccountId}",
                TestContext.Current.CancellationToken);

        Assert.Equal(
            HttpStatusCode.NoContent,
            response.StatusCode);
    }

    [Fact]
    public async Task DeleteAsync_WithExistingBudgetAccount_ShouldRemoveBudgetAccount()
    {
        BudgetAccount budgetAccount =
            await BudgetApiTestHelper.CreateBudgetAccountAsync(
                _client);

        HttpResponseMessage deleteResponse =
            await _client.DeleteAsync(
                $"/api/v1/budget/accounts/" +
                $"{budgetAccount.BudgetAccountId}",
                TestContext.Current.CancellationToken);

        Assert.Equal(
            HttpStatusCode.NoContent,
            deleteResponse.StatusCode);

        HttpResponseMessage getResponse =
            await _client.GetAsync(
                $"/api/v1/budget/accounts/" +
                $"{budgetAccount.BudgetAccountId}",
                TestContext.Current.CancellationToken);

        Assert.Equal(
            HttpStatusCode.NotFound,
            getResponse.StatusCode);
    }

    [Fact]
    public async Task DeleteAsync_WithExistingBudgetAccount_ShouldRemoveAssociatedLogs()
    {
        BudgetAccount budgetAccount =
            await BudgetApiTestHelper.CreateBudgetAccountAsync(
                _client);

        IReadOnlyList<BudgetAccountLog> logs =
            await BudgetApiTestHelper
                .GetLogsByBudgetAccountIdAsync(
                    _client,
                    budgetAccount.BudgetAccountId);

        Assert.Single(logs);

        HttpResponseMessage deleteResponse =
            await _client.DeleteAsync(
                $"/api/v1/budget/accounts/" +
                $"{budgetAccount.BudgetAccountId}",
                TestContext.Current.CancellationToken);

        Assert.Equal(
            HttpStatusCode.NoContent,
            deleteResponse.StatusCode);

        HttpResponseMessage logsResponse =
            await _client.GetAsync(
                $"/api/v1/budget/logs/accounts/" +
                $"{budgetAccount.BudgetAccountId}",
                TestContext.Current.CancellationToken);

        Assert.Equal(
            HttpStatusCode.NotFound,
            logsResponse.StatusCode);
    }

    [Fact]
    public async Task DeleteAsync_WithMissingBudgetAccount_ShouldReturnNotFound()
    {
        const int missingBudgetAccountId = int.MaxValue;

        HttpResponseMessage response =
            await _client.DeleteAsync(
                $"/api/v1/budget/accounts/" +
                $"{missingBudgetAccountId}",
                TestContext.Current.CancellationToken);

        Assert.Equal(
            HttpStatusCode.NotFound,
            response.StatusCode);
    }
}
