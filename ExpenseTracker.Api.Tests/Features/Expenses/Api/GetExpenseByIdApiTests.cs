using System.Net;
using System.Net.Http.Json;

using ExpenseTracker.Api.Features.Expenses;
using ExpenseTracker.Api.Features.Expenses.Items;
using ExpenseTracker.Api.Features.Expenses.Items.Requests;
using ExpenseTracker.Api.Features.Items;
using ExpenseTracker.Api.Tests.Features.Expenses.Helpers;
using ExpenseTracker.Api.Tests.Infrastructure;

namespace ExpenseTracker.Api.Tests.Features.Expenses.Api;

public sealed class GetExpenseByIdApiTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public GetExpenseByIdApiTests(
        CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetByIdAsync_WithExistingExpense_ShouldReturnCompleteExpense()
    {
        Item item =
            await ExpenseApiTestHelper.CreateItemAsync(
                _client,
                unitPrice: 80.00m);

        string uniqueValue = Guid.NewGuid()
            .ToString("N");

        string title = 
            $"Test Expense {uniqueValue}";

        string notes =
            $"Get expense test {uniqueValue}";

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
                            ItemId = item.ItemId,
                            Quantity = 2.500m
                        }
                    ],
                    notes: notes);

            HttpResponseMessage response =
                await _client.GetAsync(
                    $"/api/v1/expenses/" +
                    $"{createdExpense.ExpenseEntry.ExpenseEntryId}",
                    TestContext.Current.CancellationToken);

            Assert.Equal(
                HttpStatusCode.OK,
                response.StatusCode);

            ExpenseResult? retrievedExpense =
                await response.Content.ReadFromJsonAsync<ExpenseResult>(
                    TestContext.Current.CancellationToken);

            Assert.NotNull(retrievedExpense);

            Assert.Equal(
                createdExpense.ExpenseEntry.ExpenseEntryId,
                retrievedExpense.ExpenseEntry.ExpenseEntryId);

            Assert.Equal(
                title,
                retrievedExpense.ExpenseEntry.Title);

            Assert.Equal(
                notes,
                retrievedExpense.ExpenseEntry.Notes);

            Assert.Equal(
                200.00m,
                retrievedExpense.ExpenseEntry.TotalCost);

            ExpenseItem expenseItem =
                Assert.Single(
                    retrievedExpense.ExpenseItems);

            Assert.Equal(
                item.ItemId,
                expenseItem.ItemId);

            Assert.Equal(
                item.Name,
                expenseItem.ItemNameSnapshot);

            Assert.Equal(
                item.Code,
                expenseItem.ItemCodeSnapshot);

            Assert.Equal(
                item.Brand,
                expenseItem.BrandSnapshot);

            Assert.Equal(
                2.500m,
                expenseItem.Quantity);

            Assert.Equal(
                item.UnitPrice,
                expenseItem.UnitPriceSnapshot);

            Assert.Equal(
                200.00m,
                expenseItem.LineTotal);
        }
        finally
        {
            await ExpenseApiTestHelper.DeleteItemIfPresentAsync(
                _client,
                item.ItemId);
        }
    }

    [Fact]
    public async Task GetByIdAsync_AfterSourceItemDeletion_ShouldReturnSnapshotsWithNullItemId()
    {
        Item item =
            await ExpenseApiTestHelper.CreateItemAsync(
                _client,
                unitPrice: 40.00m);

        string title = 
            $"Historical Expense {Guid.NewGuid():N}";

        ExpenseResult createdExpense =
            await ExpenseApiTestHelper.CreateExpenseAsync(
                _client,
                title: title,
                items:
                [
                    new CreateExpenseItemRequest
                    {
                        ItemId = item.ItemId,
                        Quantity = 3.000m
                    }
                ],
                notes: "Historical snapshot test");

        HttpResponseMessage deleteResponse =
            await _client.DeleteAsync(
                $"/api/v1/items/{item.ItemId}",
                TestContext.Current.CancellationToken);

        Assert.Equal(
            HttpStatusCode.NoContent,
            deleteResponse.StatusCode);

        HttpResponseMessage response =
            await _client.GetAsync(
                $"/api/v1/expenses/" +
                $"{createdExpense.ExpenseEntry.ExpenseEntryId}",
                TestContext.Current.CancellationToken);

        Assert.Equal(
            HttpStatusCode.OK,
            response.StatusCode);

        ExpenseResult? retrievedExpense =
            await response.Content.ReadFromJsonAsync<ExpenseResult>(
                TestContext.Current.CancellationToken);

        Assert.NotNull(retrievedExpense);

        Assert.Equal(
            title,
            retrievedExpense.ExpenseEntry.Title);

        ExpenseItem expenseItem =
            Assert.Single(
                retrievedExpense.ExpenseItems);

        Assert.Null(expenseItem.ItemId);

        Assert.Equal(
            item.Name,
            expenseItem.ItemNameSnapshot);

        Assert.Equal(
            item.Code,
            expenseItem.ItemCodeSnapshot);

        Assert.Equal(
            item.Brand,
            expenseItem.BrandSnapshot);

        Assert.Equal(
            item.UnitPrice,
            expenseItem.UnitPriceSnapshot);

        Assert.Equal(
            3.000m,
            expenseItem.Quantity);

        Assert.Equal(
            120.00m,
            expenseItem.LineTotal);
    }

    [Fact]
    public async Task GetByIdAsync_WithMissingExpense_ShouldReturnNotFound()
    {
        const int missingExpenseEntryId = int.MaxValue;

        HttpResponseMessage response =
            await _client.GetAsync(
                $"/api/v1/expenses/{missingExpenseEntryId}",
                TestContext.Current.CancellationToken);

        Assert.Equal(
            HttpStatusCode.NotFound,
            response.StatusCode);
    }
}
