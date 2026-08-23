using ExpenseTracker.Api.Features.Expenses.Entries;
using ExpenseTracker.Api.Features.Expenses.Items;
using ExpenseTracker.Api.Features.Expenses.Items.Requests;
using ExpenseTracker.Api.Features.Items;

using ExpenseTracker.Api.Features.Expenses.Requests;

using ExpenseTracker.Api.Infrastructure.Database;

namespace ExpenseTracker.Api.Features.Expenses;

public sealed class ExpenseService
{
    private readonly ItemService _itemService;
    private readonly ExpenseEntryService _expenseEntryService;
    private readonly ExpenseItemService _expenseItemService;
    private readonly TransactionManager _transactionManager;

    public ExpenseService(
        ItemService itemService,
        ExpenseEntryService expenseEntryService,
        ExpenseItemService expenseItemService,
        TransactionManager transactionManager)
    {
        _itemService = itemService;
        _expenseEntryService = expenseEntryService;
        _expenseItemService = expenseItemService;
        _transactionManager = transactionManager;
    }

    public async Task<IReadOnlyList<ExpenseResult>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await _transactionManager.ExecuteAsync(async () =>
        {
            IReadOnlyList<ExpenseEntry> expenseEntries =
                await _expenseEntryService.GetAllAsync(cancellationToken);

            if (expenseEntries.Count == 0)
            {
                return [];
            }

            int[] expenseEntryIds = expenseEntries
                .Select(entry => entry.ExpenseEntryId)
                .ToArray();

            IReadOnlyList<ExpenseItem> expenseItems = 
                await _expenseItemService.GetAllByExpenseEntryIdsAsync(
                    expenseEntryIds, 
                    cancellationToken);

            Dictionary<int, List<ExpenseItem>> expenseItemsByEntryId = 
                expenseItems
                    .GroupBy(item => item.ExpenseEntryId)
                    .ToDictionary(
                        group => group.Key,
                        group => group.ToList());
            
            var results = new List<ExpenseResult>(expenseEntries.Count);

            foreach (ExpenseEntry expenseEntry in expenseEntries)
            {
                IReadOnlyList<ExpenseItem> itemsForEntry =
                    expenseItemsByEntryId.GetValueOrDefault(
                        expenseEntry.ExpenseEntryId,
                        new List<ExpenseItem>());

                results.Add(new ExpenseResult(expenseEntry, itemsForEntry));
            }

            return results;

        }, cancellationToken);
    }

    public async Task<ExpenseResult> GetByIdAsync(
        int expenseEntryId,
        CancellationToken cancellationToken = default)
    {
        return await _transactionManager.ExecuteAsync(async () =>
        {
            ExpenseEntry expenseEntry = await _expenseEntryService.GetByIdAsync(
                expenseEntryId,
                cancellationToken);
            
            IReadOnlyList<ExpenseItem> expenseItems = 
                await _expenseItemService.GetAllByExpenseEntryIdAsync(
                    expenseEntry.ExpenseEntryId,
                    cancellationToken);
                
            return new ExpenseResult(expenseEntry, expenseItems);
            
        }, cancellationToken);
    }

    public async Task<ExpenseResult> CreateAsync(
        CreateExpenseRequest input,
        CancellationToken cancellationToken = default)
    {
        return await _transactionManager.ExecuteAsync(async () =>
        {
            IReadOnlyList<NormalizedExpenseItem> normalizedExpenseItems = 
                NormalizeExpenseItems(input.Items);

            int[] itemIds = normalizedExpenseItems
                .Select(item => item.ItemId)
                .ToArray();

            IReadOnlyList<Item> items = await _itemService.GetByIdsAsync(
                itemIds,
                cancellationToken);

            Dictionary<int, Item> itemsById = items
                .ToDictionary(item => item.ItemId);

            var preparedExpenseItems = 
                new List<PreparedExpenseItem>(normalizedExpenseItems.Count);

            decimal totalCost = 0m;

            foreach (NormalizedExpenseItem normalizedExpenseItem in normalizedExpenseItems)
            {
                Item item = itemsById[normalizedExpenseItem.ItemId];
                
                decimal lineTotal = Math.Round(
                    item.UnitPrice * normalizedExpenseItem.Quantity, 
                    decimals: 2,
                    mode: MidpointRounding.AwayFromZero);

                totalCost += lineTotal;

                preparedExpenseItems.Add(new PreparedExpenseItem(
                    item: item,
                    quantity: normalizedExpenseItem.Quantity,
                    lineTotal: lineTotal));
            }

            totalCost = Math.Round(
                totalCost, 
                decimals: 2, 
                mode: MidpointRounding.AwayFromZero);

            ExpenseEntry expenseEntry = await _expenseEntryService.CreateAsync(
                new CreateExpenseEntryInput
                {
                    TotalCost = totalCost,
                    Notes = input.Notes
                },
                cancellationToken);
            
            var expenseItems = new List<ExpenseItem>(preparedExpenseItems.Count);

            foreach (PreparedExpenseItem preparedExpenseItem in preparedExpenseItems)
            {
                ExpenseItem expenseItem = 
                    await _expenseItemService.CreateAsync(
                        new CreateExpenseItemInput
                        {
                            ExpenseEntryId = expenseEntry.ExpenseEntryId,
                            ItemId = preparedExpenseItem.Item.ItemId,
                            ItemNameSnapshot = preparedExpenseItem.Item.Name,
                            ItemCodeSnapshot = preparedExpenseItem.Item.Code,
                            BrandSnapshot = preparedExpenseItem.Item.Brand,
                            Quantity = preparedExpenseItem.Quantity,
                            UnitPriceSnapshot = preparedExpenseItem.Item.UnitPrice,
                            LineTotal = preparedExpenseItem.LineTotal
                        }
                    , cancellationToken);

                expenseItems.Add(expenseItem);
            }

            return new ExpenseResult(expenseEntry, expenseItems);
            
        }, cancellationToken);
    }

    private static IReadOnlyList<NormalizedExpenseItem>
        NormalizeExpenseItems(
            IReadOnlyList<CreateExpenseItemRequest> items)
    {
        var quantitiesByItemId =
            new Dictionary<int, decimal>();

        foreach (CreateExpenseItemRequest item in items)
        {
            decimal existingQuantity =
                quantitiesByItemId.GetValueOrDefault(item.ItemId);

            quantitiesByItemId[item.ItemId] =
                existingQuantity + item.Quantity;
        }
        return quantitiesByItemId
            .Select(pair =>
                new NormalizedExpenseItem(
                    itemId: pair.Key,
                    quantity: pair.Value))
            .ToList();
    }

    private sealed class NormalizedExpenseItem
    {
        public NormalizedExpenseItem(
            int itemId,
            decimal quantity)
        {
            ItemId = itemId;
            Quantity = quantity;
        }

        public int ItemId { get; }

        public decimal Quantity { get; }
    }

    private sealed class PreparedExpenseItem
    {
        public PreparedExpenseItem(
            Item item,
            decimal quantity,
            decimal lineTotal)
        {
            Item = item;
            Quantity = quantity;
            LineTotal = lineTotal;
        }

        public Item Item { get; }

        public decimal Quantity { get; }

        public decimal LineTotal { get; }
    }

}
