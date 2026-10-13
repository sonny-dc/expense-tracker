using ExpenseTracker.Api.Features.Budget.Accounts.Requests;
using ExpenseTracker.Api.Features.Budget.Accounts.Exceptions;

namespace ExpenseTracker.Api.Features.Budget.Accounts;

public sealed class BudgetAccountService
{
    private readonly BudgetAccountRepository _budgetAccountRepository;
    public BudgetAccountService(BudgetAccountRepository budgetAccountRepository)
    {
        _budgetAccountRepository = budgetAccountRepository;
    }

    public async Task<IReadOnlyList<BudgetAccount>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await _budgetAccountRepository.GetAllAsync(
            cancellationToken);
    }

    public async Task<BudgetAccount> GetByIdAsync(
        int budgetAccountId,
        CancellationToken cancellationToken = default)
    {
        BudgetAccount? budgetAccount = await _budgetAccountRepository.GetByIdAsync(
            budgetAccountId,
            cancellationToken);

        if (budgetAccount is null)
        {
            throw new BudgetAccountNotFoundException();
        }
        return budgetAccount;
    }

    public async Task<BudgetAccount> CreateAsync(
        CreateBudgetAccountRequest request,
        CancellationToken cancellationToken = default)
    {
        return await _budgetAccountRepository.CreateAsync(
            request,
            cancellationToken);
    }

    public async Task<BudgetAccount> UpdateAsync(
        int budgetAccountId,
        UpdateBudgetAccountRequest request,
        CancellationToken cancellationToken = default)
    {
        await GetByIdAsync(budgetAccountId, cancellationToken);
        bool isUpdated = await _budgetAccountRepository.UpdateAsync(
            budgetAccountId,
            request,
            cancellationToken);

        if (!isUpdated)
        {
            throw new InvalidOperationException(
                $"Failed to update the budget account {budgetAccountId} after existence verification.");
        }

        return await GetByIdAsync(budgetAccountId, cancellationToken);
    }

    public async Task DeleteAsync(
        int budgetAccountId,
        CancellationToken cancellationToken)
    {
        await GetByIdAsync(budgetAccountId, cancellationToken);

        bool isDeleted = await _budgetAccountRepository.DeleteAsync(
            budgetAccountId,
            cancellationToken);

        if (!isDeleted)
        {
            throw new InvalidOperationException(
                $"Failed to delete the budget account {budgetAccountId} after existence verification.");
        }
    }
}
