using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Api.Features.Budget.Accounts;

[ApiController]
[Route("budget/[controller]")]
public sealed class AccountsController : ControllerBase
{
    private readonly BudgetAccountService _budgetAccountService;

    public AccountsController(BudgetAccountService budgetAccountService)
    {
        _budgetAccountService = budgetAccountService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<BudgetAccount>>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        IReadOnlyList<BudgetAccount> accounts =
            await _budgetAccountService.GetAllAsync(cancellationToken);

        return Ok(accounts);
    }

    [HttpGet("{budgetAccountId:int}")]
    public async Task<ActionResult<BudgetAccount>> GetByIdAsync(
        [FromRoute] int budgetAccountId,
        CancellationToken cancellationToken)
    {
        BudgetAccount account = await _budgetAccountService.GetByIdAsync(
            budgetAccountId,
            cancellationToken);

        return Ok(account);
    }
}
