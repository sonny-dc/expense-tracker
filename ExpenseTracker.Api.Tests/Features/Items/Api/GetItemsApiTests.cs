using System.Net;
using System.Net.Http.Json;

using ExpenseTracker.Api.Features.Items;
using ExpenseTracker.Api.Tests.Infrastructure;

namespace ExpenseTracker.Api.Tests.Features.Items.Api;

public sealed class GetItemsApiTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public GetItemsApiTests(
        CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnOkWithItems()
    {
        HttpResponseMessage response =
            await _client.GetAsync(
                "/api/v1/items",
                TestContext.Current.CancellationToken);

        Assert.Equal(
            HttpStatusCode.OK,
            response.StatusCode);

        IReadOnlyList<Item>? items =
            await response.Content.ReadFromJsonAsync<IReadOnlyList<Item>>(
                TestContext.Current.CancellationToken);

        Assert.NotNull(items);
    }
}
