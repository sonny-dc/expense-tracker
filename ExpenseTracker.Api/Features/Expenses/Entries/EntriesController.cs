using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Api.Features.Expenses.Entries;

[ApiController]
[Route("expenses/[controller]")]

public sealed class EntriesController : ControllerBase
{
    private readonly ExpenseEntryService _expenseEntryService;

    public EntriesController(ExpenseEntryService expenseEntryService)
    {
        _expenseEntryService = expenseEntryService;
    }

    [HttpGet("summary")]
    public async Task<ActionResult<ExpenseEntrySummary>> GetSummaryAsync(
        CancellationToken cancellationToken)
    {
        ExpenseEntrySummary summary =
            await _expenseEntryService.GetSummaryAsync(
                cancellationToken);
        return Ok(summary);

    }
}
