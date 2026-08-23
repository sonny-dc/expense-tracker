using System.Net;
using System.Net.Http.Json;

using ExpenseTracker.Api.Features.Expenses;
using ExpenseTracker.Api.Features.Expenses.Items.Requests;
using ExpenseTracker.Api.Features.Expenses.Requests;
using ExpenseTracker.Api.Features.Items;

namespace ExpenseTracker.Api.Tests.Features.Expenses.Helpers;

internal static class ExpenseApiTestHelper
{
    public static async Task<Item> CreateItemAsync(
        HttpClient client,
        decimal unitPrice = 100.00m)
    {
        string uniqueValue = Guid.NewGuid()
            .ToString("N");

        var request = new CreateItemRequest
        {
            Name = $"Expense Test Item {uniqueValue}",
            Code = $"ET-{uniqueValue[..12]}",
            Brand = $"Expense Test Brand {uniqueValue}",
            UnitPrice = unitPrice
        };

        HttpResponseMessage response =
            await client.PostAsJsonAsync(
                "/api/v1/items",
                request,
                TestContext.Current.CancellationToken);

        Assert.Equal(
            HttpStatusCode.Created,
            response.StatusCode);

        Item? item =
            await response.Content.ReadFromJsonAsync<Item>(
                TestContext.Current.CancellationToken);

        Assert.NotNull(item);

        return item;
    }

    public static async Task<ExpenseResult> CreateExpenseAsync(
        HttpClient client,
        string title,
        IReadOnlyList<CreateExpenseItemRequest> items,
        string? notes = null)
    {
        var request = new CreateExpenseRequest
        {
            Title = title,
            Notes = notes,
            Items = items
        };

        HttpResponseMessage response =
            await client.PostAsJsonAsync(
                "/api/v1/expenses",
                request,
                TestContext.Current.CancellationToken);

        Assert.Equal(
            HttpStatusCode.Created,
            response.StatusCode);

        ExpenseResult? expense =
            await response.Content.ReadFromJsonAsync<ExpenseResult>(
                TestContext.Current.CancellationToken);

        Assert.NotNull(expense);

        return expense;
    }

    public static async Task DeleteItemIfPresentAsync(
        HttpClient client,
        int itemId)
    {
        await client.DeleteAsync(
            $"/api/v1/items/{itemId}",
            TestContext.Current.CancellationToken);
    }
}
