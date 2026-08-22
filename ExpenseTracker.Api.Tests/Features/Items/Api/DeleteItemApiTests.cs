using System.Net;
using System.Net.Http.Json;

using ExpenseTracker.Api.Features.Items;
using ExpenseTracker.Api.Tests.Infrastructure;

namespace ExpenseTracker.Api.Tests.Features.Items.Api;

public sealed class DeleteItemApiTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public DeleteItemApiTests(
        CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task DeleteAsync_WithExistingItem_ShouldReturnNoContent()
    {
        string uniqueValue = Guid.NewGuid()
            .ToString("N");

        var createRequest = new CreateItemRequest
        {
            Name = $"Delete Test Item {uniqueValue}",
            Code = $"DT-{uniqueValue[..12]}",
            Brand = $"TestBrand {uniqueValue}",
            UnitPrice = 100.00m
        };

        HttpResponseMessage createResponse =
            await _client.PostAsJsonAsync(
                "/api/v1/items",
                createRequest,
                TestContext.Current.CancellationToken);

        Assert.Equal(
            HttpStatusCode.Created,
            createResponse.StatusCode);

        Item? createdItem =
            await createResponse.Content.ReadFromJsonAsync<Item>(
                TestContext.Current.CancellationToken);

        Assert.NotNull(createdItem);

        try
        {
            HttpResponseMessage deleteResponse =
                await _client.DeleteAsync(
                    $"/api/v1/items/{createdItem.ItemId}",
                    TestContext.Current.CancellationToken);

            Assert.Equal(
                HttpStatusCode.NoContent,
                deleteResponse.StatusCode);

            HttpResponseMessage getResponse =
                await _client.GetAsync(
                    $"/api/v1/items/{createdItem.ItemId}",
                    TestContext.Current.CancellationToken);

            Assert.Equal(
                HttpStatusCode.NotFound,
                getResponse.StatusCode);
        }
        finally
        {
            await _client.DeleteAsync(
                $"/api/v1/items/{createdItem.ItemId}",
                TestContext.Current.CancellationToken);
        }
    }

    [Fact]
    public async Task DeleteAsync_WithMissingItem_ShouldReturnNotFound()
    {
        const int missingItemId = int.MaxValue;

        HttpResponseMessage response =
            await _client.DeleteAsync(
                $"/api/v1/items/{missingItemId}",
                TestContext.Current.CancellationToken);

        Assert.Equal(
            HttpStatusCode.NotFound,
            response.StatusCode);
    }
}
