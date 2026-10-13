namespace ExpenseTracker.Api.Features.Budget.Logs;

public sealed class BudgetAccountLogService
{
    private readonly BudgetAccountLogRepository _budgetAccountLogRepository;

    public BudgetAccountLogService(BudgetAccountLogRepository budgetAccountLogRepository)
    {
        _budgetAccountLogRepository = budgetAccountLogRepository;
    }

    public async Task<IReadOnlyList<BudgetAccountLog>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await _budgetAccountLogRepository.GetAllAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<BudgetAccountLog>> GetAllByBudgetAccountIdAsync(
        int budgetAccountId,
        CancellationToken cancellationToken = default)
    {
        return await _budgetAccountLogRepository.GetAllByBudgetAccountIdAsync(
            budgetAccountId,
            cancellationToken);
    }

    public async Task<BudgetAccountLog?> GetByExpenseEntryIdAsync(
        int expenseEntryId,
        CancellationToken cancellationToken = default)
    {
        BudgetAccountLog? budgetAccountLog =
            await _budgetAccountLogRepository.GetByExpenseEntryIdAsync(
                expenseEntryId,
                cancellationToken);

        return budgetAccountLog;
    }

    // Needs an orchestrator to handle the transaction.
    // This method needs to be called within a transaction
    // associated with a budget account and with or without an
    // expense entry, depending on the log type.
    public async Task<BudgetAccountLog> CreateAsync(
        CreateBudgetAccountLogInput input,
        CancellationToken cancellationToken = default)
    {
        return await _budgetAccountLogRepository.CreateAsync(
            input,
            cancellationToken);
    }
}
