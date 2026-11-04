using System.Net;
using System.Net.Http.Json;

using ExpenseTracker.Api.Features.Budget.Accounts;
using ExpenseTracker.Api.Features.Budget.Logs;
using ExpenseTracker.Api.Features.Expenses;
using ExpenseTracker.Api.Features.Items;
using ExpenseTracker.Api.Tests.Features.Budget.Helpers;
using ExpenseTracker.Api.Tests.Features.Expenses.Helpers;
using ExpenseTracker.Api.Tests.Infrastructure;

namespace ExpenseTracker.Api.Tests.Features.Budget.Logs.Api;

public sealed class GetBudgetAccountLogByExpenseEntryIdApiTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public GetBudgetAccountLogByExpenseEntryIdApiTests(
        CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetLogByExpenseEntryIdAsync_WithBudgetExpense_ShouldReturnExpenseLog()
    {
        Item item =
            await ExpenseApiTestHelper.CreateItemAsync(
                _client,
                unitPrice: 125.00m);

        BudgetAccount budgetAccount =
            await BudgetApiTestHelper.CreateBudgetAccountAsync(
                _client,
                balance: 1000.00m);

        try
        {
            ExpenseResult expense =
                await BudgetApiTestHelper
                    .CreateExpenseWithBudgetAccountAsync(
                        _client,
                        budgetAccount.BudgetAccountId,
                        item.ItemId,
                        quantity: 2.000m);

            HttpResponseMessage response =
                await _client.GetAsync(
                    $"/api/v1/budget/logs/expense-entries/" +
                    $"{expense.ExpenseEntry.ExpenseEntryId}",
                    TestContext.Current.CancellationToken);

            Assert.Equal(
                HttpStatusCode.OK,
                response.StatusCode);

            BudgetAccountLog? log =
                await response.Content.ReadFromJsonAsync<
                    BudgetAccountLog>(
                        TestJsonSerializerOptions.Default,
                        TestContext.Current.CancellationToken);

            Assert.NotNull(log);

            Assert.Equal(
                budgetAccount.BudgetAccountId,
                log.BudgetAccountId);

            Assert.Equal(
                expense.ExpenseEntry.ExpenseEntryId,
                log.ExpenseEntryId);

            Assert.Equal(
                -250.00m,
                log.Amount);

            Assert.Equal(
                1000.00m,
                log.BalanceBefore);

            Assert.Equal(
                750.00m,
                log.BalanceAfter);

            Assert.Equal(
                BudgetAccountLogType.Expense,
                log.LogType);
        }
        finally
        {
            await BudgetApiTestHelper
                .DeleteBudgetAccountIfPresentAsync(
                    _client,
                    budgetAccount.BudgetAccountId);

            await ExpenseApiTestHelper.DeleteItemIfPresentAsync(
                _client,
                item.ItemId);
        }
    }

    [Fact]
    public async Task GetLogByExpenseEntryIdAsync_WithMissingExpenseEntry_ShouldReturnNotFound()
    {
        const int missingExpenseEntryId = int.MaxValue;

        HttpResponseMessage response =
            await _client.GetAsync(
                $"/api/v1/budget/logs/expense-entries/" +
                $"{missingExpenseEntryId}",
                TestContext.Current.CancellationToken);

        Assert.Equal(
            HttpStatusCode.NotFound,
            response.StatusCode);
    }
}
