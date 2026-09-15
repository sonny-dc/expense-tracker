using System.Net.Http.Json;
using System.Text.Json;
using ExpenseTracker.WinForms.Features.Items.Models;
using ExpenseTracker.WinForms.Infrastructure.Http;

namespace ExpenseTracker.WinForms.Features.Items.Api;

public sealed class ItemsApiClient
{
    private const string ItemsRoute = "items";

    private readonly IHttpClientFactory _httpClientFactory;

    public ItemsApiClient(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IReadOnlyList<Item>> GetAllAsync(
    CancellationToken cancellationToken = default)
    {
        HttpClient httpClient =
            _httpClientFactory.CreateApiClient();

        using HttpResponseMessage response =
            await httpClient.GetAsync(
                ItemsRoute,
                cancellationToken);

        await response.EnsureApiSuccessAsync(
            cancellationToken);

        IReadOnlyList<Item>? items =
            await response.Content.ReadFromJsonAsync<
                IReadOnlyList<Item>>(
                    cancellationToken);

        return items ?? [];
    }

    public async Task<Item> CreateAsync(
        CreateItemRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        HttpClient httpClient =
            _httpClientFactory.CreateApiClient();

        using HttpResponseMessage response =
            await httpClient.PostAsJsonAsync(
                ItemsRoute,
                request,
                cancellationToken);

        await response.EnsureApiSuccessAsync(
            cancellationToken);

        Item? createdItem = await response.Content.ReadFromJsonAsync<Item>(cancellationToken);

        return createdItem ?? throw new JsonException(
            "The API response did not contain a valid item.");
    }

    public async Task<Item> UpdateAsync(
        int itemId,
        UpdateItemRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(itemId);

        ArgumentNullException.ThrowIfNull(request);

        HttpClient httpClient =
            _httpClientFactory.CreateApiClient();

        using HttpResponseMessage response =
            await httpClient.PatchAsJsonAsync(
                $"{ItemsRoute}/{itemId}",
                request,
                cancellationToken);

        await response.EnsureApiSuccessAsync(
            cancellationToken);

        Item? updatedItem = await response.Content.ReadFromJsonAsync<Item>(cancellationToken);

        return updatedItem ?? throw new JsonException(
            "The API response did not contain a valid item.");
    }

    public async Task DeleteAsync(
        int itemId,
        CancellationToken cancellationToken = default)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(
            itemId);

        HttpClient httpClient =
            _httpClientFactory.CreateApiClient();

        string itemRoute =
            $"{ItemsRoute}/{itemId}";

        using HttpResponseMessage response =
            await httpClient.DeleteAsync(
                itemRoute,
                cancellationToken);

        await response.EnsureApiSuccessAsync(
            cancellationToken);
    }
}
