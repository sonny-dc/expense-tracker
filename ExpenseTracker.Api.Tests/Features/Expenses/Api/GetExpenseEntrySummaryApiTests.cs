using System.Net;
using System.Net.Http.Json;

using ExpenseTracker.Api.Features.Expenses.Entries;
using ExpenseTracker.Api.Features.Expenses.Items.Requests;
using ExpenseTracker.Api.Features.Items;
using ExpenseTracker.Api.Tests.Features.Expenses.Helpers;
using ExpenseTracker.Api.Tests.Infrastructure;

namespace ExpenseTracker.Api.Tests.Features.Expenses.Entries.Api;

public sealed class GetExpenseEntrySummaryApiTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private const string SummaryRoute =
        "/api/v1/expenses/entries/summary";

    private readonly HttpClient _client;

    public GetExpenseEntrySummaryApiTests(
        CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetSummaryAsync_ShouldReturnOkWithSummary()
    {
        HttpResponseMessage response =
            await _client.GetAsync(
                SummaryRoute,
                TestContext.Current.CancellationToken);

        Assert.Equal(
            HttpStatusCode.OK,
            response.StatusCode);

        ExpenseEntrySummary? summary =
            await response.Content.ReadFromJsonAsync<
                ExpenseEntrySummary>(
                    TestContext.Current.CancellationToken);

        Assert.NotNull(summary);

        Assert.True(
            summary.ExpenseCount >= 0);

        Assert.True(
            summary.TotalCost >= 0);
    }

    [Fact]
    public async Task GetSummaryAsync_AfterCreatingExpense_ShouldIncludeExpense()
    {
        ExpenseEntrySummary initialSummary =
            await GetSummaryAsync();

        Item item =
            await ExpenseApiTestHelper.CreateItemAsync(
                _client,
                unitPrice: 25.00m);

        try
        {
            await ExpenseApiTestHelper.CreateExpenseAsync(
                client: _client,
                title:
                    $"Expense Summary Test {Guid.NewGuid():N}",
                items:
                [
                    new CreateExpenseItemRequest
                    {
                        ItemId = item.ItemId,
                        Quantity = 2.000m
                    }
                ],
                notes:
                    "Expense Entry summary integration test");

            ExpenseEntrySummary updatedSummary =
                await GetSummaryAsync();

            Assert.True(
                updatedSummary.ExpenseCount >=
                initialSummary.ExpenseCount + 1);

            Assert.True(
                updatedSummary.TotalCost >=
                initialSummary.TotalCost + 50.00m);
        }
        finally
        {
            await ExpenseApiTestHelper.DeleteItemIfPresentAsync(
                _client,
                item.ItemId);
        }
    }

    private async Task<ExpenseEntrySummary> GetSummaryAsync()
    {
        HttpResponseMessage response =
            await _client.GetAsync(
                SummaryRoute,
                TestContext.Current.CancellationToken);

        Assert.Equal(
            HttpStatusCode.OK,
            response.StatusCode);

        ExpenseEntrySummary? summary =
            await response.Content.ReadFromJsonAsync<
                ExpenseEntrySummary>(
                    TestContext.Current.CancellationToken);

        Assert.NotNull(summary);

        return summary;
    }
}
