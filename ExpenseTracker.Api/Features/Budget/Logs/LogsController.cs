using Microsoft.AspNetCore.Mvc;

using ExpenseTracker.Api.Features.Budget;

namespace ExpenseTracker.Api.Features.Budget.Logs;

[ApiController]
[Route("budget/[controller]")]
public sealed class LogsController : ControllerBase
{
    private readonly BudgetAccountLogService _budgetAccountLogService;
    private readonly BudgetService _budgetService;

    public LogsController(
        BudgetAccountLogService budgetAccountLogService,
        BudgetService budgetService)
    {
        _budgetAccountLogService = budgetAccountLogService;
        _budgetService = budgetService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<BudgetAccountLog>>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        IReadOnlyList<BudgetAccountLog> logs =
            await _budgetAccountLogService.GetAllAsync(cancellationToken);

        return Ok(logs);
    }

    [HttpGet("accounts/{budgetAccountId:int}")]
    public async Task<ActionResult<IReadOnlyList<BudgetAccountLog>>> GetLogsByBudgetAccountIdAsync(
        [FromRoute] int budgetAccountId,
        CancellationToken cancellationToken)
    {
        IReadOnlyList<BudgetAccountLog> logs = await _budgetService.GetLogsByBudgetAccountIdAsync(
            budgetAccountId,
            cancellationToken);

        return Ok(logs);
    }

    [HttpGet("expense-entries/{expenseEntryId:int}")]
    public async Task<ActionResult<BudgetAccountLog?>> GetLogByExpenseEntryIdAsync(
        [FromRoute] int expenseEntryId,
        CancellationToken cancellationToken)
    {
        BudgetAccountLog? log = await _budgetService.GetLogByExpenseEntryIdAsync(
            expenseEntryId,
            cancellationToken);

        return Ok(log);
    }
}
