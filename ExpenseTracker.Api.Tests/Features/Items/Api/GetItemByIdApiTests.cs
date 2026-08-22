using System.Net;
using System.Net.Http.Json;

using ExpenseTracker.Api.Features.Items;
using ExpenseTracker.Api.Tests.Infrastructure;

namespace ExpenseTracker.Api.Tests.Features.Items.Api;

public sealed class GetItemByIdApiTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public GetItemByIdApiTests(
        CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetByIdAsync_WithExistingItem_ShouldReturnItem()
    {
        string uniqueValue = Guid.NewGuid()
            .ToString("N");

        var createRequest = new CreateItemRequest
        {
            Name = $"Notebook {uniqueValue}",
            Code = $"NB-{uniqueValue[..12]}",
            Brand = $"TestBrand {uniqueValue}",
            UnitPrice = 75.00m
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
            HttpResponseMessage response =
                await _client.GetAsync(
                    $"/api/v1/items/{createdItem.ItemId}",
                    TestContext.Current.CancellationToken);

            Assert.Equal(
                HttpStatusCode.OK,
                response.StatusCode);

            Item? retrievedItem =
                await response.Content.ReadFromJsonAsync<Item>(
                    TestContext.Current.CancellationToken);

            Assert.NotNull(retrievedItem);
            Assert.Equal(createdItem.ItemId, retrievedItem.ItemId);
            Assert.Equal(createRequest.Name, retrievedItem.Name);
            Assert.Equal(createRequest.Code, retrievedItem.Code);
            Assert.Equal(createRequest.Brand, retrievedItem.Brand);
            Assert.Equal(createRequest.UnitPrice, retrievedItem.UnitPrice);
        }
        finally
        {
            await _client.DeleteAsync(
                $"/api/v1/items/{createdItem.ItemId}",
                TestContext.Current.CancellationToken);
        }
    }

    [Fact]
    public async Task GetByIdAsync_WithMissingItem_ShouldReturnNotFound()
    {
        const int missingItemId = int.MaxValue;

        HttpResponseMessage response =
            await _client.GetAsync(
                $"/api/v1/items/{missingItemId}",
                TestContext.Current.CancellationToken);

        Assert.Equal(
            HttpStatusCode.NotFound,
            response.StatusCode);
    }
}
