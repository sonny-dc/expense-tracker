using System.Net;
using System.Net.Http.Json;

using ExpenseTracker.Api.Features.Items;
using ExpenseTracker.Api.Tests.Infrastructure;

namespace ExpenseTracker.Api.Tests.Features.Items.Api;

public sealed class UpdateItemApiTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public UpdateItemApiTests(
        CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task UpdateAsync_WithValidRequest_ShouldReturnUpdatedItem()
    {
        string uniqueValue = Guid.NewGuid()
            .ToString("N");

        var createRequest = new CreateItemRequest
        {
            Name = $"Printer Paper {uniqueValue}",
            Code = $"PP-{uniqueValue[..12]}",
            Brand = $"TestBrand {uniqueValue}",
            UnitPrice = 200.00m
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
            var updateRequest = new UpdateItemRequest
            {
                UnitPrice = 225.00m
            };

            HttpResponseMessage response =
                await _client.PatchAsJsonAsync(
                    $"/api/v1/items/{createdItem.ItemId}",
                    updateRequest,
                    TestContext.Current.CancellationToken);

            Assert.Equal(
                HttpStatusCode.OK,
                response.StatusCode);

            Item? updatedItem =
                await response.Content.ReadFromJsonAsync<Item>(
                    TestContext.Current.CancellationToken);

            Assert.NotNull(updatedItem);

            Assert.Equal(
                createdItem.ItemId,
                updatedItem.ItemId);

            Assert.Equal(
                createRequest.Name,
                updatedItem.Name);

            Assert.Equal(
                createRequest.Code,
                updatedItem.Code);

            Assert.Equal(
                createRequest.Brand,
                updatedItem.Brand);

            Assert.Equal(
                updateRequest.UnitPrice,
                updatedItem.UnitPrice);
        }
        finally
        {
            await _client.DeleteAsync(
                $"/api/v1/items/{createdItem.ItemId}",
                TestContext.Current.CancellationToken);
        }
    }

    [Fact]
    public async Task UpdateAsync_WithMissingItem_ShouldReturnNotFound()
    {
        const int missingItemId = int.MaxValue;

        var request = new UpdateItemRequest
        {
            UnitPrice = 225.00m
        };

        HttpResponseMessage response =
            await _client.PatchAsJsonAsync(
                $"/api/v1/items/{missingItemId}",
                request,
                TestContext.Current.CancellationToken);

        Assert.Equal(
            HttpStatusCode.NotFound,
            response.StatusCode);
    }

    [Fact]
    public async Task UpdateAsync_WithNoFieldsProvided_ShouldReturnBadRequest()
    {
        const int itemId = 1;

        var request = new UpdateItemRequest();

        HttpResponseMessage response =
            await _client.PatchAsJsonAsync(
                $"/api/v1/items/{itemId}",
                request,
                TestContext.Current.CancellationToken);

        Assert.Equal(
            HttpStatusCode.BadRequest,
            response.StatusCode);
    }
}
