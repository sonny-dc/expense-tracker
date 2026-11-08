using System.Net;
using System.Net.Http.Json;

using ExpenseTracker.Api.Features.Budget.Accounts;
using ExpenseTracker.Api.Features.Budget.Accounts.Requests;
using ExpenseTracker.Api.Features.Budget.Logs;
using ExpenseTracker.Api.Features.Expenses;
using ExpenseTracker.Api.Features.Expenses.Items.Requests;
using ExpenseTracker.Api.Features.Expenses.Requests;
using ExpenseTracker.Api.Features.Items;
using ExpenseTracker.Api.Tests.Features.Budget.Helpers;
using ExpenseTracker.Api.Tests.Features.Expenses.Helpers;
using ExpenseTracker.Api.Tests.Infrastructure;

namespace ExpenseTracker.Api.Tests.Features.Expenses.Api;

public sealed class CreateExpenseWithBudgetAccountApiTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public CreateExpenseWithBudgetAccountApiTests(
        CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CreateAsync_WithOpenBudgetAccount_ShouldReturnBudgetExpenseResult()
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

            Assert.Equal(
                250.00m,
                expense.ExpenseEntry.TotalCost);

            Assert.NotNull(
                expense.BudgetExpenseResult);

            BudgetAccount resultAccount =
                expense.BudgetExpenseResult.BudgetAccount;

            Assert.Equal(
                budgetAccount.BudgetAccountId,
                resultAccount.BudgetAccountId);

            Assert.Equal(
                budgetAccount.Name,
                resultAccount.Name);

            Assert.Equal(
                budgetAccount.Description,
                resultAccount.Description);

            Assert.Equal(
                750.00m,
                resultAccount.Balance);

            Assert.Equal(
                BudgetAccountStatus.Open,
                resultAccount.Status);

            BudgetAccountLog resultLog =
                expense.BudgetExpenseResult.BudgetAccountLog;

            Assert.True(
                resultLog.BudgetAccountLogId > 0);

            Assert.Equal(
                budgetAccount.BudgetAccountId,
                resultLog.BudgetAccountId);

            Assert.Equal(
                expense.ExpenseEntry.ExpenseEntryId,
                resultLog.ExpenseEntryId);

            Assert.Equal(
                -250.00m,
                resultLog.Amount);

            Assert.Equal(
                1000.00m,
                resultLog.BalanceBefore);

            Assert.Equal(
                750.00m,
                resultLog.BalanceAfter);

            Assert.Equal(
                BudgetAccountLogType.Expense,
                resultLog.LogType);

            BudgetAccount persistedAccount =
                await BudgetApiTestHelper.GetBudgetAccountAsync(
                    _client,
                    budgetAccount.BudgetAccountId);

            Assert.Equal(
                resultAccount.Balance,
                persistedAccount.Balance);
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
    public async Task CreateAsync_WithOpenBudgetAccount_ShouldPersistReturnedExpenseActivity()
    {
        Item item =
            await ExpenseApiTestHelper.CreateItemAsync(
                _client,
                unitPrice: 100.00m);

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

            Assert.NotNull(
                expense.BudgetExpenseResult);

            BudgetAccountLog returnedLog =
                expense.BudgetExpenseResult.BudgetAccountLog;

            IReadOnlyList<BudgetAccountLog> logs =
                await BudgetApiTestHelper
                    .GetLogsByBudgetAccountIdAsync(
                        _client,
                        budgetAccount.BudgetAccountId);

            Assert.Equal(
                2,
                logs.Count);

            BudgetAccountLog persistedLog =
                Assert.Single(
                    logs,
                    log =>
                        log.LogType ==
                        BudgetAccountLogType.Expense);

            Assert.Equal(
                returnedLog.BudgetAccountLogId,
                persistedLog.BudgetAccountLogId);

            Assert.Equal(
                returnedLog.BudgetAccountId,
                persistedLog.BudgetAccountId);

            Assert.Equal(
                returnedLog.ExpenseEntryId,
                persistedLog.ExpenseEntryId);

            Assert.Equal(
                returnedLog.Amount,
                persistedLog.Amount);

            Assert.Equal(
                returnedLog.BalanceBefore,
                persistedLog.BalanceBefore);

            Assert.Equal(
                returnedLog.BalanceAfter,
                persistedLog.BalanceAfter);

            Assert.Equal(
                returnedLog.LogType,
                persistedLog.LogType);
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
    public async Task CreateAsync_WithExpenseExceedingBalance_ShouldAllowNegativeBalance()
    {
        Item item =
            await ExpenseApiTestHelper.CreateItemAsync(
                _client,
                unitPrice: 600.00m);

        BudgetAccount budgetAccount =
            await BudgetApiTestHelper.CreateBudgetAccountAsync(
                _client,
                balance: 500.00m);

        try
        {
            ExpenseResult expense =
                await BudgetApiTestHelper
                    .CreateExpenseWithBudgetAccountAsync(
                        _client,
                        budgetAccount.BudgetAccountId,
                        item.ItemId,
                        quantity: 1.000m);

            Assert.Equal(
                600.00m,
                expense.ExpenseEntry.TotalCost);

            Assert.NotNull(
                expense.BudgetExpenseResult);

            Assert.Equal(
                -100.00m,
                expense.BudgetExpenseResult
                    .BudgetAccount
                    .Balance);

            Assert.Equal(
                -600.00m,
                expense.BudgetExpenseResult
                    .BudgetAccountLog
                    .Amount);

            Assert.Equal(
                500.00m,
                expense.BudgetExpenseResult
                    .BudgetAccountLog
                    .BalanceBefore);

            Assert.Equal(
                -100.00m,
                expense.BudgetExpenseResult
                    .BudgetAccountLog
                    .BalanceAfter);

            BudgetAccount updatedAccount =
                await BudgetApiTestHelper.GetBudgetAccountAsync(
                    _client,
                    budgetAccount.BudgetAccountId);

            Assert.Equal(
                -100.00m,
                updatedAccount.Balance);

            IReadOnlyList<BudgetAccountLog> logs =
                await BudgetApiTestHelper
                    .GetLogsByBudgetAccountIdAsync(
                        _client,
                        budgetAccount.BudgetAccountId);

            BudgetAccountLog expenseLog =
                Assert.Single(
                    logs,
                    log =>
                        log.LogType ==
                        BudgetAccountLogType.Expense);

            Assert.Equal(
                -600.00m,
                expenseLog.Amount);

            Assert.Equal(
                500.00m,
                expenseLog.BalanceBefore);

            Assert.Equal(
                -100.00m,
                expenseLog.BalanceAfter);
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
    public async Task CreateAsync_WithArchivedBudgetAccount_ShouldReturnConflictAndRollbackExpense()
    {
        Item item =
            await ExpenseApiTestHelper.CreateItemAsync(
                _client,
                unitPrice: 100.00m);

        BudgetAccount budgetAccount =
            await BudgetApiTestHelper.CreateBudgetAccountAsync(
                _client,
                balance: 1000.00m);

        string expenseTitle =
            $"Archived Budget Expense {Guid.NewGuid():N}";

        try
        {
            var archiveRequest = new UpdateBudgetAccountRequest
            {
                Status = BudgetAccountStatus.Archived
            };

            HttpResponseMessage archiveResponse =
                await _client.PatchAsJsonAsync(
                    $"/api/v1/budget/accounts/" +
                    $"{budgetAccount.BudgetAccountId}",
                    archiveRequest,
                    TestJsonSerializerOptions.Default,
                    TestContext.Current.CancellationToken);

            Assert.Equal(
                HttpStatusCode.OK,
                archiveResponse.StatusCode);

            var request = new CreateExpenseRequest
            {
                Title = expenseTitle,
                Notes = "Archived budget rollback test",
                BudgetAccountId =
                    budgetAccount.BudgetAccountId,
                Items =
                [
                    new CreateExpenseItemRequest
                    {
                        ItemId = item.ItemId,
                        Quantity = 1.000m
                    }
                ]
            };

            HttpResponseMessage response =
                await _client.PostAsJsonAsync(
                    "/api/v1/expenses",
                    request,
                    TestContext.Current.CancellationToken);

            Assert.Equal(
                HttpStatusCode.Conflict,
                response.StatusCode);

            HttpResponseMessage expensesResponse =
                await _client.GetAsync(
                    "/api/v1/expenses",
                    TestContext.Current.CancellationToken);

            Assert.Equal(
                HttpStatusCode.OK,
                expensesResponse.StatusCode);

            IReadOnlyList<ExpenseResult>? expenses =
                await expensesResponse.Content.ReadFromJsonAsync<
                    IReadOnlyList<ExpenseResult>>(
                        TestContext.Current.CancellationToken);

            Assert.NotNull(expenses);

            Assert.DoesNotContain(
                expenses,
                expense =>
                    expense.ExpenseEntry.Title ==
                    expenseTitle);

            BudgetAccount unchangedAccount =
                await BudgetApiTestHelper.GetBudgetAccountAsync(
                    _client,
                    budgetAccount.BudgetAccountId);

            Assert.Equal(
                1000.00m,
                unchangedAccount.Balance);

            Assert.Equal(
                BudgetAccountStatus.Archived,
                unchangedAccount.Status);

            IReadOnlyList<BudgetAccountLog> logs =
                await BudgetApiTestHelper
                    .GetLogsByBudgetAccountIdAsync(
                        _client,
                        budgetAccount.BudgetAccountId);

            Assert.Single(logs);
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
    public async Task CreateAsync_WithSettledBudgetAccount_ShouldReturnConflictAndRollbackExpense()
    {
        Item item =
            await ExpenseApiTestHelper.CreateItemAsync(
                _client,
                unitPrice: 100.00m);

        BudgetAccount budgetAccount =
            await BudgetApiTestHelper.CreateBudgetAccountAsync(
                _client,
                balance: 1000.00m);

        string expenseTitle =
            $"Settled Budget Expense {Guid.NewGuid():N}";

        try
        {
            var settleRequest = new UpdateBudgetAccountRequest
            {
                Status = BudgetAccountStatus.Settled
            };

            HttpResponseMessage settleResponse =
                await _client.PatchAsJsonAsync(
                    $"/api/v1/budget/accounts/" +
                    $"{budgetAccount.BudgetAccountId}",
                    settleRequest,
                    TestJsonSerializerOptions.Default,
                    TestContext.Current.CancellationToken);

            Assert.Equal(
                HttpStatusCode.OK,
                settleResponse.StatusCode);

            var request = new CreateExpenseRequest
            {
                Title = expenseTitle,
                Notes = "Settled budget rollback test",
                BudgetAccountId =
                    budgetAccount.BudgetAccountId,
                Items =
                [
                    new CreateExpenseItemRequest
                    {
                        ItemId = item.ItemId,
                        Quantity = 1.000m
                    }
                ]
            };

            HttpResponseMessage response =
                await _client.PostAsJsonAsync(
                    "/api/v1/expenses",
                    request,
                    TestContext.Current.CancellationToken);

            Assert.Equal(
                HttpStatusCode.Conflict,
                response.StatusCode);

            HttpResponseMessage expensesResponse =
                await _client.GetAsync(
                    "/api/v1/expenses",
                    TestContext.Current.CancellationToken);

            Assert.Equal(
                HttpStatusCode.OK,
                expensesResponse.StatusCode);

            IReadOnlyList<ExpenseResult>? expenses =
                await expensesResponse.Content.ReadFromJsonAsync<
                    IReadOnlyList<ExpenseResult>>(
                        TestContext.Current.CancellationToken);

            Assert.NotNull(expenses);

            Assert.DoesNotContain(
                expenses,
                expense =>
                    expense.ExpenseEntry.Title ==
                    expenseTitle);
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
    public async Task CreateAsync_WithMissingBudgetAccount_ShouldReturnNotFoundAndRollbackExpense()
    {
        Item item =
            await ExpenseApiTestHelper.CreateItemAsync(
                _client,
                unitPrice: 100.00m);

        const int missingBudgetAccountId = int.MaxValue;

        string expenseTitle =
            $"Missing Budget Expense {Guid.NewGuid():N}";

        var request = new CreateExpenseRequest
        {
            Title = expenseTitle,
            Notes = "Missing budget account rollback test",
            BudgetAccountId = missingBudgetAccountId,
            Items =
            [
                new CreateExpenseItemRequest
                {
                    ItemId = item.ItemId,
                    Quantity = 1.000m
                }
            ]
        };

        try
        {
            HttpResponseMessage response =
                await _client.PostAsJsonAsync(
                    "/api/v1/expenses",
                    request,
                    TestContext.Current.CancellationToken);

            Assert.Equal(
                HttpStatusCode.NotFound,
                response.StatusCode);

            HttpResponseMessage expensesResponse =
                await _client.GetAsync(
                    "/api/v1/expenses",
                    TestContext.Current.CancellationToken);

            Assert.Equal(
                HttpStatusCode.OK,
                expensesResponse.StatusCode);

            IReadOnlyList<ExpenseResult>? expenses =
                await expensesResponse.Content.ReadFromJsonAsync<
                    IReadOnlyList<ExpenseResult>>(
                        TestContext.Current.CancellationToken);

            Assert.NotNull(expenses);

            Assert.DoesNotContain(
                expenses,
                expense =>
                    expense.ExpenseEntry.Title ==
                    expenseTitle);
        }
        finally
        {
            await ExpenseApiTestHelper.DeleteItemIfPresentAsync(
                _client,
                item.ItemId);
        }
    }
}
