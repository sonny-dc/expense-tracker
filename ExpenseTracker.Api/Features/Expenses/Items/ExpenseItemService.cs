using ExpenseTracker.Api.Features.Expenses.Items.Exceptions;

namespace ExpenseTracker.Api.Features.Expenses.Items;

public sealed class ExpenseItemService
{
    private readonly ExpenseItemRepository _expenseItemRepository;

    public ExpenseItemService(ExpenseItemRepository expenseItemRepository)
    {
        _expenseItemRepository = expenseItemRepository;
    }

    public async Task<IReadOnlyList<ExpenseItem>> GetAllByExpenseEntryIdAsync(
        int expenseEntryId,
        CancellationToken cancellationToken = default)
    {
        return await _expenseItemRepository.GetAllByExpenseEntryIdAsync(
            expenseEntryId, 
            cancellationToken);
    }

    public async Task<ExpenseItem> GetByIdAsync(
        int expenseItemId,
        CancellationToken cancellationToken = default)
    {
        ExpenseItem? expenseItem = await _expenseItemRepository.GetByIdAsync(
            expenseItemId,
            cancellationToken);
        if (expenseItem is null)
        {
            throw new ExpenseItemNotFoundException();
        }
        return expenseItem;
    }

    public async Task<ExpenseItem> CreateAsync(
        CreateExpenseItemInput input,
        CancellationToken cancellationToken = default)
    {
        return await _expenseItemRepository.CreateAsync(
            input,
            cancellationToken);
    }
}
