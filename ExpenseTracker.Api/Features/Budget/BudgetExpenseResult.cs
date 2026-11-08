using ExpenseTracker.Api.Features.Budget.Accounts;
using ExpenseTracker.Api.Features.Budget.Logs;

namespace ExpenseTracker.Api.Features.Budget;

/// <summary>
/// This class is used to return the budget account and budget account log
/// for a given expense entry. It is used in the ExpenseResult class.
/// </summary>
public sealed class BudgetExpenseResult
{
    public BudgetExpenseResult(
        BudgetAccount budgetAccount,
        BudgetAccountLog budgetAccountLog)
    {
        BudgetAccount = budgetAccount;
        BudgetAccountLog = budgetAccountLog;
    }

    public BudgetAccount BudgetAccount { get; }

    public BudgetAccountLog BudgetAccountLog { get; }
}
