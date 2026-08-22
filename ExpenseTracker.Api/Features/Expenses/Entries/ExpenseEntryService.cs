using ExpenseTracker.Api.Features.Expenses.Entries.Exceptions;

namespace ExpenseTracker.Api.Features.Expenses.Entries;

public sealed class ExpenseEntryService
{
    private readonly ExpenseEntryRepository _expenseEntryRepository;

    public ExpenseEntryService(ExpenseEntryRepository expenseEntryRepository)
    {
        _expenseEntryRepository = expenseEntryRepository;
    }

    public async Task<IReadOnlyList<ExpenseEntry>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await _expenseEntryRepository.GetAllAsync(cancellationToken);
    }

    public async Task<ExpenseEntry> GetByIdAsync(
        int expenseEntryId,
        CancellationToken cancellationToken = default)
    {
        ExpenseEntry? expenseEntry = await _expenseEntryRepository.GetByIdAsync(
            expenseEntryId,
            cancellationToken);
        if (expenseEntry is null)
        {
            throw new ExpenseEntryNotFoundException();
        }
        return expenseEntry;
    }

    public async Task<ExpenseEntry> CreateAsync(
        CreateExpenseEntryInput input,
        CancellationToken cancellationToken = default)
    {
        return await _expenseEntryRepository.CreateAsync(
            input,
            cancellationToken);
    }
}
