using ExpenseTracker.Api.Infrastructure.Database;

using ExpenseTracker.Api.Features.Budget.Accounts;
using ExpenseTracker.Api.Features.Budget.Accounts.Requests;

using ExpenseTracker.Api.Features.Budget.Logs;

using ExpenseTracker.Api.Features.Expenses.Entries;

namespace ExpenseTracker.Api.Features.Budget;

/// <summary>
/// Serves as the parent coordinator for budget-related workflows.
/// It orchestrates budget account and log services, owns transaction
/// boundaries for cross-sub-feature operations, and provides a single
/// application-level entry point for controllers and other feature services.
/// </summary>
public sealed class BudgetService
{
    private readonly BudgetAccountService _budgetAccountService;
    private readonly BudgetAccountLogService _budgetAccountLogService;
    private readonly ExpenseEntryService _expenseEntryService;
    private readonly TransactionManager _transactionManager;

    public BudgetService(
        BudgetAccountService budgetAccountService,
        BudgetAccountLogService budgetAccountLogService,
        ExpenseEntryService expenseEntryService,
        TransactionManager transactionManager)
    {
        _budgetAccountService = budgetAccountService;
        _budgetAccountLogService = budgetAccountLogService;
        _expenseEntryService = expenseEntryService;
        _transactionManager = transactionManager;
    }

    public async Task<BudgetAccount> CreateBudgetAccountAsync(
        CreateBudgetAccountRequest request,
        CancellationToken cancellationToken = default)
    {
        return await _transactionManager.ExecuteAsync(async () =>
        {
            // Init budgetAccount
            BudgetAccount budgetAccount =
                await _budgetAccountService.CreateAsync(
                    request,
                    cancellationToken);

            // Pass values from input & account obj
            var logInput = new CreateBudgetAccountLogInput
            {
                BudgetAccountId = budgetAccount.BudgetAccountId,
                ExpenseEntryId = null,
                Amount = request.Balance,
                BalanceBefore = 0m,
                BalanceAfter = request.Balance,
                LogType = BudgetAccountLogType.StartingBalance
            };

            // Log initial acc activity
            await _budgetAccountLogService.CreateAsync(logInput, cancellationToken);

            return budgetAccount;
        }, cancellationToken);
    }

    public async Task<BudgetAccount> UpdateBudgetAccountAsync(
        int budgetAccountId,
        UpdateBudgetAccountRequest request,
        CancellationToken cancellationToken = default)
    {
        return await _transactionManager.ExecuteAsync(async () =>
        {
            BudgetAccount budgetAccount = await _budgetAccountService.GetByIdAsync(
                budgetAccountId,
                cancellationToken);

            _budgetAccountService.EnsureBudgetAccountStatus(
                budgetAccount,
                BudgetAccountStatus.Open,
                BudgetAccountStatus.Archived);

            BudgetAccount updatedAccount = await _budgetAccountService.UpdateAsync(
                budgetAccountId,
                request,
                cancellationToken);

            if (request.Balance is decimal requestedBalance &&
                requestedBalance != budgetAccount.Balance)
            {
                decimal balanceBefore = budgetAccount.Balance;
                decimal balanceAfter = requestedBalance;
                decimal amount = balanceAfter - balanceBefore;

                await _budgetAccountLogService.CreateAsync(
                    new CreateBudgetAccountLogInput
                    {
                        BudgetAccountId = budgetAccount.BudgetAccountId,
                        ExpenseEntryId = null,
                        Amount = amount,
                        BalanceBefore = balanceBefore,
                        BalanceAfter = balanceAfter,
                        LogType = BudgetAccountLogType.Adjustment
                    }
                , cancellationToken);
            }

            return updatedAccount;

        }, cancellationToken);
    }

    // Not called on the controller, this is used
    // by the ExpenseService to record an expense with a budget account.
    public async Task<BudgetAccount> RecordExpenseAsync(
        BudgetExpenseInput input,
        CancellationToken cancellationToken = default)
    {
        var account = await _budgetAccountService.GetByIdAsync(input.BudgetAccountId, cancellationToken);
        _budgetAccountService.EnsureBudgetAccountStatus(account, BudgetAccountStatus.Open);
        decimal balanceBefore = account.Balance;
        decimal balanceAfter = balanceBefore - input.Amount;
        var updateInput = new UpdateBudgetAccountRequest
        {
            Name = null,
            Description = null,
            Balance = balanceAfter,
            Status = null
        };

        BudgetAccount updatedAccount =
            await _budgetAccountService.UpdateAsync(
                input.BudgetAccountId,
                updateInput,
                cancellationToken);

        await _budgetAccountLogService.CreateAsync(
            new CreateBudgetAccountLogInput
            {
                BudgetAccountId = input.BudgetAccountId,
                ExpenseEntryId = input.ExpenseEntryId,
                Amount = -input.Amount,
                BalanceBefore = balanceBefore,
                BalanceAfter = balanceAfter,
                LogType = BudgetAccountLogType.Expense
            }, cancellationToken);

        return updatedAccount;
    }

    public async Task<IReadOnlyList<BudgetAccountLog>> GetLogsByBudgetAccountIdAsync(
        int budgetAccountId,
        CancellationToken cancellationToken = default)
    {
        return await _transactionManager.ExecuteAsync(async () =>
        {
            BudgetAccount budgetAccount =
                await _budgetAccountService.GetByIdAsync(
                    budgetAccountId,
                    cancellationToken);

            return await _budgetAccountLogService.GetAllByBudgetAccountIdAsync(
                budgetAccount.BudgetAccountId,
                cancellationToken);
        }, cancellationToken);
    }

    public async Task<BudgetAccountLog?> GetLogByExpenseEntryIdAsync(
        int expenseEntryId,
        CancellationToken cancellationToken = default)
    {
        return await _transactionManager.ExecuteAsync(async () =>
        {
            ExpenseEntry expenseEntry =
                await _expenseEntryService.GetByIdAsync(
                    expenseEntryId,
                    cancellationToken);

            return await _budgetAccountLogService.GetByExpenseEntryIdAsync(expenseEntry.ExpenseEntryId, cancellationToken);
        }, cancellationToken);
    }
}
