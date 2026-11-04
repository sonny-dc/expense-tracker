using System.Net;
using System.Net.Http.Json;

using ExpenseTracker.Api.Tests.Infrastructure;

using ExpenseTracker.Api.Features.Budget.Accounts;
using ExpenseTracker.Api.Features.Budget.Accounts.Requests;
using ExpenseTracker.Api.Features.Budget.Logs;

using ExpenseTracker.Api.Features.Expenses;
using ExpenseTracker.Api.Features.Expenses.Items.Requests;
using ExpenseTracker.Api.Features.Expenses.Requests;

namespace ExpenseTracker.Api.Tests.Features.Budget.Helpers;

internal static class BudgetApiTestHelper
{
    public static async Task<BudgetAccount> CreateBudgetAccountAsync(
        HttpClient client,
        decimal balance = 1000.00m)
    {
        string uniqueValue = Guid.NewGuid()
            .ToString("N");

        var request = new CreateBudgetAccountRequest
        {
            Name = $"Budget Test Account {uniqueValue}",
            Description =
                $"Budget account integration test {uniqueValue}",
            Balance = balance
        };

        HttpResponseMessage response =
            await client.PostAsJsonAsync(
                "/api/v1/budget/accounts",
                request,
                TestJsonSerializerOptions.Default,
                TestContext.Current.CancellationToken);

        Assert.Equal(
            HttpStatusCode.Created,
            response.StatusCode);

        BudgetAccount? budgetAccount =
            await response.Content.ReadFromJsonAsync<BudgetAccount>(
                TestJsonSerializerOptions.Default,
                TestContext.Current.CancellationToken);

        Assert.NotNull(budgetAccount);

        return budgetAccount;
    }

    public static async Task<BudgetAccount> GetBudgetAccountAsync(
        HttpClient client,
        int budgetAccountId)
    {
        HttpResponseMessage response =
            await client.GetAsync(
                $"/api/v1/budget/accounts/{budgetAccountId}",
                TestContext.Current.CancellationToken);

        Assert.Equal(
            HttpStatusCode.OK,
            response.StatusCode);

        BudgetAccount? budgetAccount =
            await response.Content.ReadFromJsonAsync<BudgetAccount>(
                TestJsonSerializerOptions.Default,
                TestContext.Current.CancellationToken);

        Assert.NotNull(budgetAccount);

        return budgetAccount;
    }

    public static async Task<IReadOnlyList<BudgetAccountLog>>
        GetLogsByBudgetAccountIdAsync(
            HttpClient client,
            int budgetAccountId)
    {
        HttpResponseMessage response =
            await client.GetAsync(
                $"/api/v1/budget/logs/accounts/{budgetAccountId}",
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

        return logs;
    }

    public static async Task DeleteBudgetAccountIfPresentAsync(
        HttpClient client,
        int budgetAccountId)
    {
        await client.DeleteAsync(
            $"/api/v1/budget/accounts/{budgetAccountId}",
            TestContext.Current.CancellationToken);
    }

    public static async Task<ExpenseResult>
    CreateExpenseWithBudgetAccountAsync(
        HttpClient client,
        int budgetAccountId,
        int itemId,
        decimal quantity)
    {
        var request = new CreateExpenseRequest
        {
            Title =
                $"Budget Expense Test {Guid.NewGuid():N}",
            Notes = "Budget expense integration test",
            BudgetAccountId = budgetAccountId,
            Items =
            [
                new CreateExpenseItemRequest
            {
                ItemId = itemId,
                Quantity = quantity
            }
            ]
        };

        HttpResponseMessage response =
            await client.PostAsJsonAsync(
                "/api/v1/expenses",
                request,
                TestJsonSerializerOptions.Default,
                TestContext.Current.CancellationToken);

        Assert.Equal(
            HttpStatusCode.Created,
            response.StatusCode);

        ExpenseResult? expense =
            await response.Content.ReadFromJsonAsync<ExpenseResult>(
                TestJsonSerializerOptions.Default,
                TestContext.Current.CancellationToken);

        Assert.NotNull(expense);

        return expense;
    }
}
