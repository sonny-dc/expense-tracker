using System.Net;
using System.Net.Http.Json;

using ExpenseTracker.Api.Features.Items;
using ExpenseTracker.Api.Tests.Infrastructure;

namespace ExpenseTracker.Api.Tests.Features.Items.Api;

public sealed class CreateItemApiTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public CreateItemApiTests(
        CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CreateAsync_WithValidRequest_ShouldReturnCreatedItem()
    {
        string uniqueValue = Guid.NewGuid()
            .ToString("N");

        var request = new CreateItemRequest
        {
            Name = $"Bond Paper {uniqueValue}",
            Code = $"BP-{uniqueValue[..12]}",
            Brand = $"PaperOne {uniqueValue}",
            UnitPrice = 250.00m
        };

        HttpResponseMessage response =
            await _client.PostAsJsonAsync(
                "/api/v1/items",
                request,
                TestContext.Current.CancellationToken);

        Assert.Equal(
            HttpStatusCode.Created,
            response.StatusCode);

        Item? createdItem =
            await response.Content.ReadFromJsonAsync<Item>(
                TestContext.Current.CancellationToken);

        Assert.NotNull(createdItem);

        try
        {
            Assert.True(createdItem.ItemId > 0);

            Assert.Equal(
                request.Name,
                createdItem.Name);

            Assert.Equal(
                request.Code,
                createdItem.Code);

            Assert.Equal(
                request.Brand,
                createdItem.Brand);

            Assert.Equal(
                request.UnitPrice,
                createdItem.UnitPrice);

            Assert.NotNull(response.Headers.Location);

            Assert.EndsWith(
                $"/api/v1/items/{createdItem.ItemId}",
                response.Headers.Location.ToString());
        }
        finally
        {
            await _client.DeleteAsync(
                $"/api/v1/items/{createdItem.ItemId}",
                TestContext.Current.CancellationToken);
        }
    }
}
