using System.Net;
using System.Net.Http.Json;

using ExpenseTracker.Api.Features.Budget.Accounts;
using ExpenseTracker.Api.Features.Budget.Logs;
using ExpenseTracker.Api.Tests.Features.Budget.Helpers;
using ExpenseTracker.Api.Tests.Infrastructure;

namespace ExpenseTracker.Api.Tests.Features.Budget.Logs.Api;

public sealed class GetBudgetAccountLogsApiTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public GetBudgetAccountLogsApiTests(
        CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnOkWithBudgetAccountLogs()
    {
        HttpResponseMessage response =
            await _client.GetAsync(
                "/api/v1/budget/logs",
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
    }

    [Fact]
    public async Task GetAllAsync_WithCreatedBudgetAccount_ShouldIncludeStartingBalanceLog()
    {
        BudgetAccount budgetAccount =
            await BudgetApiTestHelper.CreateBudgetAccountAsync(
                _client,
                balance: 625.00m);

        try
        {
            HttpResponseMessage response =
                await _client.GetAsync(
                    "/api/v1/budget/logs",
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

            BudgetAccountLog startingBalanceLog =
                Assert.Single(
                    logs,
                    log =>
                        log.BudgetAccountId ==
                            budgetAccount.BudgetAccountId &&
                        log.LogType ==
                            BudgetAccountLogType.StartingBalance);

            Assert.Equal(
                625.00m,
                startingBalanceLog.Amount);

            Assert.Equal(
                0m,
                startingBalanceLog.BalanceBefore);

            Assert.Equal(
                625.00m,
                startingBalanceLog.BalanceAfter);
        }
        finally
        {
            await BudgetApiTestHelper
                .DeleteBudgetAccountIfPresentAsync(
                    _client,
                    budgetAccount.BudgetAccountId);
        }
    }
}
