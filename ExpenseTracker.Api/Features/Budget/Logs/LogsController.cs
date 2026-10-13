using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Api.Features.Budget.Logs;

[ApiController]
[Route("budget/[controller]")]
public sealed class LogsController : ControllerBase
{
    private readonly BudgetAccountLogService _budgetAccountLogService;

    public LogsController(BudgetAccountLogService budgetAccountLogService)
    {
        _budgetAccountLogService = budgetAccountLogService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<BudgetAccountLog>>> GetAllAsync()
    {
        IReadOnlyList<BudgetAccountLog> logs =
            await _budgetAccountLogService.GetAllAsync();

        return Ok(logs);
    }
}
