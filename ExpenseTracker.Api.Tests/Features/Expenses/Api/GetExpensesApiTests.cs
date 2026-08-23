using System.Net;
using System.Net.Http.Json;

using ExpenseTracker.Api.Features.Expenses;
using ExpenseTracker.Api.Features.Expenses.Items;
using ExpenseTracker.Api.Features.Expenses.Items.Requests;
using ExpenseTracker.Api.Features.Items;
using ExpenseTracker.Api.Tests.Features.Expenses.Helpers;
using ExpenseTracker.Api.Tests.Infrastructure;

namespace ExpenseTracker.Api.Tests.Features.Expenses.Api;

public sealed class GetExpensesApiTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public GetExpensesApiTests(
        CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnOkWithExpenses()
    {
        HttpResponseMessage response =
            await _client.GetAsync(
                "/api/v1/expenses",
                TestContext.Current.CancellationToken);

        Assert.Equal(
            HttpStatusCode.OK,
            response.StatusCode);

        IReadOnlyList<ExpenseResult>? expenses =
            await response.Content.ReadFromJsonAsync<
                IReadOnlyList<ExpenseResult>>(
                    TestContext.Current.CancellationToken);

        Assert.NotNull(expenses);
    }

    [Fact]
    public async Task GetAllAsync_WithCreatedExpense_ShouldIncludeCompleteExpense()
    {
        Item firstItem =
            await ExpenseApiTestHelper.CreateItemAsync(
                _client,
                unitPrice: 20.00m);

        Item secondItem =
            await ExpenseApiTestHelper.CreateItemAsync(
                _client,
                unitPrice: 5.50m);

        string title =
            $"Office Supplies {Guid.NewGuid():N}";

        string notes =
            $"Get all expense test {Guid.NewGuid():N}";

        try
        {
            ExpenseResult createdExpense =
                await ExpenseApiTestHelper.CreateExpenseAsync(
                    _client,
                    title: title,
                    items:
                    [
                        new CreateExpenseItemRequest
                        {
                            ItemId = firstItem.ItemId,
                            Quantity = 2.000m
                        },
                        new CreateExpenseItemRequest
                        {
                            ItemId = secondItem.ItemId,
                            Quantity = 4.000m
                        }
                    ],
                    notes: notes);

            HttpResponseMessage response =
                await _client.GetAsync(
                    "/api/v1/expenses",
                    TestContext.Current.CancellationToken);

            Assert.Equal(
                HttpStatusCode.OK,
                response.StatusCode);

            IReadOnlyList<ExpenseResult>? expenses =
                await response.Content.ReadFromJsonAsync<
                    IReadOnlyList<ExpenseResult>>(
                        TestContext.Current.CancellationToken);

            Assert.NotNull(expenses);

            ExpenseResult retrievedExpense =
                Assert.Single(
                    expenses,
                    expense =>
                        expense.ExpenseEntry.ExpenseEntryId ==
                        createdExpense.ExpenseEntry.ExpenseEntryId);

            Assert.Equal(
                title,
                retrievedExpense.ExpenseEntry.Title);

            Assert.Equal(
                notes,
                retrievedExpense.ExpenseEntry.Notes);

            Assert.Equal(
                62.00m,
                retrievedExpense.ExpenseEntry.TotalCost);

            Assert.Equal(
                2,
                retrievedExpense.ExpenseItems.Count);

            ExpenseItem firstExpenseItem =
                Assert.Single(
                    retrievedExpense.ExpenseItems,
                    item => item.ItemId == firstItem.ItemId);

            Assert.Equal(
                2.000m,
                firstExpenseItem.Quantity);

            Assert.Equal(
                40.00m,
                firstExpenseItem.LineTotal);

            ExpenseItem secondExpenseItem =
                Assert.Single(
                    retrievedExpense.ExpenseItems,
                    item => item.ItemId == secondItem.ItemId);

            Assert.Equal(
                4.000m,
                secondExpenseItem.Quantity);

            Assert.Equal(
                22.00m,
                secondExpenseItem.LineTotal);
        }
        finally
        {
            await ExpenseApiTestHelper.DeleteItemIfPresentAsync(
                _client,
                firstItem.ItemId);

            await ExpenseApiTestHelper.DeleteItemIfPresentAsync(
                _client,
                secondItem.ItemId);
        }
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnExpensesInDescendingCreationOrder()
    {
        Item item =
            await ExpenseApiTestHelper.CreateItemAsync(
                _client,
                unitPrice: 10.00m);

        string uniqueValue = Guid.NewGuid()
            .ToString("N");

        string firstTitle = 
            $"First Expense {uniqueValue}";

        string secondTitle =
            $"Second Expense {uniqueValue}";

        try
        {
            ExpenseResult firstExpense =
                await ExpenseApiTestHelper.CreateExpenseAsync(
                    client: _client,
                    title: firstTitle,
                    items:
                    [
                        new CreateExpenseItemRequest
                        {
                            ItemId = item.ItemId,
                            Quantity = 1.000m
                        }
                    ],
                    notes: $"First ordering test {Guid.NewGuid():N}");

            ExpenseResult secondExpense =
                await ExpenseApiTestHelper.CreateExpenseAsync(
                    client: _client,
                    title: secondTitle,
                    items:
                    [
                        new CreateExpenseItemRequest
                        {
                            ItemId = item.ItemId,
                            Quantity = 2.000m
                        }
                    ],
                    notes: $"Second ordering test {Guid.NewGuid():N}");

            HttpResponseMessage response =
                await _client.GetAsync(
                    "/api/v1/expenses",
                    TestContext.Current.CancellationToken);

            Assert.Equal(
                HttpStatusCode.OK,
                response.StatusCode);

            IReadOnlyList<ExpenseResult>? expenses =
                await response.Content.ReadFromJsonAsync<
                    IReadOnlyList<ExpenseResult>>(
                        TestContext.Current.CancellationToken);

            Assert.NotNull(expenses);

            int firstIndex = expenses
                .Select(
                    (expense, index) =>
                        new
                        {
                            Expense = expense,
                            Index = index
                        })
                .Single(result =>
                    result.Expense.ExpenseEntry.ExpenseEntryId ==
                    firstExpense.ExpenseEntry.ExpenseEntryId)
                .Index;

            int secondIndex = expenses
                .Select(
                    (expense, index) =>
                        new
                        {
                            Expense = expense,
                            Index = index
                        })
                .Single(result =>
                    result.Expense.ExpenseEntry.ExpenseEntryId ==
                    secondExpense.ExpenseEntry.ExpenseEntryId)
                .Index;

            Assert.True(secondIndex < firstIndex);
        }
        finally
        {
            await ExpenseApiTestHelper.DeleteItemIfPresentAsync(
                _client,
                item.ItemId);
        }
    }
}
