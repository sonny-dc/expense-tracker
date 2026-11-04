using System.Net;
using System.Net.Http.Json;

using ExpenseTracker.Api.Features.Budget.Accounts;
using ExpenseTracker.Api.Features.Budget.Logs;
using ExpenseTracker.Api.Tests.Features.Budget.Helpers;
using ExpenseTracker.Api.Tests.Infrastructure;

namespace ExpenseTracker.Api.Tests.Features.Budget.Logs.Api;

public sealed class GetBudgetAccountLogsByAccountIdApiTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public GetBudgetAccountLogsByAccountIdApiTests(
        CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetLogsByBudgetAccountIdAsync_WithExistingBudgetAccount_ShouldReturnLogs()
    {
        BudgetAccount budgetAccount =
            await BudgetApiTestHelper.CreateBudgetAccountAsync(
                _client,
                balance: 800.00m);

        try
        {
            HttpResponseMessage response =
                await _client.GetAsync(
                    $"/api/v1/budget/logs/accounts/" +
                    $"{budgetAccount.BudgetAccountId}",
                    TestContext.Current.CancellationToken);

            Assert.Equal(
                HttpStatusCode.OK,
                response.StatusCode);

            IReadOnlyList<BudgetAccountLog>? logs =
                await response.Content.ReadFromJsonAsync<
                    IReadOnlyList<BudgetAccountLog>>(
                        TestJsonSerializerOptions.Default,
                        TestContext.Current.CancellationToken);

            Assert.NotNull(logs);

            BudgetAccountLog log =
                Assert.Single(logs);

            Assert.Equal(
                budgetAccount.BudgetAccountId,
                log.BudgetAccountId);

            Assert.Null(
                log.ExpenseEntryId);

            Assert.Equal(
                800.00m,
                log.Amount);

            Assert.Equal(
                0m,
                log.BalanceBefore);

            Assert.Equal(
                800.00m,
                log.BalanceAfter);

            Assert.Equal(
                BudgetAccountLogType.StartingBalance,
                log.LogType);
        }
        finally
        {
            await BudgetApiTestHelper
                .DeleteBudgetAccountIfPresentAsync(
                    _client,
                    budgetAccount.BudgetAccountId);
        }
    }

    [Fact]
    public async Task GetLogsByBudgetAccountIdAsync_WithMissingBudgetAccount_ShouldReturnNotFound()
    {
        const int missingBudgetAccountId = int.MaxValue;

        HttpResponseMessage response =
            await _client.GetAsync(
                $"/api/v1/budget/logs/accounts/" +
                $"{missingBudgetAccountId}",
                TestContext.Current.CancellationToken);

        Assert.Equal(
            HttpStatusCode.NotFound,
            response.StatusCode);
    }
}
