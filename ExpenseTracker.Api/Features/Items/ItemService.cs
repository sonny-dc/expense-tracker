using ExpenseTracker.Api.Features.Items.Exceptions;

namespace ExpenseTracker.Api.Features.Items;

public class ItemService
{
    private readonly ItemRepository _itemRepository;
    public ItemService(ItemRepository itemRepository)
    {
        _itemRepository = itemRepository;
    }

    public async Task<IReadOnlyList<Item>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await _itemRepository.GetAllAsync(
            cancellationToken);
    }

    public async Task<Item> GetByIdAsync(
        int itemId,
        CancellationToken cancellationToken = default)
    {
        Item? item = await _itemRepository.GetByIdAsync(
            itemId,
            cancellationToken);
        if (item is null)
        {
            throw new ItemNotFoundException();
        }
        return item;
    }

    public async Task<IReadOnlyList<Item>> GetByIdsAsync(
        IReadOnlyCollection<int> itemIds,
        CancellationToken cancellationToken = default)
    {
        int[] distinctItemIds = itemIds.Distinct().ToArray();

        if (distinctItemIds.Length == 0)
        {
            return [];
        }

        IReadOnlyList<Item> items = await _itemRepository.GetByIdsAsync(
            distinctItemIds,
            cancellationToken);

        if (items.Count != distinctItemIds.Length)
        {
            throw new ItemNotFoundException("One or more items were not found.");
        }

        return items;
    }

    public async Task<Item> CreateAsync(
        CreateItemRequest request,
        CancellationToken cancellationToken = default)
    {
        bool alreadyExists =
            await _itemRepository.AlreadyExistsAsync(
                name: request.Name,
                code: request.Code,
                brand: request.Brand,
                cancellationToken: cancellationToken);

        if (alreadyExists)
        {
            throw new DuplicateItemException();
        }

        return await _itemRepository.CreateAsync(
            request,
            cancellationToken);
    }

    public async Task<Item> UpdateAsync(
        int itemId,
        UpdateItemRequest request,
        CancellationToken cancellationToken = default)
    {
        Item existingItem = await GetByIdAsync(itemId, cancellationToken);

        string effectiveName = request.Name ?? existingItem.Name;
        string effectiveCode = request.Code ?? existingItem.Code;
        string effectiveBrand = request.Brand ?? existingItem.Brand;

        bool alreadyExists =
            await _itemRepository.AlreadyExistsAsync(
                name: effectiveName,
                code: effectiveCode,
                brand: effectiveBrand,
                excludedItemId: itemId,
                cancellationToken: cancellationToken);

        if (alreadyExists)
        {
            throw new DuplicateItemException();
        }

        bool wasUpdated = await _itemRepository.UpdateAsync(
            itemId,
            request,
            cancellationToken);

        if (!wasUpdated)
        {
            throw new ItemNotFoundException();
        }

        return await GetByIdAsync(
            itemId, 
            cancellationToken);
    }

    public async Task DeleteAsync(
        int itemId,
        CancellationToken cancellationToken = default)
    {
        bool wasDeleted = await _itemRepository.DeleteAsync(
            itemId,
            cancellationToken);

        if (!wasDeleted)
        {
            throw new ItemNotFoundException();
        }
    }
}
