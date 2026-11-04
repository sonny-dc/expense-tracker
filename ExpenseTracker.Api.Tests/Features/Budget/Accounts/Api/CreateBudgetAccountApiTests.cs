using System.Net;
using System.Net.Http.Json;

using ExpenseTracker.Api.Features.Budget.Accounts;
using ExpenseTracker.Api.Features.Budget.Accounts.Requests;
using ExpenseTracker.Api.Features.Budget.Logs;
using ExpenseTracker.Api.Tests.Features.Budget.Helpers;
using ExpenseTracker.Api.Tests.Infrastructure;

namespace ExpenseTracker.Api.Tests.Features.Budget.Accounts.Api;

public sealed class CreateBudgetAccountApiTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public CreateBudgetAccountApiTests(
        CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CreateAsync_WithValidRequest_ShouldReturnCreatedBudgetAccount()
    {
        string uniqueValue = Guid.NewGuid()
            .ToString("N");

        var request = new CreateBudgetAccountRequest
        {
            Name = $"Operating Budget {uniqueValue}",
            Description =
                "Valid budget account creation test",
            Balance = 1500.00m
        };

        DateTime beforeCreation = DateTime.UtcNow;

        HttpResponseMessage response =
            await _client.PostAsJsonAsync(
                "/api/v1/budget/accounts",
                request,
                TestContext.Current.CancellationToken);

        DateTime afterCreation = DateTime.UtcNow;

        Assert.Equal(
            HttpStatusCode.Created,
            response.StatusCode);

        BudgetAccount? budgetAccount =
            await response.Content.ReadFromJsonAsync<BudgetAccount>(
                TestJsonSerializerOptions.Default,
                TestContext.Current.CancellationToken);

        Assert.NotNull(budgetAccount);

        try
        {
            Assert.True(
                budgetAccount.BudgetAccountId > 0);

            Assert.Equal(
                request.Name,
                budgetAccount.Name);

            Assert.Equal(
                request.Description,
                budgetAccount.Description);

            Assert.Equal(
                request.Balance,
                budgetAccount.Balance);

            Assert.Equal(
                BudgetAccountStatus.Open,
                budgetAccount.Status);

            Assert.InRange(
                budgetAccount.CreatedDateTime,
                beforeCreation.AddSeconds(-1),
                afterCreation.AddSeconds(1));

            Assert.InRange(
                budgetAccount.LastUpdatedDateTime,
                beforeCreation.AddSeconds(-1),
                afterCreation.AddSeconds(1));

            Assert.NotNull(
                response.Headers.Location);

            Assert.EndsWith(
                $"/api/v1/budget/accounts/" +
                $"{budgetAccount.BudgetAccountId}",
                response.Headers.Location.ToString());
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
    public async Task CreateAsync_WithValidRequest_ShouldCreateStartingBalanceLog()
    {
        BudgetAccount budgetAccount =
            await BudgetApiTestHelper.CreateBudgetAccountAsync(
                _client,
                balance: 1250.00m);

        try
        {
            IReadOnlyList<BudgetAccountLog> logs =
                await BudgetApiTestHelper
                    .GetLogsByBudgetAccountIdAsync(
                        _client,
                        budgetAccount.BudgetAccountId);

            BudgetAccountLog log =
                Assert.Single(logs);

            Assert.True(
                log.BudgetAccountLogId > 0);

            Assert.Equal(
                budgetAccount.BudgetAccountId,
                log.BudgetAccountId);

            Assert.Null(
                log.ExpenseEntryId);

            Assert.Equal(
                1250.00m,
                log.Amount);

            Assert.Equal(
                0m,
                log.BalanceBefore);

            Assert.Equal(
                1250.00m,
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
    public async Task CreateAsync_WithNegativeBalance_ShouldCreateNegativeStartingBalanceLog()
    {
        BudgetAccount budgetAccount =
            await BudgetApiTestHelper.CreateBudgetAccountAsync(
                _client,
                balance: -500.00m);

        try
        {
            Assert.Equal(
                -500.00m,
                budgetAccount.Balance);

            IReadOnlyList<BudgetAccountLog> logs =
                await BudgetApiTestHelper
                    .GetLogsByBudgetAccountIdAsync(
                        _client,
                        budgetAccount.BudgetAccountId);

            BudgetAccountLog log =
                Assert.Single(logs);

            Assert.Equal(
                -500.00m,
                log.Amount);

            Assert.Equal(
                0m,
                log.BalanceBefore);

            Assert.Equal(
                -500.00m,
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
    public async Task CreateAsync_WithUnknownProperty_ShouldReturnBadRequest()
    {
        var request = new
        {
            name = "Unknown Property Test",
            description = "Unknown property rejection test",
            balance = 1000.00m,
            unexpectedProperty = true
        };

        HttpResponseMessage response =
            await _client.PostAsJsonAsync(
                "/api/v1/budget/accounts",
                request,
                TestContext.Current.CancellationToken);

        Assert.Equal(
            HttpStatusCode.BadRequest,
            response.StatusCode);
    }
}
